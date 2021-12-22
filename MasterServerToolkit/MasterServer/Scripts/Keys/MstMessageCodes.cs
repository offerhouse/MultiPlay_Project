namespace MasterServerToolkit.MasterServer
{
    public enum MstMessageCodes
    {
        // Standard error code
        Error = 31000,

        // Ping request code
        Ping,

        MstStart,

        // Security
        AesKeyRequest,
        PermissionLevelRequest,
        PeerGuidRequest,

        // Rooms
        RegisterRoomRequest,
        DestroyRoomRequest,
        SaveRoomOptionsRequest,
        GetRoomAccessRequest,
        ProvideRoomAccessCheck,
        ValidateRoomAccessRequest,
        PlayerLeftRoomRequest,

        // Spawner
        RegisterSpawner,
        SpawnProcessRequest,
        ClientsSpawnRequest,
        SpawnRequestStatusChange,
        RegisterSpawnedProcess,
        CompleteSpawnProcess,
        KillProcessRequest,
        ProcessStarted,
        ProcessKilled,
        AbortSpawnRequest,
        GetSpawnFinalizationData,
        UpdateSpawnerProcessesCount,

        // Matchmaker
        GetGameRequest,
        FindGamesRequest,
        GetRegionsRequest,

        // Auth
        SignIn,
        SignUp,
        SignOutRequest,
        GetPasswordResetCode,
        GetEmailConfirmationCode,
        ConfirmEmail,
        GetLoggedInUsersCount,
        ChangePassword,
        GetPeerAccountInfo,
        UpdateAccountInfo,

        // Chat
        PickUsername,
        JoinChannel,
        LeaveChannel,
        GetCurrentChannels,
        ChatMessage,
        GetUsersInChannel,
        UserJoinedChannel,
        UserLeftChannel,
        SetDefaultChannel,

        // TODO cleanup
        // Lobbies
        JoinLobby,
        LeaveLobby,
        CreateLobby,
        LobbyInfo,
        SetLobbyProperties,
        SetMyProperties,
        SetLobbyAsReady,
        StartLobbyGame,
        LobbyChatMessage,
        SendMessageToLobbyChat,
        JoinLobbyTeam,
        LobbyGameAccessRequest,
        LobbyIsInLobby,
        LobbyMasterChange,
        LobbyStateChange,
        LobbyStatusTextChange,
        LobbyMemberPropertySet,
        LeftLobby,
        LobbyPropertyChanged,
        LobbyMemberJoined,
        LobbyMemberLeft,
        LobbyMemberChangedTeam,
        LobbyMemberReadyStatusChange,
        LobbyMemberPropertyChanged,
        GetLobbyRoomAccess,
        GetLobbyMemberData,
        GetLobbyInfo,

        // Profiles
        ClientProfileRequest,
        ServerProfileRequest,
        UpdateServerProfile,
        UpdateClientProfile,
        UpdateDisplayNameRequest,

        // Notifications
        SubscribeToNotifications,
        UnsubscribeFromNotifications,
        Notification,

        // Customs
        Check_Player_In_Game,
        Queue1v1,
        Queue2v2,
        Queue2op,
        Queue4op,
        Local_Room_ID,
        Local_Join_Game_1v1,
        Local_Join_Game_2v2,
        Local_Join_Game_2op,
        Local_Join_Game_4op,
        Local_Rejoin_Game,
        MakeMatch,
        MakeMatchFinish,
        Room_ID,
        Destroy_Room,
        Player_Left_Room,
        Get_Profile_By_UserID,
        Get_Room_Player_List,
        Get_Player_Number_By_UserID,
        Update_Desk,
        Click_Shop_Button,
        Refresh_Exchange,
        End_Game_Reward,
        Client_Get_Profile,
        Open_Treasure,
        Tower_Level_UP,

        // Test
        Mirror_To_Master,
        Master_To_Mirror,

        Test_Desk_Guest,
        LogOut_Check_DB

    }
}
