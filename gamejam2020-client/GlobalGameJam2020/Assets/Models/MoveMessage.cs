// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class MoveMessage : GameMessage {
	[Type(3, "ref", typeof(Vector))]
	public Vector vector = new Vector();

	[Type(4, "number")]
	public float timestamp = 0;
}

