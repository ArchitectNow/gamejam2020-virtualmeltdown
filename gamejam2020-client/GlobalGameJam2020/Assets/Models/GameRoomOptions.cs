// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class GameRoomOptions : Schema {
	[Type(0, "int8")]
	public int maxClients = 0;

	[Type(1, "boolean")]
	public bool isHost = false;
}

