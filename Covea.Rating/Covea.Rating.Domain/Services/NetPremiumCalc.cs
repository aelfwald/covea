using Covea.Framework;
using System;

namespace Covea.Rating.Domain
{
	public class NetPremiumCalc : INetPremiumCalc
	{
		public decimal Calculate(decimal riskPremium, decimal renewalCommisson)
		{
			return Math.Round(riskPremium + renewalCommisson, Rounding.DecimalPlaces);
		}
	}
}
