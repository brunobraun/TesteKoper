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
    
    public partial class Unidade_Venda
    {
        public int id { get; set; }
        public long idVenda { get; set; }
        public long idUnidade { get; set; }
    
        public virtual Unidade Unidade { get; set; }
        public virtual Venda Venda { get; set; }
    }
}