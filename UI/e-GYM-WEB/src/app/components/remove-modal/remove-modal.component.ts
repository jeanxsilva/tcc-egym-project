import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-remove-modal',
  templateUrl: './remove-modal.component.html',
  styleUrls: ['./remove-modal.component.scss']
})
export class RemoveModalComponent implements OnInit {
  public entity: any;

  constructor() { }

  ngOnInit(): void {
    console.log(this.entity)
  }

}