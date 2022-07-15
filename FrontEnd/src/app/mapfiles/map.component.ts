import { Component, OnInit, ViewChild, ElementRef, OnDestroy, } from '@angular/core';
import Map from '@arcgis/core/WebMap';
import MapView from '@arcgis/core/views/MapView';
import BasemapGallery from "@arcgis/core/widgets/BasemapGallery";
import Expand from "@arcgis/core/widgets/Expand";
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
  @ViewChild('mapDiv', { static: true }) private mapEl!: ElementRef;
  @ViewChild('galleryDiv', {static:true}) private galleryEl!: ElementRef;


  initializeMap(): Promise<any> {
    const mapcontainer = this.mapEl.nativeElement;
    const galcontainer = this.galleryEl.nativeElement;


    esriConfig.apiKey = "AAPKe7389af1dfbb4743bbe963f680e347e0wy1KDOwxxmHFXka_sE4AEAJ_obIXQ4uhV647oe1n9iLD12ZiIMV-1L-FnIpESqKr";

    const map = new Map({
      basemap: "arcgis-imagery"
    });

    const view = new MapView({
      container: mapcontainer,
      map: map,
      center: [14.51500, 35.89500],
      zoom: 15
    });

    const basemapGallery = new BasemapGallery({
      view: view,
      container: galcontainer
    });

    const galExpand = new Expand({
      view: view,
      content: basemapGallery
    });

    view.ui.add(galExpand, {
      position: "top-right"
    });

    this.view = view;
    return this.view.when();
  }

  ngOnInit(): any {
    this.initializeMap().then(() => {
        this.logger.log('The map is ready.');
    });
  }

  ngOnDestroy(): void {
    if (this.view) {
      this.view.destroy();
    }
  }
}
