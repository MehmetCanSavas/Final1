import { FotoyukleDialogComponent } from './../../dialogs/fotoyukle-dialog/fotoyukle-dialog.component';

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Sonuc } from 'src/app/models/Sonuc';
import { Urun } from 'src/app/models/Urun';
import { ApiService } from 'src/app/services/api.service';
import { MyAlertService } from 'src/app/services/myAlert.service';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';

import { UrunDialogComponent } from '../../dialogs/urun-dialog/urun-dialog.component';


@Component({
  selector: 'app-urunler',
  templateUrl: './urunler.component.html',
  styleUrls: ['./urunler.component.css'],
})
export class UrunlerComponent implements OnInit {
  dialogRef: MatDialogRef<UrunDialogComponent>;
  confirmdialogRef: MatDialogRef<ConfirmDialogComponent>;
  fotoDialogRef: MatDialogRef<FotoyukleDialogComponent>;
  urunler: Urun[];
  dataSource: any;
  displayedColumns = [
    'urunFoto',
    'urunId',
    'urunAdi',
    'urunAdet',
    'urunAciklama',
    'urunKatId',
    'urunKatAdi',
    'urunFiyat',
    'urunMarkaId',
    'islemler',
  ];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    public apiServis: ApiService,
    public matDialog: MatDialog,
    public alert: MyAlertService
  ) {}

  ngOnInit() {
    this.UrunListele();
  }

  yetkiKontrol(){
    return this.apiServis.yetkiControl()
  }
  UrunListele() {
    this.apiServis.UrunListe().subscribe((d: Urun[]) => {
      this.urunler = d;
      this.dataSource = new MatTableDataSource(this.urunler);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  UrunFiltrele(e) {
    var deger = e.target.value;
    this.dataSource.filter = deger.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  Ekle() {
    var yeniKayit: Urun = new Urun();
    this.dialogRef = this.matDialog.open(UrunDialogComponent, {
      width: '60%',
      data: {
        kayit: yeniKayit,
        islem: 'ekle',
      },
    });
    this.dialogRef.afterClosed().subscribe((d) => {
      if (d) {
        this.apiServis.UrunEkle(d).subscribe((s: Sonuc) => {
          this.alert.AlertUygula(s);
          if (s.islem) {
            this.UrunListele();
          }
        });
      }
    });
  }

  Duzenle(kayit: Urun) {
    this.dialogRef = this.matDialog.open(UrunDialogComponent, {
      width: '60%',
      data: {
        kayit: kayit,
        islem: 'duzenle',
      },
    });

    this.dialogRef.afterClosed().subscribe((d) => {
      if (d) {
        kayit.urunId = d.urunId;
        kayit.urunAdi = d.urunAdi;
        kayit.urunKatId = d.urunKatId;
        kayit.urunKatAdi = d.urunKatAdi;
        kayit.urunFiyat = d.urunFiyat;
        kayit.urunMarkaId = d.urunMarkaId;
        kayit.urunFoto = d.urunFoto;
        kayit.urunAciklama = d.urunAciklama;
        kayit.urunAdet = d.urunAdet;

        this.apiServis.UrunDuzenle(kayit).subscribe((s: Sonuc) => {
          this.alert.AlertUygula(s);
          if (s.islem) {
            this.UrunListele();
          }
        });
      }
    });
  }

  Sil(kayit: Urun) {
    this.confirmdialogRef = this.matDialog.open(ConfirmDialogComponent, {
      width: '60%',
    });
    this.confirmdialogRef.componentInstance.dialogMesaj =
      kayit.urunId +
      ' Idli ' +
      kayit.urunAdi +
      '  ??r??n?? Silinecektir Onayl??yor Musunuz?';

    this.confirmdialogRef.afterClosed().subscribe((d) => {
      if (d) {
        this.apiServis.UrunSil(kayit.urunId).subscribe((s: Sonuc) => {
          this.alert.AlertUygula(s);
          if (s.islem) {
            this.UrunListele();
          }
        });
      }
    });
  }

  FotoGuncelle(kayit: Urun) {
    this.fotoDialogRef = this.matDialog.open(FotoyukleDialogComponent, {
      width: '40%',
      data: kayit,
    });

    this.fotoDialogRef.afterClosed().subscribe(d=>{
      if (d) {
        d.urunAdi=kayit.urunAdi;
        this.apiServis.UrunFotoGuncelle(d).subscribe((s:Sonuc)=>{
          this.alert.AlertUygula(s);
          if (s.islem) {
            this.UrunListele();
          }
        });
      }
    });
  }
}
