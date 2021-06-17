import { GaleriComponent } from './components/galeri/galeri.component';
import { MarkaListeComponent } from './components/ListeComponents/marka-liste/marka-liste.component';
import { MarkaComponent } from './components/admin/marka/marka.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { KategorilerComponent } from './components/admin/kategoriler/kategoriler.component';
import { KategoriListeComponent } from './components/ListeComponents/kategori-liste/kategori-liste.component';
import { SiparisListeComponent } from './components/ListeComponents/siparis-liste/siparis-liste.component';
import { UrunListeComponent } from './components/ListeComponents/urun-liste/urun-liste.component';
import { UyeListeComponent } from './components/ListeComponents/uye-liste/uye-liste.component';
import { LoginComponent } from './components/login/login.component';

import { SiparislerComponent } from './components/admin/siparisler/siparisler.component';
import { UrunlerComponent } from './components/admin/urunler/urunler.component';
import { UyelerComponent } from './components/admin/uyeler/uyeler.component';
import { AdminComponent } from './components/admin/admin/admin.component';
import { AuthGuard } from './services/AutgGuard';
import { IletisimComponent } from './components/iletisim/iletisim.component';
import { HakkimizdaComponent } from './components/hakkimizda/hakkimizda.component';


const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'login', component:LoginComponent},
  {path:'iletisim', component:IletisimComponent},
  {path:'hakkimizda', component:HakkimizdaComponent},
  {path:'galeri', component:GaleriComponent},


  //Admin
  {path:'admin', component:AdminComponent,},
  {path:'admin/uyeler', component:UyelerComponent,},
  {path:'admin/siparisler', component:SiparislerComponent,},
  {path:'admin/kategoriler', component:KategorilerComponent,},
  {path:'admin/markalar', component:MarkaComponent,},
  {path:'admin/urunler', component:UrunlerComponent,},
  {path:'admin/uyeliste/:uyeId', component:UyeListeComponent,},
  {path:'admin/urunliste/:urunId', component:UrunListeComponent,},
  {path:'admin/kategoriliste/:katId', component:KategoriListeComponent,},
  {path:'admin/siparisliste/:siparisId', component:SiparisListeComponent,},
  {path:'admin/markaliste/:markaId', component:MarkaListeComponent,},




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
