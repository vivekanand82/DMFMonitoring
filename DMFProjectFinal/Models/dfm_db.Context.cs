﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMFProjectFinal.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dfm_dbEntities : DbContext
    {
        public dfm_dbEntities()
            : base("name=dfm_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AgenciesInfo> AgenciesInfoes { get; set; }
        public virtual DbSet<BankMaster> BankMasters { get; set; }
        public virtual DbSet<CommitteeMaster> CommitteeMasters { get; set; }
        public virtual DbSet<CommitteeTypeMaster> CommitteeTypeMasters { get; set; }
        public virtual DbSet<DataLog> DataLogs { get; set; }
        public virtual DbSet<DesignationMaster> DesignationMasters { get; set; }
        public virtual DbSet<DistrictMaster> DistrictMasters { get; set; }
        public virtual DbSet<DMFFundCollection> DMFFundCollections { get; set; }
        public virtual DbSet<LesseeMaster> LesseeMasters { get; set; }
        public virtual DbSet<MineralNameMaster> MineralNameMasters { get; set; }
        public virtual DbSet<ProjectMaster> ProjectMasters { get; set; }
        public virtual DbSet<ProjectProposalPrepration> ProjectProposalPreprations { get; set; }
        public virtual DbSet<ProjectStatusMaster> ProjectStatusMasters { get; set; }
        public virtual DbSet<RoleMenuMapping> RoleMenuMappings { get; set; }
        public virtual DbSet<RoyaltyCollection> RoyaltyCollections { get; set; }
        public virtual DbSet<SectorNameMaster> SectorNameMasters { get; set; }
        public virtual DbSet<SectorTypeMaster> SectorTypeMasters { get; set; }
        public virtual DbSet<StateMaster> StateMasters { get; set; }
        public virtual DbSet<TehsilMaster> TehsilMasters { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<VillageMaster> VillageMasters { get; set; }
        public virtual DbSet<YearMaster> YearMasters { get; set; }
        public virtual DbSet<MonthMaster> MonthMasters { get; set; }
        public virtual DbSet<MenuMaster> MenuMasters { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<LesseeOpeningDMFAmt> LesseeOpeningDMFAmts { get; set; }
        public virtual DbSet<DemoState> DemoStates { get; set; }
        public virtual DbSet<CommitteeDesignationMaster> CommitteeDesignationMasters { get; set; }
        public virtual DbSet<BlockMaster> BlockMasters { get; set; }
        public virtual DbSet<ProposalStatusMaster> ProposalStatusMasters { get; set; }
        public virtual DbSet<ProjectMetting> ProjectMettings { get; set; }
    }
}
