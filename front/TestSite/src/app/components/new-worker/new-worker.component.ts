import {Component, OnInit, ViewChild, Input, Output, EventEmitter} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {NgbTypeahead} from '@ng-bootstrap/ng-bootstrap';
import {Observable, Subject, merge, OperatorFunction} from 'rxjs';
import {NgbModalConfig, NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {debounceTime, distinctUntilChanged, filter, map} from 'rxjs/operators';
import {TableComponent} from '../table/table.component';

@Component({
  selector: 'new-worker',
  templateUrl: 'new-worker.component.html',
  providers: [NgbModalConfig, NgbModal]
})
export class NewWorkerComponent implements OnInit  {


  constructor(private http: HttpClient, config: NgbModalConfig, private modalService: NgbModal) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
  }

  deps:string[] = []

  Name:string = ""

  birth:string|null = ""

  start:string|null = ""

  dep:string = ""

  wage:string = ""

  public model: any;

  error:string = ""
  
  dateChanged(model:string|null, pickerNum:number){
    if(pickerNum < 1){
      this.birth = model
    }
    else{
      this.start = model
    }
  }
  
  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  search: OperatorFunction<string, readonly string[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$).pipe(
      map(term => (term === '' ? this.deps
        : this.deps.filter(v => v.toLowerCase().indexOf(term.toLowerCase()) > -1)).slice(0, 10))
    );
  }

  @Input()
  set value(model:any) {
    this.modal?.close()
    this.table?.ResetTable()
  }

  Save(){
    if(this.Name == "") this.error = "Пусто значение ФИО"
    else {
      if(this.birth == null || this.birth == "") this.error = "Некорректная дата рождения"
      else {
        if(this.start == null  || this.start == "") this.error = "Некорректная дата устройства на работу"
        else {
          if(this.dep == "") this.error = "Выбирите отдел"
          else {
            if(Number.parseInt(this.wage).toString() == "NaN") this.error = "Зараплата должна быть числом"
            else {
              let par = {  name: this.Name, departament: this.dep, birthDate: this.birth, startWorkDate: this.start, wage: Number.parseInt(this.wage)}
              this.http.post('https://localhost:5001/Worker/NewWorkerAsync', par).subscribe((data:any) => this.value = data, (error) => {
                this.error = error.error
              });;
            }
          }
        }
      }
    }
  }

  @Output() AddWorker = new EventEmitter();

  @Input() modal:NgbModalRef|null = null

  @Input() table:TableComponent|null = null

  ngOnInit(): void {
    this.http.get('https://localhost:5001/Department/GetDepartments').subscribe((data:any) => this.deps = data["departments"]);
  }

}