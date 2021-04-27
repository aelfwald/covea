using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public class GrossPremiumCalc : IGrossPremiumCalc
	{
		public decimal Calculate(decimal netPremium, decimal initialCommission)
		{
			return netPremium + initialCommission;
		}
	}
}
