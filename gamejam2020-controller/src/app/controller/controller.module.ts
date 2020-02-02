import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ControllerRoutingModule } from './controller-routing.module';
import { ControllerContainerComponent } from './controller-container.component';
import { JoystickDirective } from './directives/joystick.directive';
import { ItemButtonComponent } from './components/item-button.component';
import { InfoButtonSvgComponent } from './components/svg/info-button-svg/info-button-svg.component';
import { PlayerInfoContainerComponent } from './components/svg/player-info-container/player-info-container.component';


@NgModule({
  declarations: [ControllerContainerComponent, JoystickDirective, ItemButtonComponent, InfoButtonSvgComponent, PlayerInfoContainerComponent],
  imports: [
    CommonModule,
    ControllerRoutingModule
  ]
})
export class ControllerModule { }
