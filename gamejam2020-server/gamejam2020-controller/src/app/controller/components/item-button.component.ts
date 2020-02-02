import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-item-button',
  template: `
    <button class="item-btn" type="button" (click)="press.emit(label)">
      <i class="las" [ngClass]="iconClass"></i>
    </button>
    <h5 class="mb-0 ml-1">{{label}}</h5>
  `,
  styles: [
      `
      :host {
        position: absolute;
        top: 200px;
        left: 200px;
        display: flex;
        align-items: center;
      }

      .item-btn {
        display: inline-block;
        text-decoration: none;
        color: rgba(152, 152, 152, 0.43); /*IconColor*/
        width: 80px;
        height: 80px;
        line-height: 80px;
        font-size: 40px;
        border-radius: 50%;
        text-align: center;
        vertical-align: middle;
        overflow: hidden;
        font-weight: bold;
        background-image: -webkit-linear-gradient(#e8e8e8 0%, #d6d6d6 100%);
        background-image: linear-gradient(#e8e8e8 0%, #d6d6d6 100%);
        text-shadow: 1px 1px 1px rgba(255, 255, 255, 0.66);
        box-shadow: inset 0 2px 0 rgba(255, 255, 255, 0.5), 0 2px 2px rgba(0, 0, 0, 0.19);
        border-bottom: solid 2px #b5b5b5;
        outline: none;
      }

      .item-btn i.la {
        line-height: 80px;
      }

      .item-btn:active {
        background-image: -webkit-linear-gradient(#efefef 0%, #d6d6d6 100%);
        box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.5), 0 2px 2px rgba(0, 0, 0, 0.19);
        border-bottom: none;
      }
    `
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ItemButtonComponent {
  @Input() iconClass: string;
  @Input() label: string;
  @Output() press: EventEmitter<string> = new EventEmitter<string>();
}
