using Covea.Rating.Domain;
using System;

namespace Covea.Rating.Application
{
	public interface IRetrieveRiskRateBandsQuery
	{
		RiskRateBands RunQuery();
	}
}
