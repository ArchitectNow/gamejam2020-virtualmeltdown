import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { ColyseusClientService } from '../common/services/colyseus-client.service';
import { Player } from '../states/Player';

@Component({
  selector: 'app-controller-container',
  template: `
    <div class="container-fluid row h-100 w-100 root-background" *ngIf="player$ | async as player">
      <player-info-container class="col-10"
                             [playerType]="player.type"
                             [playerName]="player.name"
                             [playerMessage]="playerMessage"></player-info-container>
      <div class="col-2 position-relative">
        <button class="exit-button" (click)="onClickExit()">EXIT</button>
      </div>
      <div class="col-4 position-relative" appJoystick (move)="onMove($event)"></div>
      <div class="col-8 position-relative">
        <div class="row m-0 p-0">
          <app-info-button-svg *ngFor="let color of colors"
                               class="col-6"
                               (deposit)="onDeposit(color.value)"
                               [color]="color.value"
                               [inRange]="player.inRange"
                               [imgSrc]="color.image"
                               [title]="color.label"
                               [item]="player.inventory[color.value]"
                               [itemLimit]="player.inventoryLimit[color.value]"></app-info-button-svg>
        </div>
      </div>
    </div>
  `,
  styles: [
      `.root-background {
      background-color: black;
      background-image: url("assets/svg/backPattern.svg");
      background-position: center;
      background-size: cover;
      margin: 0;
      padding: 0;
    }

    .exit-button {
      background-color: black;
      color: #40ebee;
      border: 1px solid #40ebee;
      position: absolute;
      top: 20px;
      right: 20px;
    }
    `
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ControllerContainerComponent implements OnInit, OnDestroy {
  playerMessage = 'Sweet apple pie oat cake fruitcake sesame snaps wafer. Pudding cotton candy powder cotton candy';
  player$: Observable<Player>;
  colors = [
    { label: 'Red', value: 'red', image: 'assets/jpg/redPickup.jpg' },
    { label: 'Green', value: 'green', image: 'assets/jpg/greenPickup.jpg' },
    { label: 'Blue', value: 'blue', image: 'assets/jpg/bluePickup.jpg' },
    { label: 'Yellow', value: 'yellow', image: 'assets/jpg/yellowPickup.jpg' },
  ];

  constructor(private readonly colyseusClientService: ColyseusClientService, private readonly router: Router) {
  }

  ngOnInit(): void {
    this.player$ = this.colyseusClientService.player$();
    if (window) {
      window.onbeforeunload = this.ngOnDestroy.bind(this);
    }

    this.colyseusClientService.connectionClose$().pipe(take(1)).subscribe(() => {
      this.router.navigate(['/join']);
    });
  }

  onMove(vector: { x: number; y: number }) {
    this.colyseusClientService.move(vector);
  }

  onClickExit() {
    this.router.navigate(['/join']);
  }

  ngOnDestroy(): void {
    this.colyseusClientService.disconnect();
  }

  onDeposit(color: string) {
    this.colyseusClientService.deposit(color);
  }
}
