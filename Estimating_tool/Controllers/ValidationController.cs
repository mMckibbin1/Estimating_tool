using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Estimating_Tool.DAL;

namespace Estimating_Tool.Controllers
{
	public class ValidationController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		[AllowAnonymous]
		public JsonResult ProjectNameValidation(string ProjectName, int? ProjectId)// project name validation for both create and edit pages
		{
           ProjectName = ProjectName.ToUpper().Trim();
			if (!ProjectId.HasValue)
			{
                if (db.Project.Any(x => x.ProjectName.ToUpper() == ProjectName))
                {
                    return Json("Project Name already used", JsonRequestBehavior.AllowGet);
                }
			}
            else
            {
                var CurrentProName = db.Project.Where(x => x.IsActive == true).Where(x => x.ProjectId == ProjectId).Select(x => x.ProjectName).FirstOrDefault();
                if (db.Project.Any(x => x.ProjectName.ToUpper() == ProjectName) && ProjectName != CurrentProName.ToUpper())
                {
                    return Json("Project Name must be Unique or be current stored Project name " + CurrentProName, JsonRequestBehavior.AllowGet);
                }
            }
			return Json(true, JsonRequestBehavior.AllowGet);

		}
		[AllowAnonymous]
		public JsonResult CheckID(int CustomerId)//validating CustomerID 
		{
			if (db.Customer.Any(x => x.CustomerID == CustomerId))
			{
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			var link = Content("<a href ='/Customers/Create'>Customer Does not exist Create Customer</a>");
			return Json(link.Content, JsonRequestBehavior.AllowGet);
		}

		[AllowAnonymous]
		public JsonResult AtlasIDProjectValidation(string AtlasID, int? ProjectId)// testing that Atlas ID for a project is unique
		{
            if (!string.IsNullOrEmpty(AtlasID))
            {
                AtlasID = AtlasID.Trim();
                if (!ProjectId.HasValue)
                {
                    if (db.Project.Any(x => x.AtlasID == AtlasID))
                    {
                        return Json("Atlas ID already exist, Atlas ID must be Unique", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var CurrentProAtlasId = db.Project.Where(x => x.IsActive == true).Where(x => x.ProjectId == ProjectId).Select(x => x.AtlasID).FirstOrDefault();
                    if (db.Project.Any(x => x.AtlasID == AtlasID) && AtlasID != CurrentProAtlasId)
                    {
                        return Json("Atlas ID must be Unique or be the current stored ID " + CurrentProAtlasId, JsonRequestBehavior.AllowGet);
                    }
                }
            }
			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[AllowAnonymous]
		public JsonResult CustomerNameValidation(int CustomerID)//ensuring that customername entered on project create page exists
		{
			if (!db.Customer.Any(x => x.CustomerID == CustomerID))
			{
				return Json("Customer does not exist", JsonRequestBehavior.AllowGet);
			}
			return Json(true, JsonRequestBehavior.AllowGet);
		}


        //Look Up Tables
        [AllowAnonymous]
        public JsonResult CommercialTypeNameUnique(string CommercialTypeStr, int? CommercialTypeId)//ensuring that commercialType String is unique
        {
            CommercialTypeStr = CommercialTypeStr.ToUpper().Trim();
            if (!CommercialTypeId.HasValue)
            {
                if (db.CommercialType.Any(x => x.CommercialTypeStr.ToUpper() == CommercialTypeStr))
                {
                    return Json("Commercial Type Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentCommercialTypeStr = db.CommercialType.Where(x => x.CommercialTypeId == CommercialTypeId).Select(x => x.CommercialTypeStr).FirstOrDefault();
                if (CommercialTypeStr != CurrentCommercialTypeStr.ToUpper() && db.CommercialType.Any(x => x.CommercialTypeStr.ToUpper() == CommercialTypeStr))
                {
                    return Json("Commercial Type must be Unique or the one currently saved " +CurrentCommercialTypeStr, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        //ensuring that the estimate status is unique
        [AllowAnonymous]
        public JsonResult EstimateStatusNameUnique(string EstimateStatusStr, int? EstimateStatusId)
        {
            EstimateStatusStr = EstimateStatusStr.ToUpper().Trim();
            if (!EstimateStatusId.HasValue)
            {
                if (db.EstimateStatus.Any(x => x.EstimateStatusStr.ToUpper() == EstimateStatusStr))
                {
                    return Json("Estimate Status Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentEstimateStatus = db.EstimateStatus.Where(x => x.EstimateStatusId == EstimateStatusId).Select(x => x.EstimateStatusStr).FirstOrDefault();
                if (db.EstimateStatus.Any(x => x.EstimateStatusStr.ToUpper() == EstimateStatusStr && EstimateStatusStr != CurrentEstimateStatus.ToUpper()))
                {
                    return Json("Estimate Status must be unique or the one currently saved: " + CurrentEstimateStatus, JsonRequestBehavior.AllowGet);
                }
               
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult RoleNameValidation(string RoleName, int? Id)//Testing role anem to enure it is unique 
        {
            RoleName = RoleName.ToUpper().Trim();

            if (!Id.HasValue)
            {
                if (db.Role.Any(x => x.RoleName.ToUpper() == RoleName))
                {
                    return Json("Role Name Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentRoleName = db.Role.Where(x => x.Id == Id).Select(x => x.RoleName).FirstOrDefault();
                if (db.Role.Any(x => x.RoleName.ToUpper() == RoleName) && CurrentRoleName.ToUpper() != RoleName)
                {
                    return Json("Role Name must be Unique or the value currently saved: " + CurrentRoleName, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult ContingencyDefaultValidation(int ContingencyDefaultInt, int? ContingencyDefaultId)//ensuring ContingencyDefaultInt is unique
        {
            if (!ContingencyDefaultId.HasValue)
            {
                if (db.ContingencyDefault.Any(x =>x.ContingencyDefaultInt == ContingencyDefaultInt))
                {
                    return Json("Contingency Default Must be Unqiue", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentContigencyDefault = db.ContingencyDefault.Where(x => x.ContingencyDefaultId == ContingencyDefaultId).Select(x =>x.ContingencyDefaultInt).FirstOrDefault();
                if (db.ContingencyDefault.Any(x => x.ContingencyDefaultInt == ContingencyDefaultInt) && CurrentContigencyDefault != ContingencyDefaultInt)
                {
                    return Json("Contigency Default Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult LineItemValidationName(string LineItemStr, int? LineItemId)//Line Item validation to ensure that LineItemStr is Unique
        {
            LineItemStr = LineItemStr.ToUpper().Trim();

            if (!LineItemId.HasValue)
            {
                if (db.LineItem.Any(x => x.LineItemStr.ToUpper() == LineItemStr))
                {
                    return Json("Line Item Name Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentLineItem = db.LineItem.Where(x => x.LineItemId == LineItemId).Select(x => x.LineItemStr).FirstOrDefault();
                if (db.LineItem.Any(x => x.LineItemStr.ToUpper() == LineItemStr)&& CurrentLineItem.ToUpper() != LineItemStr)
                {
                    return Json("Line Item Name Must be Unique or the one currently Saved: " + CurrentLineItem,JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult LineItemTypeValidation(string LineItemTypeStr, int? LineItemTypeId)//Validation for LineItemTypeStr to ensure it is unique
        {
            LineItemTypeStr = LineItemTypeStr.ToUpper().Trim();
            if (!LineItemTypeId.HasValue)
            {
                if (db.LineItemType.Any(x => x.LineItemTypeStr.ToUpper() == LineItemTypeStr))
                {
                    return Json("Line Item Type Name Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentLineItemTypeName = db.LineItemType.Where(x => x.LineItemTypeId == LineItemTypeId).Select(x => x.LineItemTypeStr).FirstOrDefault();
                if(db.LineItemType.Any(x => x.LineItemTypeStr.ToUpper() == LineItemTypeStr) && CurrentLineItemTypeName.ToUpper() != LineItemTypeStr)
                {
                    return Json("Line Item Type Name must be Unique or the one currently Saved: " + CurrentLineItemTypeName, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult LineItemTypeGroupValidation(string LineItemTypeGroupStr,int? LineItemTypeGroupId)// Validation on LineItemTypeGroupStr to ensure it is unique
       {
            LineItemTypeGroupStr = LineItemTypeGroupStr.ToUpper().Trim();
            if (!LineItemTypeGroupId.HasValue)
            {
                if (db.LineItemTypeGroup.Any(x => x.LineItemTypeGroupStr.ToUpper() == LineItemTypeGroupStr))
                {
                    return Json("Line Item Type Group Name Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentLineItemTypeGroupStr = db.LineItemTypeGroup.Where(x => x.LineItemTypeGroupId == LineItemTypeGroupId).Select(x => x.LineItemTypeGroupStr).FirstOrDefault();
                if (db.LineItemTypeGroup.Any(x => x.LineItemTypeGroupStr.ToUpper() == LineItemTypeGroupStr) && CurrentLineItemTypeGroupStr.ToUpper() != LineItemTypeGroupStr)
                {
                    return Json("Unit of Measure name Must be Unique or the one currently saved " + CurrentLineItemTypeGroupStr, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult UnitOfMeasureValidation(string UnitOfMeasureStr, int? UnitOfMeasureId)//ensuring that Unit of Measure str is unique
        {
            UnitOfMeasureStr = UnitOfMeasureStr.ToUpper().Trim();
            if (! UnitOfMeasureId.HasValue)
            {
                if (db.UnitOfMeasure.Any(x => x.UnitOfMeasureStr.ToUpper() == UnitOfMeasureStr))
                {
                    return Json("Unit of Measure name Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentUnitOfMeasureStr = db.UnitOfMeasure.Where(x => x.UnitOfMeasureId == UnitOfMeasureId).Select(x => x.UnitOfMeasureStr).FirstOrDefault();
                if (db.UnitOfMeasure.Any(x => x.UnitOfMeasureStr.ToUpper() == UnitOfMeasureStr)&& CurrentUnitOfMeasureStr.ToUpper() != UnitOfMeasureStr)
                {
                    return Json("Unit of Measure name Must be Unique or the one currently saved "+CurrentUnitOfMeasureStr, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult CurrencyValidation(string CurrencyName, int? CurrencyId)//ensuring that Currency Name is unique
        {
            CurrencyName = CurrencyName.ToUpper().Trim();
            if (!CurrencyId.HasValue)
            {
                if (db.Currency.Any(x => x.CurrencyName.ToUpper() == CurrencyName))
                {
                    return Json("Currency name must be unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentCurrencyName = db.Currency.Where(x => x.CurrencyId == CurrencyId).Select(x => x.CurrencyName).FirstOrDefault();
                if (db.Currency.Any(x => x.CurrencyName.ToUpper() == CurrencyName) && CurrentCurrencyName.ToUpper() != CurrencyName)
                {
                    return Json("Currency Name must be unique or the one currently saved " + CurrentCurrencyName, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult EstimateTypeValidation(string EstimateTypeStr, int? EstimateTypeId)// ensuring that Estimate Type Str is unique
        {
            EstimateTypeStr = EstimateTypeStr.ToUpper().Trim();
            if (!EstimateTypeId.HasValue)
            {
                if (db.EstimateType.Any(x => x.EstimateTypeStr.ToUpper() == EstimateTypeStr))
                {
                    return Json("Estimate Type Name Must be Unique", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var CurrentEstimateTypeStr = db.EstimateType.Where(x => x.EstimateTypeId == EstimateTypeId).Select(x => x.EstimateTypeStr).FirstOrDefault();
                if (db.EstimateType.Any(x => x.EstimateTypeStr.ToUpper() == EstimateTypeStr)&& CurrentEstimateTypeStr.ToUpper() != EstimateTypeStr)
                {
                    return Json("Estimate Type Name Must be unique or the one currently Saved: " + CurrentEstimateTypeStr, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult CustomerAtlasValidation(string AtlasID,int? CustomerID)// Ensure that customer Atlas ID is unqiue to customer
        {
            if (!string.IsNullOrEmpty(AtlasID))
            {
               AtlasID = AtlasID.Trim();
                if (!CustomerID.HasValue)//will not have a vaule if validation being called from create page.
                {
                    if (db.Customer.Any(x => x.AtlasID == AtlasID))
                    {
                        return Json("AtlasID already Used, Must be Unique", JsonRequestBehavior.AllowGet);
                    }
                }
                else//CustomerID will have a value from the edit customer page 
                {
                    var CurrentCustomerAtlasID = db.Customer.Where(x => x.CustomerID == CustomerID).Select(x => x.AtlasID).FirstOrDefault();
                    if (db.Customer.Any(x => x.AtlasID == AtlasID && CurrentCustomerAtlasID != AtlasID))
                    {
                        return Json("AtlasID Must be Unique or the one currently Saved: " + CurrentCustomerAtlasID, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult CustomerNamesValidation(string CustomerName, int? CustomerID)
        {
            if (!string.IsNullOrEmpty(CustomerName))
            {
                CustomerName = CustomerName.Trim().ToUpper();
                if (!CustomerID.HasValue)//will not have a vaule if validation being called from create page.
                {
                    if (db.Customer.Any(x => x.CustomerName.ToUpper() == CustomerName))
                    {
                        return Json("Customer Name already Used, Must be Unique", JsonRequestBehavior.AllowGet);
                    }
                }
                else//CustomerID will have a value from the edit customer page 
                {
                    var CurrentCustomerName = db.Customer.Where(x => x.CustomerID == CustomerID).Select(x => x.CustomerName).FirstOrDefault();
                    if (db.Customer.Any(x => x.CustomerName.ToUpper() == CustomerName && CurrentCustomerName != CustomerName))
                    {
                        return Json("AtlasID Must be Unique or the one currently Saved: " + CurrentCustomerName, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DurationCheck(decimal? Duration)
        {
           string strDuration = Duration.ToString();
            Regex regex = new Regex("^[0-9]*(\\.[0-9]{1,2})?$");
            if (regex.IsMatch(strDuration))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("Duration must be a number with upto to deciaml places",JsonRequestBehavior.AllowGet);

            
        }

    }
}