import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NeonTextComponent } from './neon-text.component';

@NgModule({
  declarations: [NeonTextComponent],
  imports: [
    CommonModule
  ],
  exports: [NeonTextComponent]
})
export class NeonTextModule {
}
