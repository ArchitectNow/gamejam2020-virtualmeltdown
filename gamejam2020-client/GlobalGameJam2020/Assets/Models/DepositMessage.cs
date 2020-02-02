// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class DepositMessage : GameMessage {
	[Type(3, "string")]
	public string color = "";

	[Type(4, "int8")]
	public int payload = 0;
}

