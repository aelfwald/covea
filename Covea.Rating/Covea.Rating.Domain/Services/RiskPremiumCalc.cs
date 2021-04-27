using Covea.Framework;
using System;

namespace Covea.Rating.Domain
{
	public class RiskPremiumCalc : IRiskPremiumCalc
	{
		public decimal Calculate(decimal riskRate, decimal sumAssured)
		{
			return Math.Round(riskRate * (sumAssured / 1000), Rounding.DecimalPlaces);
		}
	}
}
