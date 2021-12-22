# MultiPlay_Project

* let me introduce this project

1 , Client Register / Login / forgot Password (by Master_Server_ToolKit)<br>
[AuthBehaviour.cs](MasterServerToolkit/Bridges/Shared/Scripts/Manager/AuthBehaviour.cs)

2 , Server return player profile to client (by Master_Server_ToolKit)<br>
[DemoProfilesBehaviour.cs](MasterServerToolkit/Demos/BasicProfiles/Scripts/Client/DemoProfilesBehaviour.cs)

3, Update Local player profile and player action between Master Server (e.g : buy from shop , request game , Update Inventory)<br>
[Player_Status.cs](Scrpit/Player_Status.cs)

4, Main UI (update player status . gold , exp level ... etc)<br>
[UI_Main.cs](Scrpit/UI_Main.cs)

5, Player Request PlayGame<br>
[Player_Status.Add_Player_To_Pool](Scrpit/Player_Status.cs#L3641)<br>

6. Server Add Player to Queue and FindMatch<br>
[MatchmakerModule.cs](MasterServerToolkit/MasterServer/Scripts/Modules/Matchmaker/MatchmakerModule.cs)<br>

if match found , request open new game room<br>
[Mst.Server.Connection.SendMessage((short)MstMessageCodes.MakeMatch, room);](MasterServerToolkit/MasterServer/Scripts/Modules/Matchmaker/MatchmakerModule.cs#L204)<br>

7. Set game room IP, port , ID, password and Open game room<br>
[SpawnersModule.cs](/MasterServerToolkit/MasterServer/Scripts/Modules/Spawner/SpawnersModule.cs#L337)<br>

8, Cancel player Queue , Prepare pick player to join game room to specific ip and port and player change scene <br>
[private void MakeMatchFinishHandler(IIncomingMessage message)](MasterServerToolkit/MasterServer/Scripts/Modules/Matchmaker/MatchmakerModule.cs#L208)<br>
  
9, GameMaster - GameRoom Server Side (Setup Game Map , Spawn Unit , Set Unit walk Path , Player Profile , check game end , end Game send reward to Master Server .... etc )<br>
[GameMaster.CS](Scrpit/GameMaster.cs)

10, Enemy - Game Room Server or Client<br>
Game Room Server :<br>
seach any Enemy near , attack another enemy , HP , damage , skill , etc . <br>
[TargetRpc] mean GameRoom Server call method to specific Player Client.<br>

Client :<br>
Get Unit from pool<br>
while Server order . change position unit active Effect , animation , damage bar ... etc .<br>
[Command] Player Client call method to GameRoom Server<br>
[Enemy.Cs](Scrpit/Enemy.cs)

11, Tower - Game Room Server or Client<br>
Game Room Server :<br>
Searach and Attack enemy , HP , Damage , skill . etc .<br>

Client :<br>
active attack Effect , damage bar ... etc .<br>
[Tower.Cs](Scrpit/Tower.cs)

12 Core<br>
Game Room Server : search and Attack enemy , HP0 will end game .<br>
[Core.cs](Scrpit/Core.cs)<br>

13 End Game <br>
Game Room Server check winner , battle detail and send reward to Master Server player profile .<br>
and tell player destory all game Unit , active game result UI . after play click confirm will change scene back to Main UI .  
[void Check_Player_Core_HP()](https://github.com/offerhouse/MultiPlay_Project/blob/1ef5e5875482d471b3a9a2dde270cf7cc3c66e75/Scrpit/GameMaster.cs#L320)

14 UI Swipe and change Inventory <br>
[UI_ActionDetection.cs](Scrpit/UI_ActionDetection.cs)

15 Treasure Box canvas <br>
[UI_Reward_Canvas.cs](Scrpit/UI_Reward_Canvas.cs)
