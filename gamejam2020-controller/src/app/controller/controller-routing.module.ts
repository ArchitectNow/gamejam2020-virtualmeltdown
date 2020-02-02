import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControllerContainerComponent } from './controller-container.component';

const routes: Routes = [
  { path: '', component: ControllerContainerComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ControllerRoutingModule {
}
