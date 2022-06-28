import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompletetasksComponent } from './pages/completetasks/completetasks.component';
import { CreatetasksComponent } from './pages/createtasks/createtasks.component';
import { PendingtasksComponent } from './pages/pendingtasks/pendingtasks.component';
import { UpdatetasksComponent } from './pages/updatetasks/updatetasks.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/pendingtasks' },
  { path: 'pendingtasks',component:PendingtasksComponent},
  { path: 'createtasks',component:CreatetasksComponent},
  { path: 'completetasks',component:CompletetasksComponent},
  { path: 'updatetasks/:id',component:UpdatetasksComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
