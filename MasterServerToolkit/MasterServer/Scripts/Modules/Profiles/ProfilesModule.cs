using MasterServerToolkit.Logging;
using MasterServerToolkit.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MasterServerToolkit.MasterServer
{
    public delegate ObservableServerProfile ProfileFactory(string userId, IPeer clientPeer);

    /// <summary>
    /// Handles player profiles within master server.
    /// Listens to changes in player profiles, and sends updates to
    /// clients of interest.
    /// Also, reads changes from game server, and applies them to players profile
    /// </summary>
    public class ProfilesModule : BaseServerModule
    {
        #region INSPECTOR

        /// <summary>
        /// Time to pass after logging out, until profile
        /// will be removed from the lookup. Should be enough for game
        /// server to submit last changes
        /// </summary>
        [Tooltip("Time to pass after logging out, until profile will be removed from the lookup. Should be enough for game server to submit last changes")]
        public float unloadProfileAfter = 20f;

        /// <summary>
        /// Interval, in which updated profiles will be saved to database
        /// </summary>
        [Tooltip("Interval, in which updated profiles will be saved to database")]
        public float saveProfileInterval = 1f;

        /// <summary>
        /// Interval, in which profile updates will be sent to clients
        /// </summary>
        [Tooltip("Interval, in which profile updates will be sent to clients")]
        public float clientUpdateInterval = 0f;

        /// <summary>
        /// Permission user need to have to edit profile
        /// </summary>
        [Tooltip("Permission user need to have to edit profile")]
        public int editProfilePermissionLevel = 0;

        /// <summary>
        /// Ignore errors occurred when profile data mismatch
        /// </summary>
        [Tooltip("Ignore errors occurred when profile data mismatch")]
        public bool ignoreProfileMissmatchError = false;

        /// <summary>
        /// Database accessor factory that helps to create integration with profile db
        /// </summary>
        [Tooltip("Database accessor factory that helps to create integration with profile db")]
        public DatabaseAccessorFactory databaseAccessorFactory;

        #endregion

        /// <summary>
        /// Auth module for listening to auth events
        /// </summary>
        protected AuthModule authModule;

        /// <summary>
        /// List of profiles that will be saved to to DB with updates
        /// </summary>
        protected HashSet<string> profilesToBeSaved;

        /// <summary>
        /// List of profiles that will be sent to clients with updates
        /// </summary>
        protected HashSet<string> profilesToBeSentToClients;

        /// <summary>
        /// DB to work with profile data
        /// </summary>
        protected IProfilesDatabaseAccessor profileDatabaseAccessor;

        /// <summary>
        /// List of the users profiles
        /// </summary>
        protected Dictionary<string, ObservableServerProfile> profilesList;

        /// <summary>
        /// By default, profiles module will use this factory to create a profile for users.
        /// If you're using profiles, you will need to change this factory to construct the
        /// structure of a profile.
        /// </summary>
        public ProfileFactory ProfileFactory { get; set; }

        /// <summary>
        /// Gets list of userprofiles
        /// </summary>
        public IEnumerable<ObservableServerProfile> Profiles => profilesList.Values;

        /// <summary>
        /// Ignore errors occurred when profile data mismatch. False by default
        /// </summary>
        public bool IgnoreProfileMissmatchError
        {
            get { return ignoreProfileMissmatchError; }
            set { ignoreProfileMissmatchError = value; }
        }

        protected override void Awake()
        {
            base.Awake();

            if (DestroyIfExists())
            {
                return;
            }

            // Add auth module as a dependency of this module
            AddOptionalDependency<AuthModule>();

            // List of oaded profiles
            profilesList = new Dictionary<string, ObservableServerProfile>();

            // List of profiles that are waiting to be saved to DB
            profilesToBeSaved = new HashSet<string>();

            // List of profiles that are waiting to be sent to clients
            profilesToBeSentToClients = new HashSet<string>();
        }

        public override void Initialize(IServer server)
        {
            databaseAccessorFactory?.CreateAccessors();
            profileDatabaseAccessor = Mst.Server.DbAccessors.GetAccessor<IProfilesDatabaseAccessor>();

            if (profileDatabaseAccessor == null)
            {
                logger.Error("Profiles database implementation was not found");
            }

            // Auth dependency setup
            authModule = server.GetModule<AuthModule>();

            if (authModule != null)
            {
                authModule.OnUserLoggedInEvent += AuthModule_OnUserLoggedInEvent;
            }

            // Games dependency setup
            server.RegisterMessageHandler((short)MstMessageCodes.ServerProfileRequest, GameServerProfileRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.ClientProfileRequest, ClientProfileRequestHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.UpdateServerProfile, ProfileUpdateHandler);
            //server.RegisterMessageHandler((short)MstMessageCodes.Get_Profile_By_UserID, Get_Profile_By_UserIDHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Open_Treasure, Open_TreasureHandler);
            server.RegisterMessageHandler((short)MstMessageCodes.Tower_Level_UP, Tower_Level_UPHandler);
        }

        public override MstProperties Info()
        {
            MstProperties info = base.Info();

            info.Add("Database Accessor", profileDatabaseAccessor != null ? "Connected" : "Not Connected");
            info.Add("Profiles", Profiles.Count());

            return info;
        }

        /// <summary>
        /// Triggered when the user has successfully logged in
        /// </summary>
        /// <param name="session"></param>
        /// <param name="accountData"></param>
        protected virtual async void AuthModule_OnUserLoggedInEvent(IUserPeerExtension user)
        {
            user.Peer.OnPeerDisconnectedEvent += OnPeerPlayerDisconnectedEventHandler;

            // Create a profile
            ObservableServerProfile profile;

            if (profilesList.ContainsKey(user.UserId))
            {
                // There's a profile from before, which we can use
                profile = profilesList[user.UserId];
                profile.ClientPeer = user.Peer;
            }
            else
            {
                // We need to create a new one
                profile = CreateProfile(user.UserId, user.Peer);
                profilesList.Add(user.UserId, profile);
            }

            // Restore profile data from database
            await profileDatabaseAccessor.RestoreProfileAsync(profile);

            // Save profile property
            user.Peer.AddExtension(new ProfilePeerExtension(profile, user.Peer));

            // Listen to profile events
            profile.OnModifiedInServerEvent += OnProfileChangedEventHandler;
        }

        /// <summary>
        /// Creates an observable profile for a client.
        /// Override this, if you want to customize the profile creation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientPeer"></param>
        /// <returns></returns>
        protected virtual ObservableServerProfile CreateProfile(string userId, IPeer clientPeer)
        {
            if (ProfileFactory != null)
            {
                return ProfileFactory(userId, clientPeer);
            }

            return new ObservableServerProfile(userId, clientPeer);
        }

        /// <summary>
        /// Invoked, when profile is changed
        /// </summary>
        /// <param name="profile"></param>
        private void OnProfileChangedEventHandler(ObservableServerProfile profile)
        {
            var user = profile.ClientPeer.GetExtension<IUserPeerExtension>();

            if (!user.Account.IsGuest || (user.Account.IsGuest && authModule.SaveGuestInfo))
            {
                if (!profilesToBeSaved.Contains(profile.UserId) && profile.ShouldBeSavedToDatabase)
                {
                    // If profile is not already waiting to be saved
                    profilesToBeSaved.Add(profile.UserId);
                    _ = SaveProfile(profile, saveProfileInterval);
                }
            }

            if (!profilesToBeSentToClients.Contains(profile.UserId))
            {
                // If it's a master server
                profilesToBeSentToClients.Add(profile.UserId);
                _ = SendUpdatesToClient(profile, clientUpdateInterval);
            }
        }

        /// <summary>
        /// Invoked, when user logs out (disconnects from master)
        /// </summary>
        /// <param name="session"></param>
        private void OnPeerPlayerDisconnectedEventHandler(IPeer peer)
        {
            peer.OnPeerDisconnectedEvent -= OnPeerPlayerDisconnectedEventHandler;

            var profileExtension = peer.GetExtension<ProfilePeerExtension>();

            if (profileExtension == null)
            {
                return;
            }

            // Unload profile
            _ = UnloadProfile(profileExtension.UserId, unloadProfileAfter);
        }

        /// <summary>
        /// Saves a profile into database after delay
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private async Task SaveProfile(ObservableServerProfile profile, float delay)
        {
            // Wait for the delay
            await Task.Delay(Mathf.RoundToInt(delay < 0.01f ? 0.01f * 1000 : delay * 1000));

            // Remove value from debounced updates
            profilesToBeSaved.Remove(profile.UserId);

            await profileDatabaseAccessor.UpdateProfileAsync(profile);
        }

        /// <summary>
        /// Collects changes in the profile, and sends them to client after delay
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private async Task SendUpdatesToClient(ObservableServerProfile profile, float delay)
        {
            // Wait for the delay
            await Task.Delay(Mathf.RoundToInt(delay < 0.01f ? 0.01f * 1000 : delay * 1000));

            if (profile.ClientPeer == null || !profile.ClientPeer.IsConnected)
            {
                // If client is not connected, and we don't need to send him profile updates
                profile.ClearUpdates();

                // Remove value from debounced updates
                profilesToBeSentToClients.Remove(profile.UserId);

                return;
            }

            // Get profile updated data in bytes
            var updates = profile.GetUpdates();

            // Clear updated data in profile
            profile.ClearUpdates();

            // Send these data to client
            profile.ClientPeer.SendMessage(MessageHelper.Create((short)MstMessageCodes.UpdateClientProfile, updates), DeliveryMethod.ReliableSequenced);

            // Remove value from debounced updates
            profilesToBeSentToClients.Remove(profile.UserId);
        }

        /// <summary>
        /// Unloads profile after a period of time
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private async Task UnloadProfile(string userId, float delay)
        {
            // Wait for the delay
            await Task.Delay(Mathf.RoundToInt(delay < 0.01f ? 0.01f * 1000 : delay * 1000));

            // If user is logged in, do nothing
            if (authModule.IsUserLoggedInById(userId))
            {
                return;
            }

            profilesList.TryGetValue(userId, out ObservableServerProfile profile);

            if (profile == null)
            {
                return;
            }

            // Remove profile
            profilesList.Remove(userId);

            // Remove listeners
            profile.OnModifiedInServerEvent -= OnProfileChangedEventHandler;
        }

        /// <summary>
        /// Check if given peer has permission to edit profile
        /// </summary>
        /// <param name="messagePeer"></param>
        /// <returns></returns>
        protected virtual bool HasPermissionToEditProfiles(IPeer messagePeer)
        {
            var securityExtension = messagePeer.GetExtension<SecurityInfoPeerExtension>();

            return securityExtension != null
                   && securityExtension.PermissionLevel >= editProfilePermissionLevel;
        }

        #region INCOMMING MESSAGES

        /// <summary>
        /// Handles a message from game server, which includes player profiles updates
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ProfileUpdateHandler(IIncomingMessage message)
        {
            if (!HasPermissionToEditProfiles(message.Peer))
            {
                Logs.Error("Master server received an update for a profile, but peer who tried to " +
                           "update it did not have sufficient permissions");
                return;
            }

            var data = message.AsBytes();

            using (var ms = new MemoryStream(data))
            {
                using (var reader = new EndianBinaryReader(EndianBitConverter.Big, ms))
                {
                    // Read profiles count
                    var count = reader.ReadInt32();

                    for (var i = 0; i < count; i++)
                    {
                        // Read userId
                        var userId = reader.ReadString();

                        // Read updates length
                        var updatesLength = reader.ReadInt32();

                        // Read updates
                        var updates = reader.ReadBytes(updatesLength);

                        try
                        {
                            if (profilesList.TryGetValue(userId, out ObservableServerProfile profile))
                            {
                                profile.ApplyUpdates(updates);
                            }
                        }
                        catch (Exception e)
                        {
                            Logs.Error("Error while trying to handle profile updates from master server");
                            Logs.Error(e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles a request from client to get profile
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ClientProfileRequestHandler(IIncomingMessage message)
        {
            var clientPropCount = message.AsInt();
            Debug.Log("ClientProfileRequestHandler || clientPropCount || " + clientPropCount);
            var profileExt = message.Peer.GetExtension<ProfilePeerExtension>();

            if (profileExt == null)
            {
                message.Respond("Profile not found", ResponseStatus.Failed);
                return;
            }

            profileExt.Profile.ClientPeer = message.Peer;
            ObservableServerProfile Profile = profileExt.Profile;

            bool Set_Daily_Task = false, Set_Week_Task = false;

            #region Login_Time and Task_Time
            var Property = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Timer_05);
            int Last_Login_Time = Property.GetValue();

            int Year = DateTime.UtcNow.Year - 2000;
            int Month = DateTime.UtcNow.Month;
            int Date = DateTime.UtcNow.Day;
            int Hour = DateTime.UtcNow.Hour;
            int Minute = DateTime.UtcNow.Minute;
            DateTime datetime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            DateTime Last_Login_DateTime = new DateTime(2000, 1, 1);
            DateTime Last_Sunday = datetime.AddDays(-(int)datetime.DayOfWeek);
            if (Last_Login_Time != 0)
            {
                string Last_Login_Time_String = Last_Login_Time.ToString();
                int temp_year = int.Parse(Last_Login_Time_String.Substring(0, 2)) + 2000;
                short year = (short)temp_year;
                short month = Covert_String_Time(Last_Login_Time_String, 2);
                short date = Covert_String_Time(Last_Login_Time_String, 4);
                short hour = Covert_String_Time(Last_Login_Time_String, 6);
                short min = Covert_String_Time(Last_Login_Time_String, 8);
                Last_Login_DateTime = new DateTime(year, month, date, hour, min, 00);

                short Covert_String_Time(string QTY, short start_pos)
                {
                    return short.Parse(QTY.Substring(start_pos, 2));
                }
            }

            if (Last_Login_DateTime.Ticks < Last_Sunday.Ticks)
                Set_Week_Task = true;

            DateTime Last_Login_Date = new DateTime(Last_Login_DateTime.Year, Last_Login_DateTime.Month, Last_Login_DateTime.Day);
            DateTime Today = new DateTime(Year + 2000, Month, Date);

            if (Last_Login_Date != Today)
                Set_Daily_Task = true;

            string time = Convert_Time(Year) + Convert_Time(Month) + Convert_Time(Date) + Convert_Time(Hour) + Convert_Time(Minute);
            int int_Time = int.Parse(time);
            string Convert_Time(int number)
            {
                string time_text = number.ToString();
                if (number < 10)
                    time_text = "0" + time_text;
                return time_text;
            }

            Property.Set(int_Time);
            #endregion

            #region Set_Task
            bool Daily_Task_Empty = GetComponent<Task_Info>().Check_Task_Assign(Profile, (short)Task_Code.Daily_Task); // Daily
            bool Week__Task_Empty = GetComponent<Task_Info>().Check_Task_Assign(Profile, (short)Task_Code.Week_Task); // Week

            Set_Daily_Task = true;
            Set_Week_Task = true;

            // Daily_Task_Empty = true; Week__Task_Empty = true; ///////////////////////////////////////// for Test
            if (Set_Daily_Task || Daily_Task_Empty)
            {
                Reset_DailyTask_Player_QTY_Property(Profile);
                GetComponent<Task_Info>().Start_Set_Task(Profile, true); // Daily

                int D_Task1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_Type).GetValue();
                int D_Task2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_Type).GetValue();
                int D_Task3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_Type).GetValue();

                int QTY1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_1_QTY).GetValue();
                int QTY2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_2_QTY).GetValue();
                int QTY3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_3_QTY).GetValue();

                string Test1 = GetComponent<Task_Info>().Get_Task_Text_Info((short)D_Task1, 0, QTY1);
                string Test2 = GetComponent<Task_Info>().Get_Task_Text_Info((short)D_Task2, 0, QTY2);
                string Test3 = GetComponent<Task_Info>().Get_Task_Text_Info((short)D_Task3, 0, QTY3);

                Debug.Log("Test 2D || " + Test1 + " || " + Test2 + " || " + Test3);
            }

            if (Set_Week_Task || Week__Task_Empty)
            {
                Reset_WeeklyTask_Player_QTY_Property(Profile);
                GetComponent<Task_Info>().Start_Set_Task(Profile, false); // week

                int W_Task1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_Type).GetValue();
                int W_Task2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_Type).GetValue();
                int W_Task3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_Type).GetValue();

                int QTY1 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_1_QTY).GetValue();
                int QTY2 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_2_QTY).GetValue();
                int QTY3 = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_3_QTY).GetValue();

                string Test1 = GetComponent<Task_Info>().Get_Task_Text_Info((short)W_Task1, 0, QTY1);
                string Test2 = GetComponent<Task_Info>().Get_Task_Text_Info((short)W_Task2, 0, QTY2);
                string Test3 = GetComponent<Task_Info>().Get_Task_Text_Info((short)W_Task3, 0, QTY3);

                Debug.Log("Test 2W || " + Test1 + " || " + Test2 + " || " + Test3);
            }
            #endregion

            #region Set_Shop

            bool Create_Sell = false; bool Create_Exhange = false;
            var Sell_Time = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Last_Refresh_Sell_Time);
            var Exchange_Time = Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Last_Refresh_Exchange_Time);
            int Sell_Time_Value = Sell_Time.GetValue();
            int Exchange_Time_Value = Exchange_Time.GetValue();
            Create_Sell = true; ///////////////////////////////////////// for Test
            Create_Exhange = true; ///////////////////////////////////////// for Test
            if (Sell_Time_Value == 0)
                Create_Sell = true;

            if (Exchange_Time_Value == 0)
                Create_Sell = true;

            if (Last_Login_Date != Today)
                Create_Sell = true;

            if (Last_Login_Date != Today)
                Create_Exhange = true;

            if (Create_Sell)
                Sell_Time.Set(int_Time);

            if (Create_Exhange)
                Exchange_Time.Set(int_Time);

            if (Create_Sell)
            {
                for (short i = 1; i < 9; i++)
                {
                    short[] Type_and_QTY_Price_and_Currency = GetComponent<Shop_Info>().Set_InGame_Sell_Info(i);
                    var Sell = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Sell_" + i);
                    var QTY = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_QTY_" + i);
                    var Price = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Price_" + i);
                    var Currency = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Currency_" + i);
                    var Sold_Out = GetComponent<Shop_Info>().Get_Property_by_String(Profile, "InGame_Sold_" + i);

                    Sell.Set(Type_and_QTY_Price_and_Currency[0]);
                    QTY.Set(Type_and_QTY_Price_and_Currency[1]);
                    Price.Set(Type_and_QTY_Price_and_Currency[2]);
                    Currency.Set(Type_and_QTY_Price_and_Currency[3]);
                    Sold_Out.Set(0);
                }
            }

            if (Create_Exhange)
                GetComponent<Shop_Info>().Create_Exhchange_Shop(Profile);
            #endregion

            // Check and update Any Tower Exp > 1 but level is 0 .
            GetComponent<Tower_Available>().Server_Check_Tower_Level_0_To_Level_1(Profile);

            if (!ignoreProfileMissmatchError && clientPropCount != profileExt.Profile.PropertyCount)
            {
                logger.Error(string.Format($"Client requested a profile with {clientPropCount} properties, but server " +
                                           $"constructed a profile with {profileExt.Profile.PropertyCount}. Make sure that you've changed the " +
                                           "profile factory on the ProfilesModule"));
            }

            message.Respond(profileExt.Profile.ToBytes(), ResponseStatus.Success);
        }

        /// <summary>
        /// Handles a request from game server to get a profile
        /// </summary>
        /// <param name="message"></param>
        protected virtual void GameServerProfileRequestHandler(IIncomingMessage message)
        {
            Debug.Log("GameServerProfileRequestHandler");
            if (!HasPermissionToEditProfiles(message.Peer))
            {
                message.Respond("Invalid permission level", ResponseStatus.Unauthorized);
                return;
            }

            var userId = message.AsString();

            profilesList.TryGetValue(userId, out ObservableServerProfile profile);

            if (profile == null)
            {
                message.Respond(ResponseStatus.Failed);
                return;
            }

            int Count = profile.Properties.Count;
            message.Respond(profile.ToBytes(), ResponseStatus.Success);
        }

        #endregion

        /// <summary>
        /// Gets user profile by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ObservableServerProfile GetProfileByUserId(string userId)
        {
            profilesList.TryGetValue(userId, out ObservableServerProfile profile);
            Debug.Log("profile || " + profile + " || profile.ClientPeer || " + profile.ClientPeer + " || " + profile.Properties.Count);
            return profile;
        }

        protected virtual void Get_Profile_By_UserIDHandler(IIncomingMessage message)
        {
            if (!HasPermissionToEditProfiles(message.Peer))
            {
                message.Respond("Invalid permission level", ResponseStatus.Unauthorized);
                return;
            }

            var userId = message.AsString();

            profilesList.TryGetValue(userId, out ObservableServerProfile profile);

            if (profile == null)
            {
                message.Respond(ResponseStatus.Failed);
                return;
            }

            int Count = profile.Properties.Count;
            Debug.Log("Count || " + Count);
            message.Respond(profile.ToBytes(), ResponseStatus.Success);
        }

        protected void Reset_DailyTask_Player_QTY_Property(ObservableServerProfile Profile)
        {
            Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_1).Set(0);
            Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_2).Set(0);
            Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.D_Task_Current_QTY_3).Set(0);
        }

        protected void Reset_WeeklyTask_Player_QTY_Property(ObservableServerProfile Profile)
        {
            Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_1).Set(0);
            Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_2).Set(0);
            Profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.W_Task_Current_QTY_3).Set(0);
        }

        protected virtual void Open_TreasureHandler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            short Treasure_Box_Level = room.OP_Code;
            Treasure_Box_Info info = GetComponent<Treasure_Box_Info>();
            info.Set_Current_Box_Currency_and_Price(Treasure_Box_Level, out short currency_Code, out int Require_Resource);
            bool enough = false;

            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();
            if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
            {
                if (currency_Code == (short)Shop_Code.Diamond)
                    enough = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Diamond).TryTake(Require_Resource);
                if (currency_Code == (short)Shop_Code.Token_02)
                    enough = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_02).TryTake(Require_Resource);
                if (currency_Code == (short)Shop_Code.Token_03)
                    enough = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Token_03).TryTake(Require_Resource);

                short Player_Level = (short)profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Player_Level).GetValue();
                List<Shop_Code_Packet> Reward = info.Server_Open_Treasure_Box(Treasure_Box_Level, (short)Player_Level);
                room = new RoomOptions();
                for (int i = 0; i < Reward.Count; i++)
                {
                    short shop_Code = Reward[i].Shop_Code;
                    string shop_Code_String = shop_Code.ToString();
                    if (shop_Code != 0)
                    {
                        string Property_Code = shop_Code.ToString();
                        int QTY = Reward[i].QTY;
                        short Tower_Property_Code = GetComponent<Tower_Available>().Tower_Code_To_Tower_Property_Code(shop_Code);
                        if (Tower_Property_Code != 0) // < Reward is Tower
                            Property_Code = Tower_Property_Code.ToString();

                        short code = (short)(MstProFilePropertyCode)System.Enum.Parse(typeof(MstProFilePropertyCode), Property_Code);
                        var property = profile.GetProperty<ObservableInt>(code);
                        int Before_Value = property.GetValue();
                        property.Add(QTY);
                        int After_Value = property.GetValue();

                        bool Already_in_List = room.CustomOptions.Has(shop_Code_String);
                        if (Already_in_List)
                        {
                            int old_QTY = room.CustomOptions.AsInt(shop_Code_String);
                            room.CustomOptions.Set(shop_Code_String, QTY + old_QTY);
                        }
                        if (!Already_in_List)
                            room.CustomOptions.Add(shop_Code_String, QTY);
                    }
                }

                GetComponent<Tower_Available>().Server_Check_Tower_Level_0_To_Level_1(profile);
                message.Respond(room, ResponseStatus.Success);
            }
            if (!enough)
            {
                message.Respond(ResponseStatus.Failed);
                return;
            }
        }

        protected virtual void Tower_Level_UPHandler(IIncomingMessage message)
        {
            RoomOptions room = message.Deserialize(new RoomOptions());
            short Tower_Code = room.OP_Code;

            Tower_Available TA = GetComponent<Tower_Available>();

            short Tower_Level_Property_Code = GetComponent<Tower_Available>().Tower_Code_To_Tower_Level_Property_Code(Tower_Code);
            short Tower_EXP_Property_Code = GetComponent<Tower_Available>().Tower_Code_To_Tower_Property_Code(Tower_Code);
            int Tower_Level = 0; int Tower_Exp = 0; int Gold = 0; bool Enough_EXP_Level_UP = false; bool Enough_Gold_Level_UP = false;
            var userExtension = message.Peer.GetExtension<IUserPeerExtension>();
            if (profilesList.TryGetValue(userExtension.UserId, out ObservableServerProfile profile))
            {
                Tower_Level = profile.GetProperty<ObservableInt>(Tower_Level_Property_Code).GetValue();
                Tower_Exp = profile.GetProperty<ObservableInt>(Tower_EXP_Property_Code).GetValue();
                Gold = profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Gold).GetValue();

                int Require_Exp = TA.Set_Tower_Require_EXP_Level_UP(Tower_Level + 1);
                int Require_Level_UP_Gold = TA.Get_Tower_Require_Gold_Level_UP(Tower_Level);

                if (Tower_Exp >= Require_Exp)
                    Enough_EXP_Level_UP = true;

                if (Gold >= Require_Level_UP_Gold)
                    Enough_Gold_Level_UP = true;

                if (Enough_EXP_Level_UP && Enough_Gold_Level_UP)
                {
                    profile.GetProperty<ObservableInt>((short)MstProFilePropertyCode.Gold).TryTake(Require_Level_UP_Gold);
                    profile.GetProperty<ObservableInt>(Tower_Level_Property_Code).Add(1);
                }
            }
        }
    }
}
