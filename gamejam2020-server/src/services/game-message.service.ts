import {
  DepositMessage,
  EventType,
  GameMessage,
  InRangeMessage,
  LootMessage,
  MoveMessage,
  OutOfRangeMessage,
  StunMessage
} from '../game.state';

export class GameMessageService {
  isMoveMessage(message: GameMessage): message is MoveMessage {
    return message.eventType === EventType.Move;
  }

  isStunMessage(message: GameMessage): message is StunMessage {
    return message.eventType === EventType.Stun;
  }

  isLootMessage(message: GameMessage): message is LootMessage {
    return message.eventType === EventType.Loot;
  }

  isInRangeMessage(message: GameMessage): message is InRangeMessage {
    return message.eventType === EventType.InRange;
  }

  isOutOfRangeMessage(message: GameMessage): message is OutOfRangeMessage {
    return message.eventType === EventType.OutOfRange;
  }

  isDepositMessage(message: GameMessage): message is DepositMessage {
    return message.eventType === EventType.Deposit;
  }

}
