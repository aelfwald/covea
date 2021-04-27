using System;
using System.Collections.Generic;
using System.Linq;

namespace Covea.Rating.Domain
{
	public class RiskRateBands
	{
		List<RiskRate> _riskRates;

		private RiskRateBands(List<RiskRate> riskRates)
		{
			_riskRates = riskRates ?? throw new ArgumentNullException(nameof(riskRates));
		}

		public static RiskRateBands Create(List<RiskRate> riskRates)
		{
			return new RiskRateBands(riskRates);
		}

		public bool TryGet(decimal sumAssured, RiskAge riskAge, out RiskRate riskRate)
		{
			riskRate = _riskRates.FirstOrDefault( r => r.SumAssured == sumAssured && r.RiskAge == riskAge);
			return riskRate != null;
		}

		public bool TryGetUpperBound(decimal sumAssured, RiskAge riskAge, out RiskRate riskRate)
		{
			riskRate = _riskRates
				.Where(r => r.SumAssured > sumAssured && r.RiskAge == riskAge)
				.OrderBy(r => r.SumAssured)
				.FirstOrDefault();

			return riskRate != null;
		}

		public bool TryGetLowerBound(decimal sumAssured, RiskAge riskAge, out RiskRate riskRate)
		{
			riskRate = _riskRates
				.Where(r => r.SumAssured < sumAssured && r.RiskAge == riskAge)
				.OrderByDescending(r => r.SumAssured)
				.FirstOrDefault();

			return riskRate != null;
		}


	}
}
