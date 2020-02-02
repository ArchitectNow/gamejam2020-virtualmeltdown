import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './common/containers/home/home.component';
import { ControllerGuard } from './common/guards/controller.guard';


const routes: Routes = [
  {
    path: '', component: HomeComponent,
  },
  {
    path: 'help', loadChildren: () => import('./help/help.module').then(m => m.HelpModule)
  },
  {
    path: 'join', loadChildren: () => import('./join/join.module').then(m => m.JoinModule)
  },
  {
    path: 'controller',
    canActivate: [ControllerGuard],
    loadChildren: () => import('./controller/controller.module').then(m => m.ControllerModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
