import {Component, OnInit, ChangeDetectionStrategy, Input} from '@angular/core';

@Component({
  selector: 'player-info-container',
  template: `
    <div class="d-flex flex-direction-row player-container">
      <img [src]="robotGifMap[playerType]" alt="" class="spider-anim">
      <div class="player-text-container">
        <h3 class="player-text">{{playerName}}</h3>
        <p class="player-text">{{playerMessage}}</p>
      </div>
    </div>
  `,
  styles: [
    `.spider-anim {
        border: 1px solid #40ebee;
  }
    .player-text {
      color: #40ebee;
      padding-top: 0.2rem;
      padding-left: .5rem;
    }
    .player-container {
      margin: .5rem;
    }
      .player-text-container {
        width: 500px;
      }
    `
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlayerInfoContainerComponent implements OnInit {
  @Input() playerName: string;
  @Input() playerType: string;
  @Input() playerMessage: string;

  robotGifMap = {
    a: "assets/gif/cubot.gif",
    b: "assets/gif/spiderbot.gif",
    c: "assets/gif/spherebot.gif"
  };

  constructor() { }

  ngOnInit(): void {
  }

}
