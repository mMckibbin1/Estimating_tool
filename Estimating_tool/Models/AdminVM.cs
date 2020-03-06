using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Estimating_Tool.Models
{
	public class AdminVM
	{
		public virtual List<EstimateStatus> EStatus { get; set; }
		public virtual List<EstimateType> EType { get; set; }
		public virtual List<CommercialType> CType { get; set; }
		public virtual List<ContingencyDefault> CDefault { get; set; }
		public virtual List<LineItem> LItem { get; set; }
		public virtual List<LineItemType> LItemType { get; set; }
		public virtual List<LineItemTypeGroup> LItemTypeGroup { get; set; }
		public virtual List<Role> Role { get; set; }
		public IEnumerable<UnitOfMeasure> UnitOfMeasurement { get; set; }
	}
}