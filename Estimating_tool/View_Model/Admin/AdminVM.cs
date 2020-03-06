using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model.Admin
{
	public class AdminVM
	{
		public IEnumerable<UnitofMeasure> unitofMeasures { get; set; }
		public IEnumerable<ContingencyDefault> contingencyDefaults { get; set; }
		public IEnumerable<CommercialType> commercialTypes { get; set; }
		public IEnumerable<Role> roles { get; set; }
		public IEnumerable<LineItemType> lineItemTypes { get; set; }
        public IEnumerable<LineItem> lineItems { get; set; }

	}
}