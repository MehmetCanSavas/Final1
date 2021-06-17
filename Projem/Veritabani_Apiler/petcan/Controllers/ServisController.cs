using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Http;
using petcan.Auth;
using petcan.Models;
using petcan.ViewModel;

namespace petcan.Controllers
{
    public class ServisController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori
        
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.kategori.Select(x => new KategoriModel()
            {

                katId = x.katId,
                katAdi = x.katAdi,
                katUrunAdet = x.urunler.Count()

            }
            ).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.kategori.Where(s => s.katId == katId).Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
                katUrunAdet = x.urunler.Count()
            }).FirstOrDefault();
            return kayit;
        }
        
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.kategori.Count(s => s.katAdi == model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır.";
                return sonuc;
            }

            kategori yeni = new kategori();
            yeni.katAdi = model.katAdi;
            db.kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Başarıyla Eklendi";
            return sonuc;
        }
        
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            kategori kayit = db.kategori.Where(s => s.katId == model.katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Kategori Yok";
                return sonuc;
            }

            kayit.katAdi = model.katAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Başarıyla Düzenlendi";
            return sonuc;
        }
        
        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            kategori kayit = db.kategori.Where(s => s.katId == katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Kategori Yok";
                return sonuc;
            }

            if (db.urunler.Count(s => s.urunKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Kategorinin İçerisinde Ürünler Var Kategori Silinemedi";
                return sonuc;
            }

            db.kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }

        #endregion

        
        
        
        #region   urunler

        [HttpGet]
        [Route("api/urunlerliste")]
        public List<UrunlerModel> UrunListe()
        {
            List<UrunlerModel> liste = db.urunler.Select(x => new UrunlerModel()
            {

                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunKatId = x.urunKatId,
                urunFiyat = x.urunFiyat,
                urunAciklama = x.urunAciklama,
                urunAdet = x.urunAdet,
                urunFoto = x.urunFoto,
                urunMarkaId = x.urunMarkaId,
                urunKatAdi = x.kategori.katAdi,

            }
            ).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/urunlerbyid/{urunId}")]
        public UrunlerModel urunlerById(int urunId)
        {
            UrunlerModel kayit = db.urunler.Where(s => s.urunId == urunId).Select(x => new UrunlerModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunAdet = x.urunAdet,
                urunKatId = x.urunKatId,
                urunMarkaId = x.urunMarkaId,
                urunFoto = x.urunFoto,
                urunAciklama = x.urunAciklama,
                urunFiyat = x.urunFiyat,
              

            }).FirstOrDefault();
            return kayit;
        }
        
        [HttpPost]
        [Route("api/urunlerekle")]
        public SonucModel UrunlerEkle(UrunlerModel model)
        {
            if (db.urunler.Count(s => s.urunAdi == model.urunAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen ürün Kayıtlıdır.";
                return sonuc;
            }

            urunler yeni = new urunler();
            yeni.urunAdi = model.urunAdi;
            yeni.urunFiyat = model.urunFiyat;
            yeni.urunAciklama = model.urunAciklama;
            yeni.urunAdet = model.urunAdet;
            yeni.urunFoto = "stokfoto.png";
            yeni.urunMarkaId = model.urunMarkaId;
            yeni.urunKatId = model.urunKatId;
           

            db.urunler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Başarıyla Eklendi";
            return sonuc;
        }
        
        [HttpPut]
        [Route("api/urunlerduzenle")]
        public SonucModel UrunlerDuzenle(UrunlerModel model,Object f)
        {
            urunler kayit = db.urunler.Where(s => s.urunId == model.urunId).FirstOrDefault();



            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }
           

            kayit.urunAdi = model.urunAdi;
            kayit.urunFiyat = model.urunFiyat;
            kayit.urunAciklama = model.urunAciklama;
            kayit.urunAdet = model.urunAdet;
            kayit.urunFoto = model.urunFoto;
            kayit.urunMarkaId = model.urunMarkaId;
            kayit.urunKatId = model.urunKatId;
            


            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Başarıyla Düzenlendi";
            return sonuc;
        }
        
        [HttpDelete]
        [Route("api/urunlersil/{urunId}")]
        public SonucModel UrunlerSil(int urunId)
        {
            urunler kayit = db.urunler.Where(s => s.urunId == urunId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }

            if (db.urunler.Count(s => s.urunKatId == urunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Kategorinin İçerisinde Ürünler Silinemedi";
                return sonuc;
            }

            db.urunler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi";
            return sonuc;
        }
        
        [HttpPost]
        [Route("api/urunfotoguncelle")]
        public SonucModel UrunGorselGuncelle(UrunFotoModel model)
        {
            urunler urn = db.urunler.Where(s => s.urunId == model.urunId).SingleOrDefault();
            if (urn == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }

            if (urn.urunFoto != "stokfoto.png")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + urn.urunFoto);
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.fotoData;
            string base64 = data.Substring(data.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = urn.urunId + model.fotoUzanti.Replace("image/", ".");

            using (var ms = new MemoryStream(imgbytes, 0, imgbytes.Length))
            {

                Image img = Image.FromStream(ms, true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + dosyaAdi));

            }

            urn.urunFoto = dosyaAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Görsel Güncellendi";


            return sonuc;
        }


        #endregion


        #region markalar

        [HttpGet]
        [Route("api/markaliste")]
        public List<ModelMarka> markaListe()
        {
            List<ModelMarka> liste = db.markalar.Select(x => new ModelMarka()
            {
                markaId = x.markaId,
                markaAdi = x.markaAdi,
            }

            ).ToList();
     
            return liste;

        }
        [HttpGet]
        [Route("api/markabyid/{markaId}")]
        public ModelMarka markaById(int markaId)
        {
            ModelMarka kayit = db.markalar.Where(s => s.markaId == markaId).Select(x => new ModelMarka()
            {
                markaId = x.markaId,
                markaAdi = x.markaAdi,

            }).FirstOrDefault();
            return kayit;
        }
        
        [HttpPost]
        [Route("api/markaekle")]
        public SonucModel MarkaEkle(ModelMarka model)
        {
            if (db.markalar.Count(s => s.markaAdi == model.markaAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Marka Kayıtlıdır.";
                return sonuc;
            }

            markalar yeni = new markalar();
            yeni.markaAdi = model.markaAdi;
            yeni.markaAdi = model.markaAdi;


            db.markalar.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Başarıyla Eklendi";
            return sonuc;
        }
        
        [HttpPut]
        [Route("api/markaduzenle")]
        public SonucModel MarkaDuzenle(ModelMarka model)
        {
            markalar kayit = db.markalar.Where(s => s.markaId == model.markaId).FirstOrDefault();



            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Ürün Yok";
                return sonuc;
            }

            kayit.markaAdi = model.markaAdi;
            kayit.markaId = model.markaId;


            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Marka Başarıyla Düzenlendi";
            return sonuc;
        }
        
        [HttpDelete]
        [Route("api/markasil/{urunId}")]
        public SonucModel MarkaSil(int markaId)
        {
            markalar kayit = db.markalar.Where(s => s.markaId == markaId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Marka Yok";
                return sonuc;
            }

            if (db.markalar.Count(s => s.markaId == markaId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Marka Silinemedi";
                return sonuc;
            }

            db.markalar.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Marka Silindi";
            return sonuc;
            
        }
        #endregion

        #region uyeler
        public class UyeGiris
        {
            public string Name { get; set; }
            public string parola { get; set; }
        }


        [HttpPost]
        [Route("api/giris")]
        public UyelerModel UyeGirisi(UyeGiris u)
        {

            UyelerModel uye = db.uyeler.Where(s => s.uyeKulAdi == u.Name && s.uyeSifre == u.parola).Select(x => new UyelerModel()
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


        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyelerModel> UyeListe()
        {
            List<UyelerModel> liste = db.uyeler.Select(x => new UyelerModel()
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
            }
            ).ToList();

            return liste;
        }
        
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyelerModel UyeById(int uyeId)
        {
            UyelerModel kayit = db.uyeler.Where(s => s.uyeId == uyeId).Select(x => new UyelerModel()
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

            }).FirstOrDefault();
            return kayit;
        }
        
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyelerModel model)
        {
            if (db.uyeler.Count(s => s.uyeKulAdi == model.uyeKulAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Üye Kayıtlıdır.";
                return sonuc;
            }

            uyeler yeni = new uyeler();

            yeni.uyeAdi = model.uyeAdi;
            yeni.uyeSoyadi = model.uyeSoyadi;
            yeni.uyeKulAdi = model.uyeKulAdi;
            yeni.uyeSifre = model.uyeSifre;
            yeni.uyeTelefon = model.uyeTelefon;
            yeni.uyeMail = model.uyeMail;
            yeni.uyeAdres = model.uyeAdres;

            db.uyeler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Başarıyla Eklendi";
            return sonuc;
        }
        
        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyelerModel model)
        {
            uyeler kayit = db.uyeler.Where(s => s.uyeId == model.uyeId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Üye Yok";
                return sonuc;
            }


       
            kayit.uyeAdi = model.uyeAdi;
            kayit.uyeSoyadi = model.uyeSoyadi;
            kayit.uyeKulAdi = model.uyeKulAdi;
            kayit.uyeSifre = model.uyeSifre;
            kayit.uyeTelefon = model.uyeTelefon;
            kayit.uyeMail = model.uyeMail;
            kayit.uyeAdres = model.uyeAdres;
            kayit.uyeYetki = model.uyeYetki;


            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Başarıyla Düzenlendi";
            return sonuc;
        }
        
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            uyeler kayit = db.uyeler.Where(s => s.uyeId == uyeId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Üye Yok";
                return sonuc;
            }




            db.uyeler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
        #endregion

        #region Siparis
        
        [HttpGet]
        [Route("api/siparisliste")]
        public List<SiparisModel> SiparisListe()
        {
            List<SiparisModel> liste = db.siparis.Select(x => new SiparisModel()
            {

                siparisId = x.siparisId,
                siparisUyeId = x.siparisUyeId,
                siparisTarih = x.siparisTarih,
                siparisUrunId = x.siparisUrunId,

                siparisVerenAdres = x.uyeler.uyeAdres,
                siparisVerenAdi = x.uyeler.uyeAdi,
                siparisVerenSoyadi = x.uyeler.uyeSoyadi,
                siparisVerenUyeMail = x.uyeler.uyeMail,
                siparisVerenUyeTelefon = x.uyeler.uyeTelefon,

                siparisUrunAdi = x.urunler.urunAdi,
                siparisUrunFiyati = x.urunler.urunFiyat,
                siparisUrunAciklama = x.urunler.urunAciklama,
                siparisUrunAdet = x.urunler.urunAdet,

                siparisUrunMarkaAdi = x.urunler.markalar.markaAdi,
                siparisUrunKategoriAdi = x.urunler.kategori.katAdi,

            }
            ).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/siparisbyid/{siparisId}")]
        public SiparisModel SiparisById(int siparisId)
        {
            SiparisModel kayit = db.siparis.Where(s => s.siparisId == siparisId).Select(x => new SiparisModel()
            {
                siparisId = x.siparisId,
                siparisUyeId = x.siparisUyeId,
                siparisTarih = x.siparisTarih,
                siparisUrunId = x.siparisUrunId,

                siparisVerenAdres = x.uyeler.uyeAdres,
                siparisVerenAdi = x.uyeler.uyeAdi,
                siparisVerenSoyadi = x.uyeler.uyeSoyadi,
                siparisVerenUyeMail = x.uyeler.uyeMail,
                siparisVerenUyeTelefon = x.uyeler.uyeTelefon,

                siparisUrunAdi = x.urunler.urunAdi,
                siparisUrunFiyati = x.urunler.urunFiyat,
                siparisUrunAciklama = x.urunler.urunAciklama,
                siparisUrunAdet = x.urunler.urunAdet,

                siparisUrunMarkaAdi = x.urunler.markalar.markaAdi,
                siparisUrunKategoriAdi = x.urunler.kategori.katAdi,



            }).FirstOrDefault();
            return kayit;
        }
        
        [HttpPost]
        [Route("api/siparisEkle")]
        public SonucModel SiparisEkle(SiparisModel model)
        {
            if (db.siparis.Count(s => s.siparisId == model.siparisId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Sipariş Kayıtlıdır.";
                return sonuc;
            }

            siparis yeni = new siparis();

            
            yeni.siparisUyeId = model.siparisUyeId;
            yeni.siparisTarih = model.siparisTarih;
            yeni.siparisUrunId = model.siparisUrunId;


            db.siparis.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sipariş Başarıyla Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/siparisduzenle")]
        public SonucModel SiparisDuzenle(SiparisModel model)
        {
            siparis kayit = db.siparis.Where(s => s.siparisId == model.siparisId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Sipariş Yok";
                return sonuc;
            }


            kayit.siparisId = model.siparisId;
            kayit.siparisUyeId = model.siparisUyeId;
            kayit.siparisTarih = model.siparisTarih;
            kayit.siparisUrunId = model.siparisUrunId;



            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sipariş Başarıyla Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/siparissil/{siparisId}")]
        public SonucModel SiparisSil(int siparisId)
        {
            siparis kayit = db.siparis.Where(s => s.siparisId == siparisId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Böyle Bir Sipariş Yok";
                return sonuc;
            }




            db.siparis.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sipariş Silindi";
            return sonuc;
        }

        #endregion
    }
}