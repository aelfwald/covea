using Covea.Rating.Application;
using Covea.Rating.Domain;
using System;
using System.Collections.Generic;

namespace Covea.Rating.Infrastructure
{
	public class RetrieveRiskRateBandsQuery : IRetrieveRiskRateBandsQuery
	{

		public RiskRateBands RunQuery()
		{

			List<RiskRate> riskRates =
				new List<RiskRate>
				{
					RateableRiskRate.Create(25000M, RiskAge.LessThanOrEqualToThirty, 0.0172M),
					RateableRiskRate.Create(50000M, RiskAge.LessThanOrEqualToThirty, 0.0165M),
					RateableRiskRate.Create(100000M, RiskAge.LessThanOrEqualToThirty, 0.0154M),
					RateableRiskRate.Create(200000M, RiskAge.LessThanOrEqualToThirty, 0.0147M),
					RateableRiskRate.Create(300000M, RiskAge.LessThanOrEqualToThirty, 0.0144M),
					RateableRiskRate.Create(500000M, RiskAge.LessThanOrEqualToThirty, 0.0146M),

					RateableRiskRate.Create(25000M, RiskAge.ThirtyOneToFifty, 0.1043M),
					RateableRiskRate.Create(50000M, RiskAge.ThirtyOneToFifty, 0.0999M),
					RateableRiskRate.Create(100000M, RiskAge.ThirtyOneToFifty, 0.0932M),
					RateableRiskRate.Create(200000M, RiskAge.ThirtyOneToFifty, 0.0887M),
					RateableRiskRate.Create(300000M, RiskAge.ThirtyOneToFifty, 0.0872M),
					UnRateableRiskRate.Create(500000M, RiskAge.ThirtyOneToFifty),

					RateableRiskRate.Create(25000M, RiskAge.GreaterThanFifty, 0.2677M),
					RateableRiskRate.Create(50000M, RiskAge.GreaterThanFifty, 0.2565M),
					RateableRiskRate.Create(100000M, RiskAge.GreaterThanFifty, 0.2393M),
					RateableRiskRate.Create(200000M, RiskAge.GreaterThanFifty, 0.2285M),
					UnRateableRiskRate.Create(300000M, RiskAge.GreaterThanFifty),
					UnRateableRiskRate.Create(500000M, RiskAge.GreaterThanFifty)
				};


			return RiskRateBands.Create(riskRates);


		}

	}
}
