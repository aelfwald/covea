using System;

namespace Covea.Rating.Domain
{
	public interface IRiskPremiumCalc
	{
		decimal Calculate(decimal riskRate, decimal sumAssured);
	}
}
