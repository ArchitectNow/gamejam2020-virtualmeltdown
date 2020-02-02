// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

import { Schema, type, ArraySchema, MapSchema, DataChange } from "@colyseus/schema";
import { Player } from "./Player"
import { Entity } from "./Entity"

export class GameState extends Schema {
    @type("string") public level: string;
    @type("string") public status: string;
    @type("int8") public difficulty: number;
    @type({ map: Player }) public players: MapSchema<Player> = new MapSchema<Player>();
    @type([ Entity ]) public entities: ArraySchema<Entity> = new ArraySchema<Entity>();

    constructor () {
        super();

        // initialization logic here.
    }

    onChange (changes: DataChange[]) {
        // onChange logic here.
    }

    onAdd () {
        // onAdd logic here.
    }

    onRemove () {
        // onRemove logic here.
    }

}
