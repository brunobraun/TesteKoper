﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KoperEntities : DbContext
    {
        public KoperEntities()
            : base("name=KoperEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Andar> Andar { get; set; }
        public virtual DbSet<Bloco> Bloco { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Empreendimento> Empreendimento { get; set; }
        public virtual DbSet<Proposta> Proposta { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Status_Unidade> Status_Unidade { get; set; }
        public virtual DbSet<Tipo_Empreendimento> Tipo_Empreendimento { get; set; }
        public virtual DbSet<Unidade> Unidade { get; set; }
        public virtual DbSet<Unidade_Proposta> Unidade_Proposta { get; set; }
        public virtual DbSet<Unidade_Reserva> Unidade_Reserva { get; set; }
        public virtual DbSet<Unidade_Venda> Unidade_Venda { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
    }
}
