using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petcan.ViewModel
{
    public class SiparisModel
    {
        public int siparisId { get; set; }
        public int siparisUyeId { get; set; }
        public string siparisTarih { get; set; }
       
        public int siparisUrunId { get; set; }
        public string siparisVerenAdi { get; set; }
        public string siparisVerenSoyadi{ get; set; }
        public string siparisVerenAdres { get; set; }
        public string siparisVerenUyeTelefon { get; set; }
        public string siparisVerenUyeMail { get; set; }

        public string siparisUrunAdi { get; set; }
        public decimal siparisUrunFiyati { get; set; }
        public string siparisUrunAciklama { get; set; }
        public int siparisUrunAdet { get; set; }

        public string siparisUrunMarkaAdi { get; set; }
        public string siparisUrunKategoriAdi { get; set; }







    }
}