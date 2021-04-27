using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public abstract class RiskRate
	{
		public abstract bool NotAvailable { get; }
		public decimal SumAssured { get; protected set; }
		public RiskAge RiskAge { get; protected set; }
		public virtual decimal? Rate { get; protected set; }
	}
}
