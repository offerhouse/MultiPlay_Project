using MasterServerToolkit.Games;
using MasterServerToolkit.MasterServer;
using MasterServerToolkit.Networking;
using MasterServerToolkit.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace MasterServerToolkit.Examples.BasicProfile
{
    public class DemoProfilesBehaviour : ProfilesBehaviour
    {
        public event EventHandler ClickUpdate;
        private ProfileView profileView;
        private ProfileSettingsView profileSettingsView;

        public event Action<short, IObservableProperty> OnPropertyUpdatedEvent;
        public event Action Update_Add_Property_Finish_Event;
        public UnityEvent OnProfileSavedEvent;
        public UnityEvent Load_Profile_FinishEvent;

        protected override void OnInitialize()
        {
            //profileView = ViewsManager.GetView<ProfileView>("ProfileView");
            //profileSettingsView = ViewsManager.GetView<ProfileSettingsView>("ProfileSettingsView");

            Profile = GetComponent<Account_Info>().Request_New_Profile(null, null);
            Profile.OnPropertyUpdatedEvent += OnPropertyUpdatedEventHandler;
            Profile.Update_Add_Property_Finish_Event += Load_Profile_Finish;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Profile.OnPropertyUpdatedEvent -= OnPropertyUpdatedEventHandler;
            Profile.Update_Add_Property_Finish_Event -= Load_Profile_Finish;
        }

        private void OnPropertyUpdatedEventHandler(short key, IObservableProperty property)
        {
            OnPropertyUpdatedEvent?.Invoke(key, property);
        }

        public void UpdateProfile()
        {
            Debug.Log("UpdateProfile");
            Mst.Events.Invoke(MstEventKeys.showLoadingInfo, "Saving profile data... Please wait!");

            MstTimer.WaitForSeconds(1f, () =>
            {
                var data = new Dictionary<string, string>
                {
                    { "displayName", profileSettingsView.DisplayName },
                    { "avatarUrl", profileSettingsView.AvatarUrl }
                };

                Connection.SendMessage((short)MstMessageCodes.UpdateDisplayNameRequest, data.ToBytes(), OnSaveProfileResponseCallback);
            });
        }

        private void OnSaveProfileResponseCallback(ResponseStatus status, IIncomingMessage response)
        {
            Debug.Log("OnSaveProfileResponseCallback");
            Mst.Events.Invoke(MstEventKeys.hideLoadingInfo);

            if (status == ResponseStatus.Success)
            {
                OnProfileSavedEvent?.Invoke();

                logger.Debug("Your profile is successfuly updated and saved");
            }
            else
            {
                Mst.Events.Invoke(MstEventKeys.showOkDialogBox, new OkDialogBoxEventMessage(response.AsString()));
                logger.Error(response.AsString());
            }
        }

        public void Test_Gold()
        {
            float Gold = Profile.GetProperty<ObservableFloat>((short)MstProFilePropertyCode.Gold).GetValue();

            ClickUpdate?.Invoke(this, EventArgs.Empty); // You could make custom eventargs class
            var LevelProperty = Profile.GetProperty<ObservableFloat>((short)MstProFilePropertyCode.Gold);
            LevelProperty.Add(1f);
        }

        public void Load_Profile_Finish()
        {
            Debug.Log("Local || Load_Profile_Finish || " + gameObject.name);
            bool same_Property = false;
            ObservableProfile Local_profile = Profile;
            ObservableProfile server_profile = GetComponent<Account_Info>().Request_New_Profile("A9527", null);

            // Get Server Side Profile
            Mst.Client.Connection.SendMessage((short)MstMessageCodes.Client_Get_Profile, (status, response) =>
            {
                if (status == ResponseStatus.Success)
                {
                    server_profile.FromBytes(response.AsBytes());
                    // Compare local Property with server Property
                    same_Property = GetComponent<Account_Info>().Compare_Server_and_Local_Profile(Local_profile, server_profile);
                    if (!same_Property)
                        Debug.LogWarning("same_Property || " + same_Property);
                    if (same_Property)
                    {
                        Load_Profile_FinishEvent?.Invoke(); // Tell PlayerStatus Update Local Profile
                    }
                }
            });
        }
    }
}
