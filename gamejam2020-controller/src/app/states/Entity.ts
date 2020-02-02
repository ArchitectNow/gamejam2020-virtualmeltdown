// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.24
// 

import { Schema, type, ArraySchema, MapSchema, DataChange } from "@colyseus/schema";


export class Entity extends Schema {
    @type("float32") public x: number;
    @type("float32") public y: number;
    @type("string") public type: string;
    @type("int8") public health: number;
    @type("boolean") public isHealthy: boolean;
    @type("int8") public power: number;

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
