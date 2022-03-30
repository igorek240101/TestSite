import {Component, OnInit, ViewChild, Input, Output, EventEmitter} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {NgbTypeahead} from '@ng-bootstrap/ng-bootstrap';
import {Observable, Subject, merge, OperatorFunction} from 'rxjs';
import {NgbModalConfig, NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {debounceTime, distinctUntilChanged, filter, map} from 'rxjs/operators';
import {TableComponent} from '../table/table.component';

@Component({
  selector: 'app-update-worker',
  templateUrl: './update-worker.component.html',
  styleUrls: ['./update-worker.component.sass']
})
export class UpdateWorkerComponent implements OnInit {

  constructor(private http: HttpClient, config: NgbModalConfig, private modalService: NgbModal) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
  }

  deps:string[] = []

  @Input() Name:string|null = null

  @Input() birth:string|null = null

  @Input() start:string|null = null

  @Input() dep:string|null = null

  @Input() wage:string|null = null

  @Input() id:number|null = null

  public model: any;

  error:string = ""

  @Input()
  set value(model:any) {
    this.modal?.close()
    this.table?.ResetTable()
  }
  
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

  Save(){
    console.log(this.dep)
    if(this.Name == "") this.Name = null
    else {
      if(this.birth == "") this.birth = null
      else {
        if(this.start == "") this.start = null
        else {
          if(this.dep == "") this.dep = null
          else {
            if(this.wage == null || Number.parseInt(this.wage).toString() == "NaN") this.wage = null
            else {
              let par = { id: this.id,  name: this.Name, departament: this.dep, birthDate: this.birth, startWorkDate: this.start, wage: Number.parseInt(this.wage)}
              this.http.put('https://localhost:5001/Worker/UpdateWorker', par).subscribe((data:any) => this.value = data, (error) => {
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
