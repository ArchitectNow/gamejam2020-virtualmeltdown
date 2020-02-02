import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as Colyseus from 'colyseus.js';
import { BehaviorSubject, Observable, of, Subject, throwError } from 'rxjs';
import { fromPromise } from 'rxjs/internal-compatibility';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { DepositMessage } from '../../states/DepositMessage';
import { GameRoomAuthOptions } from '../../states/GameRoomAuthOptions';
import { GameState } from '../../states/GameState';
import { MoveMessage } from '../../states/MoveMessage';
import { Player } from '../../states/Player';
import { ErrorDialogComponent } from '../layouts/error-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ColyseusClientService {
  private _client: Colyseus.Client;
  private _room: Colyseus.Room<GameState>;
  private _nameSubject: BehaviorSubject<string> = new BehaviorSubject<string>(sessionStorage.getItem(
    'virtualmeltdown_username') || '');
  private _playerSubject: BehaviorSubject<Player> = new BehaviorSubject<Player>(null);
  private _leaveSubject: Subject<null> = new Subject<null>();

  constructor(private readonly ngbModal: NgbModal) {
    this._client = new Colyseus.Client(environment.serverUrl);
  }

  player$(): Observable<Player> {
    return this._playerSubject.asObservable();
  }

  isConnected$(): Observable<boolean> {
    return of(this._room)
      .pipe(map(room => !!room));
  }

  username$(): Observable<string> {
    return this._nameSubject.asObservable();
  }

  connectionClose$(): Observable<null> {
    return this._leaveSubject.asObservable();
  }

  setName(name: string) {
    this._nameSubject.next(name);
    sessionStorage.setItem('virtualmeltdown_username', name);
  }

  setRoom(room: Colyseus.Room<GameState>) {
    this._room = room;
    this._room.state.players.onChange = changes => {
      if (!this._playerSubject.value || (changes.id === this._playerSubject.value.id)) {
        this._playerSubject.next(changes);
      }
    };

    this._room.onLeave(this._leaveSubject.next.bind(this._leaveSubject));
  }

  create() {
    this._client.create('game');
  }

  join(roomId: string, username: string, type: string): Observable<Colyseus.Room<GameState>> {
    this.setName(username);
    const joinOptions = new GameRoomAuthOptions();
    joinOptions.username = username;
    joinOptions.type = type;
    return fromPromise(this._client.joinById<GameState>('aaaaa'.concat(roomId), joinOptions))
      .pipe(
        catchError(err => {
          if (err instanceof ProgressEvent) {
            const ref = this.ngbModal.open(ErrorDialogComponent, { backdrop: 'static', centered: true });
            ref.componentInstance.errorMessage = 'Error joining a room. Most likely, the server is not running.';
          }
          return throwError(err);
        })
      );
  }

  move(vector: { x: number, y: number }) {
    try {
      const message = new MoveMessage();
      message.vector.x = vector.x;
      message.vector.y = vector.y;
      message.username = this._nameSubject.value;
      message.eventType = 'move';
      message.playerId = this._playerSubject.value.id;
      this._room.send(message);
    } catch (e) {
      // TODO: handle error
      return;
    }
  }

  disconnect() {
    try {
      this._playerSubject.next(null);
      this._room.leave(true);
    } catch (e) {
      // TODO: handle error
      return;
    }
  }

  stun() {
    try {

    } catch (e) {
      // TODO: handle error
      return;
    }
  }

  deposit(color: string) {
    try {
      const message = new DepositMessage();
      message.payload = 1;
      message.color = color;
      message.playerId = this._playerSubject.value.id;
      message.eventType = 'deposit';
      message.username = this._playerSubject.value.name;
      this._room.send(message);
    } catch (e) {
      // TODO: handle error
      return;
    }
  }
}
