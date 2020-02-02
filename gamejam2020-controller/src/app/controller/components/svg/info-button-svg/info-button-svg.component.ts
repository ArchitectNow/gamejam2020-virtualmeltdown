import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-info-button-svg',
  template: `
    <button type="button"
            class="info-button-container d-flex"
            (click)="deposit.emit()"
            [disabled]="!inRange || !item"
            [ngClass]="{glow: inRange && item}">
      <img [src]="imgSrc" alt="" class="info-button-icon">
      <div class="d-flex flex-column info-button-text">
        <span>{{title}}</span>
        <small>{{item}} / {{itemLimit}}</small>
      </div>
    </button>
  `,
  styles: [
      `
      .info-button-container {
        margin-top: 20px;
        width: 200px;
        height: 70px;
        color: #40ebee;
        border: 1px solid #40ebee;
        opacity: 1;
        background-color: black;
        transition: opacity 200ms ease-in-out;
      }

      .info-button-container:disabled {
        opacity: 0.5;
      }

      .info-button-container.glow {
        animation: neonGlow 2s infinite alternate
      }

      .info-button-icon {
        background: rgba(64, 235, 238, .2);
        border: 1px solid #40ebee;
        width: 50px;
        height: 50px;
        margin: 10px;
      }

      .info-button-text {
        margin-top: 10px;
        margin-right: 10px;
        padding: 0;
      }

      @keyframes neonGlow {
        0% {
          box-shadow: 0 0 10px rgba(255, 255, 255, .8),
          0 0 20px rgba(255, 255, 255, .8),
          0 0 22px rgba(255, 255, 255, .8),
          0 0 40px rgba(66, 220, 219, .8),
          0 0 60px rgba(66, 220, 219, .8),
          0 0 80px rgba(66, 220, 219, .5),
          0 0 100px rgba(66, 220, 219, .5),
          0 0 140px rgba(66, 220, 219, .5),
          0 0 200px rgba(66, 220, 219, .5);
        }
        100% {
          box-shadow: 0 0 2px rgba(255, 255, 255, .8),
          0 0 8px rgba(255, 255, 255, .8),
          0 0 10px rgba(255, 255, 255, .8),
          0 0 20px rgba(66, 220, 219, .8),
          0 0 30px rgba(66, 220, 219, .8),
          0 0 40px rgba(66, 220, 219, .8),
          0 0 50px rgba(66, 220, 219, .5),
          0 0 80px rgba(66, 220, 219, .5);
        }
      }
    `
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InfoButtonSvgComponent implements OnInit {
  @Input() title: string;
  @Input() imgSrc: string;
  @Input() item: number = 0;
  @Input() itemLimit: number = 0;
  @Input() inRange: string = '';
  @Input() color: string;
  @Output() deposit: EventEmitter<null> = new EventEmitter<null>();

  constructor() {
  }

  ngOnInit(): void {
  }

}
