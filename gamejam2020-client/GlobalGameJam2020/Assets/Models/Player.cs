// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class Player : Schema {
	[Type(0, "string")]
	public string id = "";

	[Type(1, "string")]
	public string name = "";

	[Type(2, "string")]
	public string status = "";

	[Type(3, "string")]
	public string type = "";

	[Type(4, "float32")]
	public float horizontal = 0;

	[Type(5, "float32")]
	public float vertical = 0;

	[Type(6, "int8")]
	public int speed = 0;

	[Type(7, "ref", typeof(Inventory))]
	public Inventory inventory = new Inventory();

	[Type(8, "ref", typeof(Inventory))]
	public Inventory inventoryLimit = new Inventory();

	[Type(9, "string")]
	public string inRange = "";

	[Type(10, "number")]
	public float moveTimestamp = 0;
}

