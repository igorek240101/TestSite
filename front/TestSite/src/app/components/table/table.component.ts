import { Component, OnInit} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { filter } from 'rxjs';
import { NgbModal, NgbModalOptions, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import { NewWorkerComponent } from '../new-worker/new-worker.component';
import { DeleteWorkerComponent } from '../delete-worker/delete-worker.component';
import { UpdateWorkerComponent } from '../update-worker/update-worker.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.sass']
})
export class TableComponent implements OnInit {

    Workers:Worker[] = []

    Departments:string[]= []

    WorkersCount:number = 0

    Page:number = 1

    PageSize:number = 0

    sort:boolean|null = null

    sortTarget:number = 0

    minWage:string = ""

    maxWage:string = ""

    minBirthModel:any = null

    minBirth:string|null = null

    maxBirth:string|null = null

    minStart:string|null = null

    maxStart:string|null = null

    departs:string[] = []

  constructor(private http: HttpClient, private modalService: NgbModal) { }

  PageSizeChange(n:number){
    this.PageSize = n
    this.ResetTable()
  }

  SortTargetChanged(n:number){
    this.sortTarget = n
    if(this.sort != null){
      this.ResetTable()
    }
  }

  SortChanged(n:boolean|null){
    this.sort = n
    this.ResetTable()
  }

  public ResetTable() {
    this.GetWorkers(1)
    this.Page = 1
  }

  GetFilter():number{
    return Number.parseInt(this.minWage)
  }

  dateChanged(model:string|null, pickerNum:number){
      if(pickerNum < 2){
        if(pickerNum < 1){
          this.minBirth = model
        }
        else{
          this.maxBirth = model
        }
      }
      else{
        if(pickerNum < 3){
          this.minStart = model
        }
        else{
          this.maxStart = model
        }
      }
  }

  DepFilterAdd(dep:string){
    if(!this.departs.includes(dep)){
      this.departs.unshift(dep)
    }
  }

  DepFilterDel(dep:string){
    for(let i = 0; i < this.departs.length; i++){
      if(this.departs[i] == dep){
        this.departs.splice(i, 1)
        return
      }
    }
  }

  GetWorkers(page:number) {
    this.http.get('https://localhost:5001/Department/GetDepartments').subscribe((data:any) => this.Departments = data["departments"]);
    this.GetPagesCount()
    let departs:string[]|null
    if(this.departs.length == 0) departs = null
    else departs = this.departs
    let par = {filter: {minWage: Number.parseInt(this.minWage), maxWage: Number.parseInt(this.maxWage), minBirth: this.minBirth, maxBirth: this.maxBirth, minStartWork: this.minStart, maxStartWork: this.maxStart, departament: departs},sort: {isSort : this.sort, sortKey : this.sortTarget}}
    this.http.put('https://localhost:5001/Worker/GetWorkers/' + page + '/' + this.PageSize, par).subscribe((data:any) => this.Workers = data["workers"]);
  }
  GetPagesCount() { 
    let departs:string[]|null
    if(this.departs.length == 0) departs = null
    else departs = this.departs
    let par = {minWage: Number.parseInt(this.minWage), maxWage: Number.parseInt(this.maxWage), minBirth: this.minBirth, maxBirth: this.maxBirth, minStartWork: this.minStart, maxStartWork: this.maxStart, departament: departs}
    this.http.put('https://localhost:5001/Worker/WorkersCount', par).subscribe((data:any) => this.WorkersCount = data);
  }

  ngOnInit(): void {
    this.PageSizeChange(10)
    this.GetWorkers(1);  
  }

  NewWorker(){
    let r : NgbModalOptions = { animation:true, keyboard:true, size : 'xl' }
    const ref = this.modalService.open(NewWorkerComponent, r) 
    ref.componentInstance.modal = ref
    ref.componentInstance.table = this
  }

  DelWorker(id:number){
    let r : NgbModalOptions = { animation:true, keyboard:true, size : 'xl' }
    const ref = this.modalService.open(DeleteWorkerComponent, r) 
    ref.componentInstance.modal = ref
    ref.componentInstance.table = this
    ref.componentInstance.id = id
  }

  UpdateWorker(worker:Worker){
    console.log(worker)
    let r : NgbModalOptions = { animation:true, keyboard:true, size : 'xl' }
    const ref = this.modalService.open(UpdateWorkerComponent, r) 
    ref.componentInstance.modal = ref
    ref.componentInstance.table = this
    ref.componentInstance.id = worker.id
    ref.componentInstance.Name = worker.name
    console.log(ref.componentInstance.Name)
    ref.componentInstance.birth = worker.birthDate
    ref.componentInstance.start = worker.startWorkDate
    ref.componentInstance.dep = worker.departament
    ref.componentInstance.wage = worker.wage.toString()
  }

}


interface Worker {
  id:number
  name:string
  wage:number
  birthDate:string
  startWorkDate:string
  departament:string
}

