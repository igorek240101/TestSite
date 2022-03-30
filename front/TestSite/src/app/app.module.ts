import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule }   from '@angular/common/http';
import { TableComponent } from './components/table/table.component';
import { MainPageComponent } from './component/main-page/main-page.component';
import { RouterModule, Routes } from '@angular/router';
import { NewWorkerComponent } from './components/new-worker/new-worker.component';
import { NgbdDatepickerPopup } from './datepicker-popup';
import { UpdateWorkerComponent } from './components/update-worker/update-worker.component';
import { DeleteWorkerComponent } from './components/delete-worker/delete-worker.component';

const routes: Routes = [
  {path:'', component:MainPageComponent},
  {path:'workers', component:TableComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    TableComponent,
    MainPageComponent,
    NewWorkerComponent,
    NgbdDatepickerPopup,
    UpdateWorkerComponent,
    DeleteWorkerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent, TableComponent]
})
export class AppModule { }
