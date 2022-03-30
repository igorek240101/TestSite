import {Component, OnInit, EventEmitter, Output, Input} from '@angular/core';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ngbd-datepicker-popup',
  templateUrl: './datepicker-popup.html'
})
export class NgbdDatepickerPopup implements OnInit {

  @Input() model:DateModel|null = null

  ngOnInit(){

  }

  @Output() onChanged = new EventEmitter<string|null>();

  @Input()
    set value(model:string|null) {
      console.log(model)
      if(model != null){
      this.model = new DateModel
      let array = model.split(".")
      if(array.length == 3){
        this.model.day = Number.parseInt(array[0])
        this.model.month = Number.parseInt(array[1])
        this.model.year = Number.parseInt(array[2])
        this.validate();
      }
      else {
        array = model.split("-")
        if(array.length == 3){
          this.model.year = Number.parseInt(array[0])
          this.model.month = Number.parseInt(array[1])
          this.model.day = Number.parseInt(array[2])
          this.validate();
        }
      }
      console.log(this.model)
      }
  }

  validate(){
    if(this.model != null && this.model["year"] >= 1000){
      let month:string = ""
      if(this.model["month"] < 10) month = "0"
      month += this.model["month"]
      let day:string = ""
      if(this.model["day"] < 10) day = "0"
      day += this.model["day"]
      let d: string = this.model["year"] + "-" + month + "-" + day
      this.onChanged.emit(d);
    }
    else this.onChanged.emit(null);
  }
}

export class DateModel{
  public day:number = 0
  public month:number = 0
  public year:number = 0
}