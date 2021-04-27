using Covea.Rating.Application;
using Covea.Rating.Domain;
using Covea.Rating.Infrastructure;
using System;

namespace Covea.Rating.DI
{
	public class Resolver
	{
		public static RatingService ResolveRatingService()
		{
			return new RatingService(
				new RatingEngine(
					new GrossPremiumCalc(),
					new InitialCommissionCalc(),
					new NetPremiumCalc(),
					new RenewalCommissionCalc(),
					new RiskPremiumCalc(),
					new RiskRateCalc()),
				new RetrieveRiskRateBandsQuery());
		}
	}
}
