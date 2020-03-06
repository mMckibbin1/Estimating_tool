using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Estimating_Tool.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Estimating_Tool.DAL
{
    public class Estimatingcontext:DbContext
    {

        public Estimatingcontext() : base("EstimatingContext")
        {
            Database.SetInitializer(new EstimatingInitializer());
        }

        public DbSet<Customer> Customer { get; set; }
        
        public DbSet<Project> Project { get; set; }

        public DbSet<CommercialType> CommercialType { get; set; }
        public DbSet<ContingencyDefault> ContingencyDefault { get; set; }
        public DbSet<EstimateDetail> EstimateDetail { get; set; }
        public DbSet<EstimateHeader> EstimateHeader { get; set; }
        public DbSet<EstimateStatus> EstimateStatus { get; set; }
        public DbSet<EstimateType> EstimateType { get; set; }
        public DbSet<LineItem> LineItem { get; set; }
        public DbSet<LineItemType> LineItemType { get; set; }
        public DbSet<LineItemTypeGroup> LineItemTypeGroup { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<Currency> Currency { get; set; }
		public DbSet<Manager> Managers { get; set; }
		public DbSet<ManagerPreferences> Preferences { get; set; }

		public DbSet<Consultant> Consultants { get; set; }
		public DbSet<Team> Teams { get; set; }
        public DbSet<Consultant_Project_join> consultant_Projects { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
	}
}