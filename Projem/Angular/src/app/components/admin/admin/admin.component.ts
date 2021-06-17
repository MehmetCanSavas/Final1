import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  yetki :boolean = false
  constructor(public apiServis:ApiService) { }

  ngOnInit() {
  }

  yetkiKontrol(){
    return this.apiServis.yetkiControl()
  }


}
