// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

using Colyseus.Schema;

public class Entity : Schema {
	[Type(0, "float32")]
	public float x = 0;

	[Type(1, "float32")]
	public float y = 0;

	[Type(2, "string")]
	public string type = "";

	[Type(3, "int8")]
	public int health = 0;

	[Type(4, "boolean")]
	public bool isHealthy = false;

	[Type(5, "int8")]
	public int power = 0;
}

