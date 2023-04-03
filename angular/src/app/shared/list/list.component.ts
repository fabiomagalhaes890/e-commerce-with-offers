import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {

  @Input() public items: Array<any> = []
  @Input() public buttonName: string = "";
  @Output() public sendItem = new EventEmitter()

  public GetSelected(event: any) {
    this.sendItem.emit(event);
  }
}
