using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	public interface IRatingEngine
	{
		void Run(int age, decimal sumAssured, RiskRateBands riskRateBands, RatingEngineDispatcher dispatcher);
	}
}
