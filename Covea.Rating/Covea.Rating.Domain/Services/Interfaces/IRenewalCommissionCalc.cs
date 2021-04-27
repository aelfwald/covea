using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public interface IRenewalCommissionCalc
	{
		decimal Calculate(decimal riskPremium);
	}
}
