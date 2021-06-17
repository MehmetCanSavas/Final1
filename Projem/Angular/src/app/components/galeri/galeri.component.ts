import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-galeri',
  templateUrl: './galeri.component.html',
  styleUrls: ['./galeri.component.css']
})
export class GaleriComponent implements OnInit {

  constructor(
    public apiServis: ApiService
  ) { }

  ngOnInit() {
  }

}
