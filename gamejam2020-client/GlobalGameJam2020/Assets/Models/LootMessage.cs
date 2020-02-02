// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class LootMessage : GameMessage {
	[Type(3, "ref", typeof(Inventory))]
	public Inventory loot = new Inventory();
}

