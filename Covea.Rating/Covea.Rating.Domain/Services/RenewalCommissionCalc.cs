using Covea.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public class RenewalCommissionCalc : IRenewalCommissionCalc
	{
		public decimal Calculate(decimal riskPremium)
		{
			return Math.Round( riskPremium * 0.03M, Rounding.DecimalPlaces);
		}
	}
}
