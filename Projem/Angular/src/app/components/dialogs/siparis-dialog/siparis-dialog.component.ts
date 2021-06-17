import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Siparis } from 'src/app/models/Siparis';

@Component({
  selector: 'app-siparis-dialog',
  templateUrl: './siparis-dialog.component.html',
  styleUrls: ['./siparis-dialog.component.css']
})
export class SiparisDialogComponent implements OnInit {
  dialogBaslik: string;
  yeniKayit: Siparis;
  islem: string;
  frm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<SiparisDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild: FormBuilder,
  ) {
    this.islem = data.islem;
    this.yeniKayit = data.kayit;

    if (this.islem == 'ekle') {
      this.dialogBaslik = 'Sipariş Ekle';
    }
    if (this.islem == 'duzenle') {
      this.dialogBaslik = 'Sipariş Düzenle';
    }

    this.frm=this.FormOlustur();
   }

  ngOnInit() {
  }

  FormOlustur() {
    return this.frmBuild.group({


      siparisUyeId: [this.yeniKayit.siparisUyeId],
      siparisTarih: [this.yeniKayit.siparisTarih],
      siparisUrunId: [this.yeniKayit.siparisUrunId],

    });
  }

}
