import { Client, Room } from 'colyseus';
import { GameMessage, GameRoomAuthOptions, GameRoomOptions, GameState } from './game.state';
import { GameMessageService } from './services/game-message.service';
import { PlayerService } from './services/player.service';
import { RoomMessageService } from './services/room-message.service';

export class GameRoom extends Room<GameState> {
  private readonly robotTypes: { label: string, value: string }[] = [
    { label: 'Cube Bot', value: 'a' },
    { label: 'Spidey Bot', value: 'b' },
    { label: 'Sphere Bot', value: 'c' },
  ];

  private readonly playerService: PlayerService = new PlayerService();
  private readonly messageService: GameMessageService = new GameMessageService();
  private readonly roomMessageService: RoomMessageService = new RoomMessageService();
  private readonly roomIdMaps: Map<string, string> = new Map<string, string>();

  onMessage(client: Client, message: GameMessage) {
    const player = this.state.players[message.playerId];

    if (player == null) {
      // TODO: handle no player found
      return;
    }

    if (this.messageService.isMoveMessage(message)) {
      this.playerService.move(player, message.vector, message.timestamp);
      return;
    }

    if (this.messageService.isStunMessage(message)) {
      const { targetId } = message;
      const target = this.state.players[targetId];
      this.playerService.stun(player, target);
      return;
    }

    if (this.messageService.isLootMessage(message)) {
      const { loot } = message;
      this.playerService.pickUpLoot(player, loot);
      return;
    }

    if (this.messageService.isInRangeMessage(message)) {
      const { color } = message;
      this.playerService.toggleInRange(player, color);
      return;
    }

    if (this.messageService.isOutOfRangeMessage(message)) {
      this.playerService.toggleInRange(player);
      return;
    }

    if (this.messageService.isDepositMessage(message)) {
      if (!this.clients[0]) {
        return;
      }

      const { color } = message;
      this.playerService.deposit(player, color);
      this.state.messages.push(this.roomMessageService.createDepositMessage(player.name, color));
      this.send(this.clients[0], message);
      return;
    }
  }

  onCreate(options: GameRoomOptions) {
    this.maxClients = options.maxClients;
    const randomDigits = GameRoom.generateRoomId();
    const roomId = GameRoom.appendRoomId(randomDigits);
    !this.roomIdMaps.has(randomDigits) && this.roomIdMaps.set(randomDigits, roomId);
    this.roomId = roomId;
    this.setState(new GameState(roomId));
  }

  onAuth(
    client: Client,
    options: GameRoomAuthOptions
  ) {
    const existedPlayer = Object.values(this.state.players).find(player => player.name === options.username);

    if (existedPlayer != null) {
      throw new Error(`${ options.username } already exists`);
    }

    return client;
  }

  onJoin(
    client: Client,
    options?: GameRoomAuthOptions | GameRoomOptions
  ) {
    if ((options as GameRoomOptions).isHost != null) {
      return;
    }
    const { username, type } = options as GameRoomAuthOptions;
    this.state.players[client.sessionId] = this.playerService.create(type, username, client.sessionId);
    this.state.messages.push(this.roomMessageService.createJoinMessage(username, this.robotTypes[type]));
  }

  onLeave(
    client: Client,
    consented?: boolean
  ) {
    if (consented) {
      const playerName = this.state.players[client.sessionId].name;
      this.state.messages.push(this.roomMessageService.createLeaveMessage(playerName));
      delete this.state.players[client.sessionId];
    }
  }

  private static getPlayerId(sessionId: string, username: string): string {
    return sessionId + '_' + username;
  }

  private static generateRoomId(): string {
    return (Math.floor(Math.random() * (9999 - 1)) + 1).toString();
  }

  private static appendRoomId(rawRoomId: string) {
    let result: string = rawRoomId;
    while (result.length < 9) {
      if (result.length < 4) {
        result = '0' + result;
      } else {
        result = 'a' + result;
      }
    }
    return result;
  }
}
