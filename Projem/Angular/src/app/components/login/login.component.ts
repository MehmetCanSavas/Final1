import { Component, OnInit } from '@angular/core';
import { Sonuc } from 'src/app/models/Sonuc';
import { Uye } from 'src/app/models/Uye';
import { ApiService } from 'src/app/services/api.service';
import { MyAlertService } from 'src/app/services/myAlert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  constructor(
    public apiServis:ApiService,
    public alert:MyAlertService,
  ) {

  }

  ngOnInit() {

  }


  OturumAc(kullaniciAdi: string, sifre: string) {
    this.apiServis.UyeGiris(kullaniciAdi,sifre).subscribe((d:Uye)=>{
      console.log("d",d);
      localStorage.setItem("uid",d.uyeId.toString());
      localStorage.setItem("kadi",d.uyeKulAdi);
      localStorage.setItem("uyeYetkileri",d.uyeYetki.toString());
      location.href="";
    },err=>{
      var s:Sonuc=new Sonuc();
      s.islem=false;
      s.mesaj="Kullanıcı Adı veya Şifre Geçersiz!";
      this.alert.AlertUygula(s);
    });

  }
}
