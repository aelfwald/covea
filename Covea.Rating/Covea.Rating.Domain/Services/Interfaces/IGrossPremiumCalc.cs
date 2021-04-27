using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public interface IGrossPremiumCalc
	{
		decimal Calculate(decimal netPremium, decimal initialCommission);
	}
}
