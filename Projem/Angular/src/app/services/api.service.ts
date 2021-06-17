import { UrunFoto } from './../models/UrunFoto';
import { Marka } from './../models/Marka';
import { HttpClient,  HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Kategori } from '../models/Kategori';
import { Siparis } from '../models/Siparis';
import { Urun } from '../models/Urun';
import { Uye } from '../models/Uye';
import { giris } from '../models/giris';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  apiUrl = 'https://localhost:44347/api/';
  siteUrl = 'https://localhost:44347/';

  constructor(public http: HttpClient) {}

  // Oturum İşlemleri
  UyeGiris(kullaniciAdi: string, sifre: string) {
    var data =
      'Name=' +
      kullaniciAdi +
      '&parola=' +
      sifre +
      '&grant_type=password';
      //console.log(data);
    var reqHeader = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
    });
    return this.http.post(this.apiUrl + 'giris', data, { headers: reqHeader });
  }
  yetkiControl(){
      if (localStorage.getItem('uyeYetkileri') == "1") {
        return true;
      } 
      return false;
      
  }
  oturumKontrol() {
    if (localStorage.getItem('token')) {
      return true;
    } else {
      return false;
    }
  }



  //Kategori Servis
  KategoriListe() {
    return this.http.get(this.apiUrl + 'kategoriliste');
  }

  KategoriById(katId: number) {
    return this.http.get(this.apiUrl + 'kategoribyid/' + katId);
  }

  KategoriEkle(kat: Kategori) {
    return this.http.post(this.apiUrl + 'kategoriekle', kat);
  }

  KategoriDuzenle(kat: Kategori) {
    return this.http.put(this.apiUrl + 'kategoriduzenle', kat);
  }

  KategoriSil(katId: number) {
    return this.http.delete(this.apiUrl + 'kategorisil/' + katId);
  }

  //Ürün Servis

  UrunListe() {
    return this.http.get(this.apiUrl + 'urunlerliste');
  }



//
  UrunListeByKatId(katId: number) {
    return this.http.get(this.apiUrl + 'urunlerlistebykatid/' + katId);
  }
//


  UrunById(urunId: number) {
    return this.http.get(this.apiUrl + 'urunlerbyid/' + urunId);
  }

  UrunEkle(urn: Urun) {
    return this.http.post(this.apiUrl + 'urunlerekle', urn);
  }

  UrunDuzenle(urn: Urun) {
    return this.http.put(this.apiUrl + 'urunlerduzenle', urn);
  }

  UrunSil(urunId: number) {
    return this.http.delete(this.apiUrl + 'urunlersil/' + urunId);
  }

  UrunFotoGuncelle(UrunFoto: UrunFoto) {
    return this.http.post(this.apiUrl + 'urunfotoguncelle', UrunFoto);
  }
  //Marka Servis

  markaliste() {
    return this.http.get(this.apiUrl + 'markaliste');
  }

  markabyid(markaId: number) {
    return this.http.get(this.apiUrl + 'markabyid/' + markaId);
  }

  markaekle(mrk: Marka) {
    return this.http.get(this.apiUrl + 'markaekle/' + mrk);
  }

  markaduzenle(mrk: Marka) {
    return this.http.put(this.apiUrl + 'markaduzenle', mrk);
  }

  markasil(markaId: number) {
    return this.http.delete(this.apiUrl + 'markasil/' + markaId);
  }


  // Üye Servis

  UyeListe() {
    return this.http.get(this.apiUrl + 'uyeliste');
  }

  UyeById(uyeId: number) {
    return this.http.get(this.apiUrl + 'uyebyid/' + uyeId);
  }

  UyeEkle(uy: Uye) {
    return this.http.post(this.apiUrl + 'uyeekle', uy);
  }

  UyeDuzenle(uy: Uye) {
    return this.http.put(this.apiUrl + 'uyeduzenle', uy);
  }

  UyeSil(uyeId: number) {
    return this.http.delete(this.apiUrl + 'uyesil/' + uyeId);
  }

  // Sipariş Servis

  SiparisListe() {
    return this.http.get(this.apiUrl + 'siparisliste');
  }

  SiparisById(siparisId: number) {
    return this.http.get(this.apiUrl + 'siparisbyid/' + siparisId);
  }

  SiparisEkle(sprs: Siparis) {
    return this.http.post(this.apiUrl + 'siparisEkle', sprs);
  }

  SiparisDuzenle(sprs: Siparis) {
    return this.http.put(this.apiUrl + 'siparisduzenle', sprs);
  }

  SiparisSil(siparisId: number) {
    return this.http.delete(this.apiUrl + 'siparissil/' + siparisId);
  }
}
