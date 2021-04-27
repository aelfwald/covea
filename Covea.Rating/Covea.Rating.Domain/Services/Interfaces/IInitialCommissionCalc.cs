using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public interface IInitialCommissionCalc
	{
		decimal Calculate(decimal netPremium);
	}
}
