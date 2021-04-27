using Covea.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public class InitialCommissionCalc : IInitialCommissionCalc
	{
		public decimal Calculate(decimal netPremium)
		{
			return Math.Round(netPremium * 2.03M, Rounding.DecimalPlaces);
		}
	}
}
