using System;

namespace Covea.Rating.Domain
{
	public sealed class UnRateableRiskRate : RiskRate
	{
		public override bool NotAvailable { get { return true; } }

		public override decimal? Rate { get { return null; } }

		public static UnRateableRiskRate Create(decimal sumAssured, RiskAge riskAge)
		{
			return new UnRateableRiskRate()
			{
				SumAssured = sumAssured,
				RiskAge = riskAge
			};
		}

	}
}
