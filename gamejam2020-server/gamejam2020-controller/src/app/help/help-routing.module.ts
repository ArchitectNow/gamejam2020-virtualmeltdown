import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HelpContainerComponent } from './help-container.component';

const routes: Routes = [
  {path: '', component: HelpContainerComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HelpRoutingModule { }
