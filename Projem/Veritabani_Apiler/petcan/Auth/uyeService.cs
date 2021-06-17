using petcan.Models;
using petcan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petcan.Auth
{
    public class uyeService
    {
        Database1Entities db = new Database1Entities();
        public UyelerModel UyeOturumAc(string kadi, string parola)
        {
            UyelerModel uye = db.uyeler.Where(s => s.uyeKulAdi == kadi && s.uyeSifre == parola).Select(x => new UyelerModel()
            {
                uyeId = x.uyeId,
                uyeAdi = x.uyeAdi,
                uyeSoyadi = x.uyeSoyadi,
                uyeKulAdi = x.uyeKulAdi,
                uyeSifre = x.uyeSifre,
                uyeTelefon = x.uyeTelefon,
                uyeMail = x.uyeMail,
                uyeAdres = x.uyeAdres,
                uyeYetki = x.uyeYetki,

            }).SingleOrDefault();

            return uye;
        }

    }
}