//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace petcan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public urunler()
        {
            this.siparis = new HashSet<siparis>();
        }
    
        public int urunId { get; set; }
        public string urunAdi { get; set; }
        public decimal urunFiyat { get; set; }
        public string urunAciklama { get; set; }
        public int urunAdet { get; set; }
        public string urunFoto { get; set; }
        public int urunKatId { get; set; }
        public int urunMarkaId { get; set; }
    
        public virtual kategori kategori { get; set; }
        public virtual markalar markalar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<siparis> siparis { get; set; }
    }
}
