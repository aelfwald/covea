using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public interface INetPremiumCalc
	{
		decimal Calculate(decimal riskPremium, decimal renewalCommisson);
	}
}
