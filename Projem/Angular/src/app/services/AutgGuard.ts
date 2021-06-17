import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { MainNavComponent } from "../components/main-nav/main-nav.component";
import { ApiService } from "./api.service";


@Injectable()
export class AuthGuard implements CanActivate{

  constructor(
    public apiServis:ApiService,
    public router:Router
  ){}


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){

    var yetkiler=route.data["yetkiler"] as Array<string>;
    var gitUrl=route.data["gerigit"] as string;

    if (!yetkiler || !yetkiler.length) {

      this.router.navigate([gitUrl]);
      return false;
    }
    var sonuc:boolean=false;
    sonuc=this.apiServis.yetkiControl();
    if (!sonuc) {
      this.router.navigate([gitUrl]);
    }


    return sonuc;

  }

}
