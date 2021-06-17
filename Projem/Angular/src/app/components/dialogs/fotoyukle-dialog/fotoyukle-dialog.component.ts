import { UrunFoto } from './../../../models/UrunFoto';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Urun } from 'src/app/models/Urun';
import { ApiService } from 'src/app/services/api.service';
import { fileURLToPath } from 'url';

@Component({
  selector: 'app-fotoyukle-dialog',
  templateUrl: './fotoyukle-dialog.component.html',
  styleUrls: ['./fotoyukle-dialog.component.css'],
})
export class FotoyukleDialogComponent implements OnInit {
  secilenfoto: any;
  urunfoto: UrunFoto = new UrunFoto();
  secUrun: Urun;
  constructor(
    public dialogRef: MatDialogRef<FotoyukleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public apiServis: ApiService
  ) {
    this.secUrun = this.data;
  }

  ngOnInit() {

  }

  
  FotoSec(e) {
    var gorseller = e.target.files;
    var gorsel = <File>gorseller[0];
    var fr = new FileReader();

    fr.onloadend = () => {
      this.secilenfoto = fr.result;
      this.urunfoto.fotoData = fr.result.toString();
      this.urunfoto.fotoUzanti = gorsel.type;
    };
    this.secUrun.urunFoto = ""
    this.apiServis.UrunDuzenle(this.secUrun)
    fr.readAsDataURL(gorsel);
  }


}
