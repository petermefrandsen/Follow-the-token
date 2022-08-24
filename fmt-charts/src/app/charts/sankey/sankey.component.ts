import { Component, OnInit } from '@angular/core';
import { Plotly } from 'angular-plotly.js/lib/plotly.interface';

@Component({
  selector: 'app-sankey',
  templateUrl: './sankey.component.html',
  styleUrls: ['./sankey.component.css']
})
export class SankeyComponent implements OnInit {
  data = [
    { x: [1, 2, 3], y: [2, 6, 3], type: 'scatter', mode: 'lines+points', marker: {color: 'red'} },
    { x: [1, 2, 3], y: [2, 5, 3], type: 'bar' },
  ];
  layout = {width: 320, height: 240, title: 'A Fancy Plot'};

  constructor() { }

  ngOnInit(): void {
  }

}
