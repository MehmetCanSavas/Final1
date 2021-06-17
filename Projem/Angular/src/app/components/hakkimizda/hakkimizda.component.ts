import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-hakkimizda',
  templateUrl: './hakkimizda.component.html',
  styleUrls: ['./hakkimizda.component.css']
})
export class HakkimizdaComponent implements OnInit {

  constructor(
    public apiServis: ApiService
  ) { }

  ngOnInit() {
  }

}
