//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Koper.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Andar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Andar()
        {
            this.Unidade = new HashSet<Unidade>();
        }
    
        public long id { get; set; }
        public string nome { get; set; }
        public int posicao { get; set; }
        public long idBloco { get; set; }
    
        public virtual Bloco Bloco { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidade> Unidade { get; set; }
    }
}