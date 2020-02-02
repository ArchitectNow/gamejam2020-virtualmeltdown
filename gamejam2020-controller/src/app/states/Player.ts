// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

import { Schema, type, ArraySchema, MapSchema, DataChange } from "@colyseus/schema";
import { Inventory } from "./Inventory"

export class Player extends Schema {
    @type("string") public id: string;
    @type("string") public name: string;
    @type("string") public status: string;
    @type("string") public type: string;
    @type("float32") public horizontal: number;
    @type("float32") public vertical: number;
    @type("int8") public speed: number;
    @type(Inventory) public inventory: Inventory = new Inventory();
    @type(Inventory) public inventoryLimit: Inventory = new Inventory();
    @type("string") public inRange: string;

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
