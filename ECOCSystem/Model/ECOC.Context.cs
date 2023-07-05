﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECOCSystem.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ECOCEntities : DbContext
    {
        public ECOCEntities()
            : base("name=ECOCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientAddress> ClientAddress { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyBranch> CompanyBranch { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<TitleType> TitleType { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<RegistrationType> RegistrationType { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<VehicleInfo> VehicleInfo { get; set; }
        public virtual DbSet<VehicleBodyType> VehicleBodyType { get; set; }
        public virtual DbSet<VehicleColor> VehicleColor { get; set; }
        public virtual DbSet<VehicleFuelType> VehicleFuelType { get; set; }
        public virtual DbSet<VehicleMake> VehicleMake { get; set; }
        public virtual DbSet<UserEntity> UserEntity { get; set; }
        public virtual DbSet<CompanyEntity> CompanyEntity { get; set; }
        public virtual DbSet<SystemVariable> SystemVariable { get; set; }
        public virtual DbSet<CTPL> CTPL { get; set; }
        public virtual DbSet<CTPLTerm> CTPLTerm { get; set; }
        public virtual DbSet<MVPremium> MVPremium { get; set; }
        public virtual DbSet<VehicleClassification> VehicleClassification { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<CTPLTaxType> CTPLTaxType { get; set; }
        public virtual DbSet<CTPLApplication> CTPLApplication { get; set; }
        public virtual DbSet<InsuranceCOCSeries> InsuranceCOCSeries { get; set; }
        public virtual DbSet<MakeBodyType> MakeBodyType { get; set; }
        public virtual DbSet<MakeColor> MakeColor { get; set; }
        public virtual DbSet<VehicleSeries> VehicleSeries { get; set; }
        public virtual DbSet<MVSeries> MVSeries { get; set; }
        public virtual DbSet<VehicleModel> VehicleModel { get; set; }
        public virtual DbSet<VehicleVariant> VehicleVariant { get; set; }
    }
}
