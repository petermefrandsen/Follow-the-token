import { BrowserModule } from '@angular/platform-browser';
import { NgModule, DoBootstrap, Injector, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { SankeyComponent } from './charts/sankey/sankey.component';
import * as PlotlyJS from 'plotly.js-dist-min';
import { PlotlyModule } from 'angular-plotly.js';

PlotlyModule.plotlyjs = PlotlyJS;

@NgModule({
  declarations: [
    SankeyComponent
  ],
  imports: [
    BrowserModule,
    PlotlyModule
  ],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule implements DoBootstrap {

  constructor(private injector: Injector) {}

  ngDoBootstrap() {
    const elements = [{componet: SankeyComponent, name: 'fmt-sankey'}];

    for(const el of elements) {
      const element = createCustomElement(el.componet, {
        injector: this.injector
      });

      customElements.define(el.name, element);
    }
  }
 }
