// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class GameMessage : Schema {
	[Type(0, "string")]
	public string eventType = "";

	[Type(1, "string")]
	public string username = "";

	[Type(2, "string")]
	public string playerId = "";
}

