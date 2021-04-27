using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public sealed class RateableRiskRate : RiskRate
	{
		public override bool NotAvailable { get { return false; } }

		public static RiskRate Create(decimal sumAssured, RiskAge riskAge, decimal rate)
		{
			return new RateableRiskRate
			{
				Rate = rate,
				SumAssured = sumAssured,
				RiskAge = riskAge
			};
		}
	}
}
