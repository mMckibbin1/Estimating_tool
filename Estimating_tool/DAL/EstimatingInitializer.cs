using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Estimating_Tool.Models;

namespace Estimating_Tool.DAL
{
    public class EstimatingInitializer : System.Data.Entity.DropCreateDatabaseAlways<Estimatingcontext>
    /*
       [DropCreateDatabaseAlways] ---> set when changing initializer
       [DropCreateDatabaseIfModelChanges] ---> set normally - only rebuilds and drops database whenever the model physically changes
       if changing the initializer, please try to structure the same, as some of these need to load in this order because of their relationships
       i.e. customers loaded before projects
    */
    {
        protected override void Seed(Estimatingcontext context)
        {
            var customer = new List<Customer>
                {
                    new Customer{CustomerID=1, AtlasID=24587956.ToString(), CreatedBy="mccoooeyp", CreatedDate=DateTime.Parse("2019-01-25"),
                        CustomerName ="Sustainable Energy Authority of Ireland", ModifiedBy="mccooeyp",ModifiedDate=DateTime.Parse("2019-01-25"), IsActive = true},
                    new Customer{CustomerID=2, AtlasID=54875154.ToString(), CreatedBy="mccoooeyp", CreatedDate=DateTime.Parse("2019-01-25"),
                        CustomerName ="Local Government Management Authority", ModifiedBy="mccooeyp",ModifiedDate=DateTime.Parse("2019-01-25"), IsActive = true },
                    new Customer{CustomerID=3, AtlasID=54875421.ToString(), CreatedBy="mccoooeyp", CreatedDate=DateTime.Parse("2019-01-25"),
                        CustomerName ="Arnotts", ModifiedBy="mccooeyp",ModifiedDate=DateTime.Parse("2019-01-25") , IsActive = true},
                    new Customer { CustomerID = 4, AtlasID = 546985.ToString(), CreatedBy = "mccoooeyp", CreatedDate = DateTime.Parse("2019-01-25"),
                        CustomerName = "DAE", ModifiedBy = "mccooeyp", ModifiedDate = DateTime.Parse("2019-01-25"), IsActive = true },
                    new Customer { CustomerID = 5, AtlasID = 58745.ToString(), CreatedBy = "mccoooeyp", CreatedDate = DateTime.Parse("2019-01-25"),
                        CustomerName = "SOLAS", ModifiedBy = "mccooeyp", ModifiedDate = DateTime.Parse("2019-01-25"), IsActive = true},
                };
            customer.ForEach(n => context.Customer.Add(n));
            context.SaveChanges();

            var currency = new List<Currency>
            {
                new Currency{CurrencyId=1, CurrencyName="Sterling(£)", CreatedDate = DateTime.Parse("12/12/2012"),CreatedBy="Mark",ModifiedDate= DateTime.Parse("12/12/2012"),ModifiedBy="Mark", IsActive = true },
                new Currency{CurrencyId=2, CurrencyName="Euros(€)", CreatedDate = DateTime.Parse("12/12/2012"),CreatedBy="Mark",ModifiedDate= DateTime.Parse("12/12/2012"),ModifiedBy="Mark", IsActive = true}
            };
            currency.ForEach(n => context.Currency.Add(n));
            context.SaveChanges();

            var project = new List<Project>
            {
                new Project{ProjectId=1, AtlasID=1791.ToString(), CurrencyId=1 ,Rate=660,CustomerID=1,ProjectName="NAS",CreatedDate=DateTime.Parse("2019-01-25")
                ,CreatedBy="mccoooeyp",ModifiedDate=DateTime.Parse("2019-01-25"),ModifiedBy = "mccooeyp",IsActive = true, },
                new Project{ProjectId=2, AtlasID=2464.ToString(), CurrencyId=1, Rate=660 ,CustomerID=1,ProjectName="HES",CreatedDate=DateTime.Parse("2019-01-25")
                ,CreatedBy="mccoooeyp",ModifiedDate=DateTime.Parse("2019-01-25"),ModifiedBy = "mccooeyp" , IsActive =true,},
                new Project{ProjectId=3, AtlasID=5282.ToString(), CurrencyId=2, Rate=660 ,CustomerID=1,ProjectName="ECMS",CreatedDate=DateTime.Parse("2019-01-25")
                ,CreatedBy="mccoooeyp",ModifiedDate=DateTime.Parse("2019-01-25"),ModifiedBy = "mccooeyp", IsActive = true, },
                new Project{ProjectId=4, AtlasID=6262.ToString(), CurrencyId=1, Rate=660 ,CustomerID=2,ProjectName="TEST",CreatedDate=DateTime.Parse("2019-01-25")
                ,CreatedBy="mccoooeyp",ModifiedDate=DateTime.Parse("2019-01-25"),ModifiedBy = "mccooeyp", IsActive = true}
            };
            project.ForEach(n => context.Project.Add(n));
            context.SaveChanges();


            var CommercialType = new List<CommercialType>
            {
                new CommercialType{CommercialTypeId=1, CommercialTypeStr = "Fixed Price",CreatedDate = DateTime.Parse("12/12/2012"),CreatedBy="Matthew",ModifiedDate= DateTime.Parse("12/12/2012"),ModifiedBy="Matthew", IsActive = true},
                new CommercialType{CommercialTypeId=2, CommercialTypeStr = "Time & Materials",CreatedDate = DateTime.Parse("12/12/2012"),CreatedBy="Matthew",ModifiedDate= DateTime.Parse("12/12/2012"),ModifiedBy="Matthew",  IsActive = true}
            };
            CommercialType.ForEach(n => context.CommercialType.Add(n));
            context.SaveChanges();


            var ContingencyDefault = new List<ContingencyDefault>
            {
                new ContingencyDefault{ContingencyDefaultId=1, ContingencyDefaultInt=5, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=2, ContingencyDefaultInt=10, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=3, ContingencyDefaultInt=15, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=4, ContingencyDefaultInt=20, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=5, ContingencyDefaultInt=25, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=6, ContingencyDefaultInt=30, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=7, ContingencyDefaultInt=35, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=8, ContingencyDefaultInt=40, IsActive=true},
                new ContingencyDefault{ContingencyDefaultId=9, ContingencyDefaultInt=50, IsActive=true},

            };
            ContingencyDefault.ForEach(n => context.ContingencyDefault.Add(n));
            context.SaveChanges();


            var Role = new List<Role>
            {
                new Role{Id=1,RoleName="Admin", IsActive = true }
            };
            Role.ForEach(n => context.Role.Add(n));
            context.SaveChanges();


            var UnitOfMeaure = new List<UnitOfMeasure>
            {
                new UnitOfMeasure{UnitOfMeasureId=1, UnitOfMeasureStr="Hours", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true},
                new UnitOfMeasure{UnitOfMeasureId=2, UnitOfMeasureStr="Days", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true},
                new UnitOfMeasure{UnitOfMeasureId=3, UnitOfMeasureStr="Weeks", CreatedDate=DateTime.Parse("2019-03-19"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true},
                new UnitOfMeasure{UnitOfMeasureId=4, UnitOfMeasureStr="Months", CreatedDate=DateTime.Parse("2019-03-19"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true},
                new UnitOfMeasure{UnitOfMeasureId=5, UnitOfMeasureStr="Years", CreatedDate=DateTime.Parse("2019-03-19"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true}
            };
            UnitOfMeaure.ForEach(n => context.UnitOfMeasure.Add(n));
            context.SaveChanges();


            var lineItemType = new List<LineItemType>
            {
                new LineItemType{LineItemTypeId=1, LineItemTypeStr="General", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItemType{LineItemTypeId=2, LineItemTypeStr="Customer", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItemType{LineItemTypeId=3, LineItemTypeStr="Project", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItemType{LineItemTypeId=4, LineItemTypeStr="Team", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItemType{LineItemTypeId=5, LineItemTypeStr="Other", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true }

            };
            lineItemType.ForEach(n => context.LineItemType.Add(n));
            context.SaveChanges();


            var LineItemTypeGroup = new List<LineItemTypeGroup>
            {
                new LineItemTypeGroup{LineItemTypeGroupId=1, LineItemTypeGroupStr="Project Setup", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=2, LineItemTypeGroupStr="Development Work", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=3, LineItemTypeGroupStr="Communications", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=4, LineItemTypeGroupStr="Update Documentation", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=5, LineItemTypeGroupStr="Project Status Reports", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=12, LineItemTypeGroupStr="UAT", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=13, LineItemTypeGroupStr="Test in UAT", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=6, LineItemTypeGroupStr="Prep for go-live", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=7, LineItemTypeGroupStr="Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=8, LineItemTypeGroupStr="Rework", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=9, LineItemTypeGroupStr="Release to live", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=10, LineItemTypeGroupStr="Post GoLive actions", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true, LineItemType = 1},
                new LineItemTypeGroup{LineItemTypeGroupId=11, LineItemTypeGroupStr="ClientDemo", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Mark", IsActive=true, LineItemType = 2},          
                new LineItemTypeGroup{LineItemTypeGroupId=14, LineItemTypeGroupStr="Lessions Learned workshops", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Mark", IsActive=true, LineItemType = 2}

            };
            LineItemTypeGroup.ForEach(n => context.LineItemTypeGroup.Add(n));
            context.SaveChanges();


            var LineItem = new List<LineItem>
            {
                new LineItem{LineItemId=1, LineItemGroupId=1, LineItemStr="Download/Install software required for project", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=1, LineItemGroupId=1, LineItemStr="Branch Code", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=2, LineItemGroupId=1, LineItemStr="RFC Document Creation", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=3, LineItemGroupId=1, LineItemStr="Incident/Change Ticket creation", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=4, LineItemGroupId=1, LineItemStr="Create source control repository", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=5, LineItemGroupId=1, LineItemStr="Escrow Deposit", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=6, LineItemGroupId=1, LineItemStr="Client workshops", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=7, LineItemGroupId=1, LineItemStr="KT to other developers regarding spec", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=8, LineItemGroupId=1, LineItemStr="Dev/UAT database refresh and anonymisation", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=9, LineItemGroupId=2, LineItemStr="Create work items in the code repository ", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=10, LineItemGroupId=2, LineItemStr="Code", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=11, LineItemGroupId=2, LineItemStr="Logging", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=12, LineItemGroupId=2, LineItemStr="Database scripts", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=13, LineItemGroupId=2, LineItemStr="Reporting changes", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=14, LineItemGroupId=2, LineItemStr="SSIS package changes", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=15, LineItemGroupId=2, LineItemStr="New jobs on database", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=16, LineItemGroupId=2, LineItemStr="Updates to nightly extract", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=17, LineItemGroupId=3, LineItemStr="Phone calls", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=18, LineItemGroupId=3, LineItemStr="Emails", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=19, LineItemGroupId=3, LineItemStr="Meetings with client", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=20, LineItemGroupId=3, LineItemStr="Meetings with team, team or tech lead, sdm, etc…", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=21, LineItemGroupId=4, LineItemStr="Specification", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=22, LineItemGroupId=4, LineItemStr="FRD - Functional Requirement Document", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=23, LineItemGroupId=4, LineItemStr="CID - Configuration Item Diagram", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=24, LineItemGroupId=4, LineItemStr="Customer UAT Checklist", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=25, LineItemGroupId=5, LineItemStr="Just Once", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=26, LineItemGroupId=5, LineItemStr="Daily", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=27, LineItemGroupId=5, LineItemStr="Weekly", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=28, LineItemGroupId=5, LineItemStr="Monthly", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=31, LineItemGroupId=7, LineItemStr="Screenshots", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=32, LineItemGroupId=7, LineItemStr="Demo to client", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=33, LineItemGroupId=7, LineItemStr="Preparation for Demo to client", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=34, LineItemGroupId=7, LineItemStr="Document Test Cases", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=35, LineItemGroupId=7, LineItemStr="Training to client", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=36, LineItemGroupId=7, LineItemStr="Handover to other consultants", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=37, LineItemGroupId=13, LineItemStr="Path Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=38, LineItemGroupId=13, LineItemStr="Data Set Testting", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=39, LineItemGroupId=13, LineItemStr="Unit Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=40, LineItemGroupId=13, LineItemStr="System Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=41, LineItemGroupId=13, LineItemStr="Integration Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=42, LineItemGroupId=13, LineItemStr="Black-box Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=43, LineItemGroupId=13, LineItemStr="White-box Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=44, LineItemGroupId=13, LineItemStr="Regression Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=45, LineItemGroupId=13, LineItemStr="Automation Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=46, LineItemGroupId=13, LineItemStr="User Acceptance Testing", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=47, LineItemGroupId=13, LineItemStr="Software Performance", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=48, LineItemGroupId=7, LineItemStr="UAT Rework", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=49, LineItemGroupId=8, LineItemStr="UAT Queries", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=50, LineItemGroupId=8, LineItemStr="UAT Assistance", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=51, LineItemGroupId=8, LineItemStr="RAF doc", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=52, LineItemGroupId=6, LineItemStr="FSC/CAB", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=53, LineItemGroupId=6, LineItemStr="Customer Documents", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=53, LineItemGroupId=6, LineItemStr="Rollout Plan and Release Checklist", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=57, LineItemGroupId=9, LineItemStr="Merge code", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=58, LineItemGroupId=9, LineItemStr="Resolve conflicts", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=59, LineItemGroupId=9, LineItemStr="Test code following conflicts", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=60, LineItemGroupId=9, LineItemStr="Database backup", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=61, LineItemGroupId=9, LineItemStr="Code backup", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=62, LineItemGroupId=9, LineItemStr="Request snapshot creation/retention from Infrastructure Supplier", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=63, LineItemGroupId=9, LineItemStr="Request snapshot removal from Infrastructure Supplier", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=64, LineItemGroupId=9, LineItemStr="Offline pages", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=65, LineItemGroupId=9, LineItemStr="Stop services/Jobs", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=66, LineItemGroupId=12, LineItemStr="Deployment", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=66, LineItemGroupId=12, LineItemStr="Queries from UAT tester", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=66, LineItemGroupId=12, LineItemStr="Customer Issue Tracking Management(for example: Jira, Bugzilla, Mantis….) ", CreatedDate=DateTime.Parse("2019 -01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=66, LineItemGroupId=9, LineItemStr="Deployment", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=67, LineItemGroupId=9, LineItemStr="Eventual Rollback", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=68, LineItemGroupId=9, LineItemStr="Testing by developer", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=69, LineItemGroupId=9, LineItemStr="Testing by client", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=70, LineItemGroupId=9, LineItemStr="Client communications", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=71, LineItemGroupId=10, LineItemStr="Updates to DR environment", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=72, LineItemGroupId=10, LineItemStr="Website back online", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=73, LineItemGroupId=10, LineItemStr="Start services/jobs", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=74, LineItemGroupId=10, LineItemStr="Clean up database backups - delete unnecessary ones ", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=75, LineItemGroupId=10, LineItemStr="Close off FSC", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=76, LineItemGroupId=10, LineItemStr="Close off Ticket", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=77, LineItemGroupId=10, LineItemStr="Monitoring the release", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=77, LineItemGroupId=14, LineItemStr="Post Implementation Review Doc", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=79, LineItemGroupId=14, LineItemStr="Internal", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true },
                new LineItem{LineItemId=78, LineItemGroupId=11, LineItemStr="Client Demo", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true }
            };
            LineItem.ForEach(n => context.LineItem.Add(n));
            context.SaveChanges();
        

            var EstimateStatus = new List<EstimateStatus>
            {
                new EstimateStatus{EstimateStatusId=1, EstimateStatusStr="New", IsActive=true},
                new EstimateStatus{EstimateStatusId=2, EstimateStatusStr="Work in Progress", IsActive=true},
                new EstimateStatus{EstimateStatusId=3, EstimateStatusStr="For Review", IsActive=true},
                new EstimateStatus{EstimateStatusId=4, EstimateStatusStr="Reviewed", IsActive=true},
                new EstimateStatus{EstimateStatusId=5, EstimateStatusStr="Rejected", IsActive=true},
                new EstimateStatus{EstimateStatusId=6, EstimateStatusStr="Approved", IsActive=true},
                new EstimateStatus{EstimateStatusId=7, EstimateStatusStr="Complete", IsActive=true},
                new EstimateStatus{EstimateStatusId=8, EstimateStatusStr="Sent to Customer", IsActive=true},
                new EstimateStatus{EstimateStatusId=9, EstimateStatusStr="Test is Not Active", IsActive=true}
            };
            EstimateStatus.ForEach(n => context.EstimateStatus.Add(n));
            context.SaveChanges();


            var EstimateType = new List<EstimateType>
            {
                new EstimateType{EstimateTypeId=1, EstimateTypeStr="RFC", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true},
                new EstimateType{EstimateTypeId=2, EstimateTypeStr="Small Project", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true},
                new EstimateType{EstimateTypeId=3, EstimateTypeStr="Large Project", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Patrick", IsActive=true}
            };
            EstimateType.ForEach(n => context.EstimateType.Add(n));
            context.SaveChanges();


            var estimateHeader = new List<EstimateHeader>
            {
                new EstimateHeader{EstimateHeaderId=1, CustomerId=1, ProjectId=1, EstimateName="test", UnitOfMeasureId=1, EstimateTypeId=1, CommercialTypeId=1, ContingencyDefaultId=1, EstimateStatusId=1, CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Patrick",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Mark" ,IsActive = true},
                new EstimateHeader{EstimateHeaderId=2, CustomerId=1, ProjectId=2, EstimateName="test2", UnitOfMeasureId=3, EstimateTypeId=1, CommercialTypeId=1, ContingencyDefaultId=1, EstimateStatusId=1, CreatedDate=DateTime.Parse("2019-03-19"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-03-19"), ModifiedBy="Mark" , IsActive = true},
            };
            estimateHeader.ForEach(s => context.EstimateHeader.Add(s));
            context.SaveChanges();


            var estimateDetail = new List<EstimateDetail>
            {
                new EstimateDetail{EstimateDetailId=1, EstimateHeaderId=1, LineItemId=1, Estimate=1.1M, Note="test", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Mark" },
                new EstimateDetail{EstimateDetailId=2, EstimateHeaderId=1, LineItemId=10, Estimate=1.1M, Note="this is a testing note", CreatedDate=DateTime.Parse("2019-01-25"), CreatedBy="Mark",
                    ModifiedDate =DateTime.Parse("2019-01-25"), ModifiedBy="Mark" }
            };
            estimateDetail.ForEach(s => context.EstimateDetail.Add(s));
            context.SaveChanges();

			//initial Manager seed data
			var Managers = new List<Manager>
			{
				new Manager{Firstname="Helen", Lastname="Cole", Username="V1\\coleh" },
				new Manager{Firstname="Keith", Lastname="Jones", Username="V1\\joneskei" },
				new Manager{Firstname="Orlaith", Lastname="Carey", Username="V1\\careyo" },
				new Manager{Firstname="Patrick", Lastname="McCooey", Username="V1\\mccooeyp"},
				new Manager{Firstname="Mark", Lastname="Davidson", Username="V1\\davidsonm"},
				new Manager{Firstname="Matthew", Lastname="McKibbin", Username="V1\\mckibbinm"},
				new Manager{Firstname="Conor", Lastname="McKibbin", Username="V1\\mckibbinc"},


			};
			Managers.ForEach(i => context.Managers.Add(i));
			context.SaveChanges();


			var ManagerPreferences = new List<ManagerPreferences>
			{
				new ManagerPreferences{ManagerId = 1},
				new ManagerPreferences{ManagerId = 2},
				new ManagerPreferences{ManagerId = 3},
				new ManagerPreferences{ManagerId = 4}

			};
			ManagerPreferences.ForEach(i => context.Preferences.Add(i));
			context.SaveChanges();
			//initial Consultant seed data
			var Consultants = new List<Consultant>
			{
				new Consultant{Firstname="Robert", Lastname="Twyford", Username="V1\\Twyfordr", ManagerId=2},
				new Consultant{Firstname="Swathi", Lastname="Senthil-Kumar", Username="V1\\KumarS", ManagerId=2},
				new Consultant{Firstname="Mallory", Lastname="McConville", Username="V1\\MCConvilleM", ManagerId=2},
				new Consultant{Firstname="Alberto", Lastname="Hortiguela", Username="V1\\HordiguelaA", ManagerId=2},
				new Consultant{Firstname="Dragomir", Lastname="Donkov", Username="V1\\DonkovD", ManagerId=2},
				new Consultant{Firstname="Fabio", Lastname="Parente", Username="V1\\ParenteF", ManagerId=2},
				new Consultant{Firstname="Boris", Lastname="Hubner", Username="V1\\HubnerB", ManagerId=2},
				new Consultant{Firstname="Shane", Lastname="Bourke", Username="V1\\BourheS", ManagerId=1},
				new Consultant{Firstname="John", Lastname="Cronogue", Username="V1\\CronogueJ", ManagerId=1},
				new Consultant{Firstname="Anke", Lastname="Tabaku", Username="V1\\TabakuA", ManagerId=2},
				new Consultant{Firstname="Francois", Lastname="Malgreve", Username="V1\\MalgreveF", ManagerId=1},
				new Consultant{Firstname="Aastha", Lastname="Hissaria", Username="V1\\HissariaA", ManagerId=2},
				new Consultant{Firstname="Stuart", Lastname="Retson", Username="V1\\RetsonS", ManagerId=2},
				new Consultant{Firstname="Victoria", Lastname="Kenny", Username="V1\\KennyV", ManagerId=1},
				new Consultant{Firstname="Gavin", Lastname="Doherty", Username="V1\\DohertyG", ManagerId=2},
				new Consultant{Firstname="Nigel", Lastname="Liggett", Username="V1\\LiggettN", ManagerId=2},
				new Consultant{Firstname="Michael", Lastname="Brooks", Username="V1\\BrooksM", ManagerId=1},
				new Consultant{Firstname="Bhaswar", Lastname="Dutta", Username="V1\\DuttaB", ManagerId=1},
				new Consultant{Firstname="Donal", Lastname="Devine", Username="V1\\DevineD", ManagerId=2},
				new Consultant{Firstname="Andre", Lastname="Leite", Username="V1\\LeiteA", ManagerId=1},
				new Consultant{Firstname="Simi", Lastname="Sebastian", Username="V1\\SebastianS", ManagerId=1},
				new Consultant{Firstname="Vaishali", Lastname="Kothandan", Username="V1\\KothandanV", ManagerId=1},
				new Consultant{Firstname="Hayleigh", Lastname="Wronski", Username="V1\\WronskiH", ManagerId=1},
				new Consultant{Firstname="Alan", Lastname="Paladini", Username="V1\\PaladiniA", ManagerId=1},
				new Consultant{Firstname="Gustavo", Lastname="Rocha", Username="V1\\RochaG", ManagerId=2},
				new Consultant{Firstname="Adam", Lastname="McGready", Username="V1\\McGreadyA", ManagerId=3},
				new Consultant{Firstname="Arjun", Lastname="Ramesh", Username="V1\\RameshA", ManagerId=3},
				new Consultant{Firstname="Carol", Lastname="Deegan", Username="V1\\DeeganC", ManagerId=3},
				new Consultant{Firstname="Chris", Lastname="Monyihan", Username="V1\\MonyihanC", ManagerId=3},
				new Consultant{Firstname="Conor", Lastname="Byrne", Username="V1\\ByrneC", ManagerId=3},
				new Consultant{Firstname="David", Lastname="OConnell", Username="V1\\OConnellD", ManagerId=3},
				new Consultant{Firstname="Eoin", Lastname="Maher", Username="V1\\MaherE", ManagerId=3},
				new Consultant{Firstname="Hamzah", Lastname="Hassan", Username="V1\\HassanH", ManagerId=3},
				new Consultant{Firstname="Jean", Lastname="Loughran", Username="V1\\LoughranJ", ManagerId=3},
				new Consultant{Firstname="Odhran", Lastname="OMaoileidigh", Username="V1\\OMaoileidighO", ManagerId=4},
				new Consultant{Firstname="Suzanne", Lastname="Hennessy", Username="V1\\HennessyS", ManagerId=4},
				new Consultant{Firstname="Tim", Lastname="Leung", Username="V1\\LeungT", ManagerId=4},
                new Consultant{Firstname="conor2",Lastname="McKibbin",Username="V1\\MckibbinC",ManagerId=4 },
                new Consultant{Firstname="Matthew",Lastname="McKibbin",Username="V1\\MckibbinM",ManagerId=4 },
                new Consultant{Firstname="Mark",Lastname="Davidson", Username="V1\\DavidsonM", ManagerId=4}
			};
			Consultants.ForEach(i => context.Consultants.Add(i));
			context.SaveChanges();

			var Teams = new List<Team>
			{
				new Team{TeamName=".NET Services 2", ManagerId = 1},
				new Team{TeamName = "ERP L1", ManagerId = 4 }
			};
			Teams.ForEach(i => context.Teams.Add(i));
			context.SaveChanges();
		}
    }
}