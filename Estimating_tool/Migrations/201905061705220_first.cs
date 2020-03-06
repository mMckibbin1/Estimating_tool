namespace Estimating_Tool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommercialType",
                c => new
                    {
                        CommercialTypeId = c.Int(nullable: false, identity: true),
                        CommercialTypeStr = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.CommercialTypeId);
            
            CreateTable(
                "dbo.EstimateHeader",
                c => new
                    {
                        EstimateHeaderId = c.Int(nullable: false, identity: true),
                        ConsultantId = c.Int(),
                        CustomerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        EstimateName = c.String(maxLength: 100),
                        UnitOfMeasureId = c.Int(nullable: false),
                        EstimateTypeId = c.Int(nullable: false),
                        CommercialTypeId = c.Int(nullable: false),
                        ContingencyDefaultId = c.Int(nullable: false),
                        EstimateStatusId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EstimateHeaderId)
                .ForeignKey("dbo.CommercialType", t => t.CommercialTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ContingencyDefault", t => t.ContingencyDefaultId, cascadeDelete: true)
                .ForeignKey("dbo.EstimateStatus", t => t.EstimateStatusId, cascadeDelete: true)
                .ForeignKey("dbo.EstimateType", t => t.EstimateTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.UnitOfMeasure", t => t.UnitOfMeasureId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UnitOfMeasureId)
                .Index(t => t.EstimateTypeId)
                .Index(t => t.CommercialTypeId)
                .Index(t => t.ContingencyDefaultId)
                .Index(t => t.EstimateStatusId);
            
            CreateTable(
                "dbo.ContingencyDefault",
                c => new
                    {
                        ContingencyDefaultId = c.Int(nullable: false, identity: true),
                        ContingencyDefaultInt = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ContingencyDefaultId);
            
            CreateTable(
                "dbo.EstimateDetail",
                c => new
                    {
                        EstimateDetailId = c.Int(nullable: false, identity: true),
                        EstimateHeaderId = c.Int(nullable: false),
                        LineItemId = c.Int(nullable: false),
                        Estimate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(maxLength: 30),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EstimateDetailId)
                .ForeignKey("dbo.EstimateHeader", t => t.EstimateHeaderId, cascadeDelete: true)
                .ForeignKey("dbo.LineItem", t => t.LineItemId, cascadeDelete: true)
                .Index(t => t.EstimateHeaderId)
                .Index(t => t.LineItemId);
            
            CreateTable(
                "dbo.LineItem",
                c => new
                    {
                        LineItemId = c.Int(nullable: false, identity: true),
                        LineItemGroupId = c.Int(nullable: false),
                        LineItemStr = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LineItemId)
                .ForeignKey("dbo.LineItemTypeGroup", t => t.LineItemGroupId, cascadeDelete: true)
                .Index(t => t.LineItemGroupId);
            
            CreateTable(
                "dbo.LineItemTypeGroup",
                c => new
                    {
                        LineItemTypeGroupId = c.Int(nullable: false, identity: true),
                        LineItemType = c.Int(nullable: false),
                        LineItemTypeGroupStr = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LineItemTypeGroupId)
                .ForeignKey("dbo.LineItemType", t => t.LineItemType, cascadeDelete: true)
                .Index(t => t.LineItemType);
            
            CreateTable(
                "dbo.LineItemType",
                c => new
                    {
                        LineItemTypeId = c.Int(nullable: false, identity: true),
                        LineItemTypeStr = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LineItemTypeId);
            
            CreateTable(
                "dbo.EstimateStatus",
                c => new
                    {
                        EstimateStatusId = c.Int(nullable: false, identity: true),
                        EstimateStatusStr = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.EstimateStatusId);
            
            CreateTable(
                "dbo.EstimateType",
                c => new
                    {
                        EstimateTypeId = c.Int(nullable: false, identity: true),
                        EstimateTypeStr = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EstimateTypeId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        AtlasID = c.String(maxLength: 20),
                        ProjectName = c.String(nullable: false, maxLength: 100),
                        CurrencyId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 30),
                        CustomerID = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        consultant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Consultant", t => t.consultant_Id)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CurrencyId)
                .Index(t => t.CustomerID)
                .Index(t => t.consultant_Id);
            
            CreateTable(
                "dbo.Consultant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manager", t => t.ManagerId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        CurrencyName = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        AtlasID = c.String(),
                        CustomerName = c.String(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Consultant_Project_join",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        ConsultantId = c.Int(nullable: false),
                        ConsultantUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consultant", t => t.ConsultantId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.ConsultantId);
            
            CreateTable(
                "dbo.UnitOfMeasure",
                c => new
                    {
                        UnitOfMeasureId = c.Int(nullable: false, identity: true),
                        UnitOfMeasureStr = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 30),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UnitOfMeasureId);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Firstname = c.String(nullable: false, maxLength: 30),
                        Lastname = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ManagerPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManagerId = c.Int(nullable: false),
                        Customer = c.Boolean(nullable: false),
                        Project = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamConsultant",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Consultant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Consultant_Id })
                .ForeignKey("dbo.Team", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Consultant", t => t.Consultant_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Consultant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.EstimateHeader", "UnitOfMeasureId", "dbo.UnitOfMeasure");
            DropForeignKey("dbo.Consultant_Project_join", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Consultant_Project_join", "ConsultantId", "dbo.Consultant");
            DropForeignKey("dbo.EstimateHeader", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Project", "CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Project", "consultant_Id", "dbo.Consultant");
            DropForeignKey("dbo.TeamConsultant", "Consultant_Id", "dbo.Consultant");
            DropForeignKey("dbo.TeamConsultant", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.EstimateHeader", "EstimateTypeId", "dbo.EstimateType");
            DropForeignKey("dbo.EstimateHeader", "EstimateStatusId", "dbo.EstimateStatus");
            DropForeignKey("dbo.EstimateDetail", "LineItemId", "dbo.LineItem");
            DropForeignKey("dbo.LineItem", "LineItemGroupId", "dbo.LineItemTypeGroup");
            DropForeignKey("dbo.LineItemTypeGroup", "LineItemType", "dbo.LineItemType");
            DropForeignKey("dbo.EstimateDetail", "EstimateHeaderId", "dbo.EstimateHeader");
            DropForeignKey("dbo.EstimateHeader", "ContingencyDefaultId", "dbo.ContingencyDefault");
            DropForeignKey("dbo.EstimateHeader", "CommercialTypeId", "dbo.CommercialType");
            DropIndex("dbo.TeamConsultant", new[] { "Consultant_Id" });
            DropIndex("dbo.TeamConsultant", new[] { "Team_Id" });
            DropIndex("dbo.Consultant_Project_join", new[] { "ConsultantId" });
            DropIndex("dbo.Consultant_Project_join", new[] { "ProjectId" });
            DropIndex("dbo.Team", new[] { "ManagerId" });
            DropIndex("dbo.Project", new[] { "consultant_Id" });
            DropIndex("dbo.Project", new[] { "CustomerID" });
            DropIndex("dbo.Project", new[] { "CurrencyId" });
            DropIndex("dbo.LineItemTypeGroup", new[] { "LineItemType" });
            DropIndex("dbo.LineItem", new[] { "LineItemGroupId" });
            DropIndex("dbo.EstimateDetail", new[] { "LineItemId" });
            DropIndex("dbo.EstimateDetail", new[] { "EstimateHeaderId" });
            DropIndex("dbo.EstimateHeader", new[] { "EstimateStatusId" });
            DropIndex("dbo.EstimateHeader", new[] { "ContingencyDefaultId" });
            DropIndex("dbo.EstimateHeader", new[] { "CommercialTypeId" });
            DropIndex("dbo.EstimateHeader", new[] { "EstimateTypeId" });
            DropIndex("dbo.EstimateHeader", new[] { "UnitOfMeasureId" });
            DropIndex("dbo.EstimateHeader", new[] { "ProjectId" });
            DropTable("dbo.TeamConsultant");
            DropTable("dbo.Role");
            DropTable("dbo.ManagerPreferences");
            DropTable("dbo.Manager");
            DropTable("dbo.UnitOfMeasure");
            DropTable("dbo.Consultant_Project_join");
            DropTable("dbo.Customer");
            DropTable("dbo.Currency");
            DropTable("dbo.Team");
            DropTable("dbo.Consultant");
            DropTable("dbo.Project");
            DropTable("dbo.EstimateType");
            DropTable("dbo.EstimateStatus");
            DropTable("dbo.LineItemType");
            DropTable("dbo.LineItemTypeGroup");
            DropTable("dbo.LineItem");
            DropTable("dbo.EstimateDetail");
            DropTable("dbo.ContingencyDefault");
            DropTable("dbo.EstimateHeader");
            DropTable("dbo.CommercialType");
        }
    }
}
