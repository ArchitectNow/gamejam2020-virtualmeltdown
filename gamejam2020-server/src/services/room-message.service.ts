export class RoomMessageService {
  createJoinMessage(playerName: string, type: string) {
    return `${playerName} has joined as ${type}`;
  }

  createLeaveMessage(playerName: string) {
    return `${playerName} has left`;
  }

  createDepositMessage(name: string, color: string) {
    return `${name} made a deposit to ${color}`;
  }
}
