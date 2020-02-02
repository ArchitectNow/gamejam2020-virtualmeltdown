// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class GameState : Schema {
	[Type(0, "string")]
	public string level = "";

	[Type(1, "string")]
	public string status = "";

	[Type(2, "int8")]
	public int difficulty = 0;

	[Type(3, "array", "string")]
	public ArraySchema<string> messages = new ArraySchema<string>();

	[Type(4, "map", typeof(MapSchema<Player>))]
	public MapSchema<Player> players = new MapSchema<Player>();

	[Type(5, "array", typeof(ArraySchema<Entity>))]
	public ArraySchema<Entity> entities = new ArraySchema<Entity>();
}

