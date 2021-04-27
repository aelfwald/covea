using Covea.Framework;
using System;

namespace Covea.Rating.Domain
{
	public class RiskRateCalc : IRiskRateCalc
	{
		public RiskRate GetRiskRate(int age, decimal sumAssured, RiskRateBands riskRates)
		{
			RiskAge riskAge = (RiskAge) age;
			RiskRate riskRate;
			if (riskRates.TryGet(sumAssured, riskAge, out riskRate))
			{		
				return riskRate;

			};

			RiskRate lowerRiskRate;
			RiskRate upperRiskRate;

			if (!riskRates.TryGetLowerBound(sumAssured, riskAge, out lowerRiskRate))
			{
				throw new Exception("Invalid sum assured.");
			}

			if (!riskRates.TryGetUpperBound(sumAssured, riskAge, out upperRiskRate))
			{
				throw new Exception("Invalid sum assured.");
			}

			if(lowerRiskRate.NotAvailable || upperRiskRate.NotAvailable)
			{
				return UnRateableRiskRate.Create(sumAssured, riskAge);
			}

			decimal riskRateValue = ((sumAssured - lowerRiskRate.SumAssured)/(upperRiskRate.SumAssured - lowerRiskRate.SumAssured) * upperRiskRate.Rate.Value 
				+ (upperRiskRate.SumAssured - sumAssured) / (upperRiskRate.SumAssured - lowerRiskRate.SumAssured) * lowerRiskRate.Rate.Value);

			return RateableRiskRate.Create(sumAssured, riskAge, riskRateValue);
		}
	}
}
