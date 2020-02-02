import {
  AfterViewInit,
  Directive,
  ElementRef,
  EventEmitter,
  NgZone,
  OnDestroy,
  Output,
  Renderer2
} from '@angular/core';
import * as nipplejs from 'nipplejs';

@Directive({
  selector: '[appJoystick]'
})
export class JoystickDirective implements AfterViewInit, OnDestroy {
  private _manager: nipplejs.JoystickManager;
  @Output() move: EventEmitter<{ x: number, y: number }> = new EventEmitter<{ x: number, y: number }>();

  constructor(
    private readonly el: ElementRef<HTMLElement>,
    private readonly ngZone: NgZone,
    private readonly renderer: Renderer2
  ) {

  }

  ngAfterViewInit(): void {
    this.ngZone.runOutsideAngular(() => {
      this._manager = nipplejs.create({
        zone: this.el.nativeElement,
        mode: 'static',
        position: {
          bottom: '115px',
          left: '115px'
        },
        fadeTime: 200,
        maxNumberOfNipples: 1,
        color: 'rgba(0,0,0,0)',
        multitouch: false,
        size: 225,
        restOpacity: 1,
      });
      this.ngZone.run(this._registerJoystickListener.bind(this));
    });
  }

  private _registerJoystickListener() {
    const joystick = this._manager[0];
    if (joystick) {
      this._setJoystickStyle(joystick);
      joystick.on('move', (evt1, data) => {
        this.move.emit({ ...data.vector });
      });
    }
  }

  ngOnDestroy(): void {
    this._manager && this._manager.destroy();
  }

  private _setJoystickStyle(joystick: nipplejs.Joystick) {
    const { ui: { back, front } } = joystick;
    if (back && front) {
      this.renderer.setStyle(back, 'background-color', 'rgba(0,0,0,0)');
      this.renderer.setStyle(back,
        'background-image',
        'url(assets/svg/thumbStickBack.svg)');
      this.renderer.setStyle(back, 'background-size', '100% 100%');

      this.renderer.setStyle(front,
        'background',
        'url(assets/svg/thumbStickFront.svg)');
      this.renderer.setStyle(front, 'box-shadow', 'rgba(0, 0, 0, 1) 1px 3px 7px 3px');
    }
  }
}
