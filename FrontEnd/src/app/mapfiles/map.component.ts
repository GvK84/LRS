import { Component, OnInit, ViewChild, ElementRef, OnDestroy, } from '@angular/core';
import Map from '@arcgis/core/WebMap';
import MapView from '@arcgis/core/views/MapView';
import esriConfig from "@arcgis/core/config";
import { NGXLogger } from 'ngx-logger';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})
export class MapComponent implements OnInit, OnDestroy {
  public view: any = null;
  constructor(private logger: NGXLogger) { }
  // The <div> where we will place the map
  @ViewChild('mapViewNode', { static: true }) private mapViewEl!: ElementRef;

  initializeMap(): Promise<any> {
    const container = this.mapViewEl.nativeElement;
    esriConfig.apiKey = "AAPKe7389af1dfbb4743bbe963f680e347e0wy1KDOwxxmHFXka_sE4AEAJ_obIXQ4uhV647oe1n9iLD12ZiIMV-1L-FnIpESqKr";
    const map = new Map({
/*       portalItem: {
        id: 'aa1d3f80270146208328cf66d022e09c',
      }, */
      basemap: "arcgis-navigation"
    });

    const view = new MapView({
      container,
      map: map,
      center: [14.506944, 35.884445],
      zoom: 15
    });


    this.view = view;
    return this.view.when();
  }

  ngOnInit(): any {
    // Initialize MapView and return an instance of MapView
    this.initializeMap().then(() => {
      // The map has been initialized
        this.logger.log('The map is ready.');
    });
  }

  ngOnDestroy(): void {
    if (this.view) {
      // destroy the map view
      this.view.destroy();
    }
  }
}
