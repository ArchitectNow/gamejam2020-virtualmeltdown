import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { DropdownControlModule } from '../common/components/dropdown-control/dropdown-control.module';
import { JoinContainerComponent } from './join-container.component';

import { JoinRoutingModule } from './join-routing.module';

@NgModule({
  declarations: [JoinContainerComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    JoinRoutingModule,
    DropdownControlModule,
    NgbModalModule
  ]
})
export class JoinModule {
}
