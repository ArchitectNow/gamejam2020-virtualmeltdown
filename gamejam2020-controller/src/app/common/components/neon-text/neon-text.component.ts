import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-neon-text',
  template: `
    <div class="neon">
      <span class="text" [attr.data-text]="text">{{text}}</span>
      <span class="gradient"></span>
      <span class="spotlight"></span>
    </div>
  `,
  styles: [
      `
      .neon {
        position: relative;
        overflow: hidden;
        filter: brightness(200%);
      }

      .text {
        background-color: black;
        color: white;
        font-size: 3rem;
        font-weight: bold;
        text-transform: uppercase;
        position: relative;
        user-select: none;
      }

      .text::before {
        content: attr(data-text);
        position: absolute;
        color: white;
        filter: blur(0.02em);
        mix-blend-mode: difference;
      }

      .gradient {
        position: absolute;
        background: linear-gradient(45deg, red, aquamarine, gold, aquamarine, red);
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        mix-blend-mode: multiply;
      }

      .spotlight {
        position: absolute;
        top: -100%;
        left: -100%;
        right: 0;
        bottom: 0;
        background: radial-gradient(
          circle,
          white,
          transparent 25%
        ) center / 25% 25%,
        radial-gradient(
          circle,
          white,
          black 25%
        ) center / 12.5% 12.5%;
        animation: light 5s linear infinite;
        mix-blend-mode: color-dodge;
      }

      @keyframes light {
        to {
          transform: translate(50%, 50%);
        }
      }

    `
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NeonTextComponent implements OnInit {
  @Input() text: string;

  constructor() {
  }

  ngOnInit(): void {
  }

}
