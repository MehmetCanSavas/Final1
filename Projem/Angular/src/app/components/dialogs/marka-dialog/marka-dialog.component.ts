import { Marka } from './../../../models/Marka';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Inject } from '@angular/core';

@Component({
  selector: 'app-marka-dialog',
  templateUrl: './marka-dialog.component.html',
  styleUrls: ['./marka-dialog.component.css']
})
export class MarkaDialogComponent implements OnInit {
  dialogBaslik: string;
  yeniKayit: Marka;
  islem: string;
  frm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<MarkaDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild: FormBuilder,
  ) {

    this.islem = data.islem;
    this.yeniKayit = data.kayit;

    if (this.islem == 'ekle') {
      this.dialogBaslik = 'Marka Ekle';
    }
    if (this.islem == 'duzenle') {
      this.dialogBaslik = 'Marka Düzenle';
    }

    this.frm=this.FormOlustur();
   }

  ngOnInit() {
  }
  FormOlustur() {
    return this.frmBuild.group({

      markaId: [this.yeniKayit.markaId],
      markaAdi: [this.yeniKayit.markaAdi],
    });
  }
}
