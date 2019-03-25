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
    
    public partial class Unidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unidade()
        {
            this.Unidade_Proposta = new HashSet<Unidade_Proposta>();
            this.Unidade_Reserva = new HashSet<Unidade_Reserva>();
            this.Unidade_Venda = new HashSet<Unidade_Venda>();
        }
    
        public long id { get; set; }
        public string nome { get; set; }
        public int idStatus_Unidade { get; set; }
        public double area_privativa { get; set; }
        public double area_comum { get; set; }
        public double vendavel { get; set; }
        public double valor { get; set; }
        public double valor_minimo_venda { get; set; }
        public int pertence_a_tipo { get; set; }
        public Nullable<long> idAndar { get; set; }
        public Nullable<long> idBloco { get; set; }
        public Nullable<long> idEmpreendimento { get; set; }
    
        public virtual Andar Andar { get; set; }
        public virtual Bloco Bloco { get; set; }
        public virtual Empreendimento Empreendimento { get; set; }
        public virtual Status_Unidade Status_Unidade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidade_Proposta> Unidade_Proposta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidade_Reserva> Unidade_Reserva { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidade_Venda> Unidade_Venda { get; set; }
    }
}
