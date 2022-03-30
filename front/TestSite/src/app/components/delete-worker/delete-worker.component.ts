import {Component, OnInit, ViewChild, Input, Output, EventEmitter} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {NgbTypeahead} from '@ng-bootstrap/ng-bootstrap';
import {Observable, Subject, merge, OperatorFunction} from 'rxjs';
import {NgbModalConfig, NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {debounceTime, distinctUntilChanged, filter, map} from 'rxjs/operators';
import {TableComponent} from '../table/table.component';

@Component({
  selector: 'app-delete-worker',
  templateUrl: './delete-worker.component.html',
  styleUrls: ['./delete-worker.component.sass']
})
export class DeleteWorkerComponent implements OnInit {


  constructor(private http: HttpClient, config: NgbModalConfig, private modalService: NgbModal) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
  }

  Yes(){
    this.http.delete('https://localhost:5001/Worker/DeleteWorker/'+this.id).subscribe((data:any) => this.value = data);;
  }

  @Input()
  set value(model:any) {
    this.modal?.close()
    this.table?.ResetTable()
  }

  No(){
    this.modal?.close()
  }

  @Output() AddWorker = new EventEmitter();

  @Input() modal:NgbModalRef|null = null

  @Input() table:TableComponent|null = null

  @Input() id:number|null = null

  ngOnInit(): void {
  }


}
