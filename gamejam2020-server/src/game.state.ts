import { ArraySchema, MapSchema, Schema, type } from '@colyseus/schema';

export enum PlayerStatus {
  Normal = 'normal',
  Stunned = 'stunned'
}

export enum RobotType {
  A = 'a',
  B = 'b',
  C = 'c',
  D = 'd'
}

export class Inventory extends Schema {
  @type('int8')
  red: number;
  @type('int8')
  blue: number;
  @type('int8')
  green: number;
  @type('int8')
  yellow: number;

  constructor(red: number = 0, blue: number = 0, green: number = 0, yellow: number = 0) {
    super();
    this.red = red;
    this.blue = blue;
    this.green = green;
    this.yellow = yellow;
  }
}

export class Player extends Schema {
  @type('string')
  id: string;
  @type('string')
  name: string;
  @type('string')
  status: PlayerStatus;
  @type('string')
  type: RobotType;
  @type('float32')
  horizontal: number;
  @type('float32')
  vertical: number;
  @type('int8')
  speed: number;
  @type(Inventory)
  inventory: Inventory;
  @type(Inventory)
  inventoryLimit: Inventory;
  @type('string')
  inRange: string;
  @type('number')
  moveTimestamp: number;

  constructor(type: RobotType, name: string, id: string) {
    super();
    this.type = type;
    switch (type) {
      case RobotType.A:
        this.speed = 10;
        this.inventoryLimit = new Inventory(5, 3, 3, 3);
        break;
      case RobotType.B:
        this.speed = 15;
        this.inventoryLimit = new Inventory(5, 5, 5, 5);
        break;
      case RobotType.C:
        this.speed = 8;
        this.inventoryLimit = new Inventory(2, 2, 2, 3);
        break;
      case RobotType.D:
        this.speed = 6;
        this.inventoryLimit = new Inventory(4, 4, 5, 5);
        break;
    }
    this.id = id;
    this.name = name;
    this.status = PlayerStatus.Normal;
    this.inventory = new Inventory();
    this.inRange = '';
    this.moveTimestamp = Date.now();
  }

  hasLimitReached(type: 'red' | 'green' | 'blue' | 'yellow'): boolean {
    return this.inventory[type] === this.inventoryLimit[type];
  }

  itemDifference(type: 'red' | 'green' | 'blue' | 'yellow'): number {
    return this.inventoryLimit[type] - this.inventory[type];
  }
}

export class Entity extends Schema {
  @type('float32')
  x: number;
  @type('float32')
  y: number;
  @type('string')
  type: any;
  @type('int8')
  health: number;
  @type('boolean')
  isHealthy: boolean;
  @type('int8')
  power: number;
}

export enum GameStatus {
  Wait = 'wait',
  Start = 'start'
}

export class GameState extends Schema {
  @type('string')
  level: string;
  @type('string')
  status: GameStatus;
  @type('int8')
  difficulty: number;
  @type(['string'])
  messages: ArraySchema<string>;
  @type({ map: Player })
  players: MapSchema<Player>;
  @type([Entity])
  entities: ArraySchema<Entity>;

  constructor(roomId: string) {
    super();
    this.level = roomId;
    this.status = GameStatus.Wait;
    this.players = new MapSchema<Player>();
    this.entities = new ArraySchema<Entity>();
    this.messages = new ArraySchema<string>();
  }
}

export class GameRoomOptions extends Schema {
  @type('int8')
  maxClients: number;
  @type('boolean')
  isHost: boolean;

  constructor() {
    super();
    this.maxClients = 5;
    this.isHost = true;
  }
}

export class GameRoomAuthOptions extends Schema {
  @type('string')
  username: string;
  @type('string')
  type: RobotType;
}

export enum EventType {
  Move = 'move',
  Stun = 'stun',
  Loot = 'loot',
  InRange = 'inRange',
  OutOfRange = 'outOfRange',
  Deposit = 'deposit'
}

export class GameMessage extends Schema {
  @type('string')
  eventType: EventType;
  @type('string')
  username: string;
  @type('string')
  playerId: string;
}

export class Vector extends Schema {
  @type('float32')
  x: number;
  @type('float32')
  y: number;
}

export class MoveMessage extends GameMessage {
  @type(Vector)
  vector: Vector;
  @type('number')
  timestamp: number;
}

export class StunMessage extends GameMessage {
  @type('string')
  targetId: string;
}

export class LootMessage extends GameMessage {
  @type(Inventory)
  loot: Inventory;
}

export class InRangeMessage extends GameMessage {
  @type('string')
  color: string;
}

export class OutOfRangeMessage extends GameMessage {
}

export class DepositMessage extends GameMessage {
  @type('string')
  color: string;
  @type('int8')
  payload: number;
}
