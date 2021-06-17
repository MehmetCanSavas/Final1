import { Marka } from './../../../models/Marka';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Kategori } from 'src/app/models/Kategori';
import { Urun } from 'src/app/models/Urun';
import { ApiService } from 'src/app/services/api.service';
import { MyAlertService } from 'src/app/services/myAlert.service';

@Component({
  selector: 'app-urun-dialog',
  templateUrl: './urun-dialog.component.html',
  styleUrls: ['./urun-dialog.component.css']
})
export class UrunDialogComponent implements OnInit {
  dialogBaslik: string;
  yeniKayit: Urun;
  islem: string;
  frm: FormGroup;
  dataSource: any;
  kategoriler: Kategori[];
  markalar:Marka[];

  constructor(
    public dialogRef: MatDialogRef<UrunDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild: FormBuilder,
    public apiServis: ApiService,
    public matDialog: MatDialog,
    public alert: MyAlertService
  ) {
    this.islem = data.islem;
    this.yeniKayit = data.kayit;

    if (this.islem == 'ekle') {
      this.dialogBaslik = 'Ürün Ekle';
    }
    if (this.islem == 'duzenle') {
      this.dialogBaslik = 'Ürün Düzenle';
    }

    this.frm=this.FormOlustur();
   }

  ngOnInit() {
    this.KategoriListe()
    this.MarkaListe()
  }

  FormOlustur() {
    return this.frmBuild.group({
      urunId:[this.yeniKayit.urunId],
      urunAdi: [this.yeniKayit.urunAdi],
      urunKatId: [this.yeniKayit.urunKatId],
      urunKatAdi: [this.yeniKayit.urunKatAdi],
      urunFiyat: [this.yeniKayit.urunFiyat],
      urunMarkaId: [this.yeniKayit.urunMarkaId],

      urunAciklama: [this.yeniKayit.urunAciklama],
      urunAdet: [this.yeniKayit.urunAdet],
    });
  }

  KategoriListe() {
    this.apiServis.KategoriListe().subscribe((d: Kategori[]) => {
      this.kategoriler = d;
      this.dataSource = new MatTableDataSource(this.kategoriler);

    });
  }

  MarkaListe() {
    this.apiServis.markaliste().subscribe((d: Marka[]) => {
      this.markalar = d;
      this.dataSource = new MatTableDataSource(this.markalar);

    });
  }
}
