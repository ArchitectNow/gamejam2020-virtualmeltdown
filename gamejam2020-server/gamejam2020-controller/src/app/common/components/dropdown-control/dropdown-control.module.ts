import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { DropdownControlComponent } from './dropdown-control.component';

@NgModule({
  declarations: [DropdownControlComponent],
  imports: [CommonModule, ReactiveFormsModule, NgbDropdownModule],
  exports: [DropdownControlComponent]
})
export class DropdownControlModule {
}
