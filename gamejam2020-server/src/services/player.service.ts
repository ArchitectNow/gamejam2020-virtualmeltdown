import { Inventory, Player, PlayerStatus, RobotType, Vector } from '../game.state';

export class PlayerService {
  create(type: RobotType, username: string, id: string): Player {
    return new Player(type, username, id);
  }

  move(player: Player, vector: Vector, timestamp: number): void {
    player.vertical = vector.x;
    player.horizontal = vector.y;
    player.moveTimestamp = timestamp;
  }

  stun(player: Player, target: Player): void {
    target.status = PlayerStatus.Stunned;
  }

  pickUpLoot(player: Player, loot: Inventory) {
    if (!player.hasLimitReached('red')) {
      player.inventory.red += Math.min(loot.red, player.itemDifference('red'));
    }

    if (!player.hasLimitReached('blue')) {
      player.inventory.blue += Math.min(loot.blue, player.itemDifference('blue'));
    }

    if (!player.hasLimitReached('yellow')) {
      player.inventory.yellow += Math.min(loot.yellow, player.itemDifference('yellow'));
    }

    if (!player.hasLimitReached('green')) {
      player.inventory.green += Math.min(loot.green, player.itemDifference('green'));
    }
  }

  toggleInRange(player: Player, color?: string) {
    player.inRange = color || '';
    // console.log('player: ' + player.name + 'is in range of ' + player.inRange);
  }

  deposit(player: Player, color: string) {
    player.inventory[color] -= 1;
  }
}
