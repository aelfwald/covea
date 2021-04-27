using System;

namespace Covea.Rating.Domain
{
	public class RatingEngine : IRatingEngine
	{
		private readonly IGrossPremiumCalc _grossPremiumCalc;
		private readonly IInitialCommissionCalc _initialCommissionCalc;
		private readonly INetPremiumCalc _netPremiumCalc;
		private readonly IRenewalCommissionCalc _renewalCommissionCalc;
		private readonly IRiskPremiumCalc _riskPremiumCalc;
		private readonly IRiskRateCalc _riskRateCalc;

		public RatingEngine(
			IGrossPremiumCalc grossPremiumCalc,
			IInitialCommissionCalc initialCommissionCalc,
			INetPremiumCalc netPremiumCalc,
			IRenewalCommissionCalc renewalCommissionCalc,
			IRiskPremiumCalc riskPremiumCalc,
			IRiskRateCalc riskRateCalc
			)
		{
			_grossPremiumCalc = grossPremiumCalc ?? throw new ArgumentNullException(nameof(grossPremiumCalc));
			_initialCommissionCalc = initialCommissionCalc ?? throw new ArgumentNullException(nameof(initialCommissionCalc));
			_netPremiumCalc = netPremiumCalc ?? throw new ArgumentNullException(nameof(netPremiumCalc));
			_renewalCommissionCalc = renewalCommissionCalc ?? throw new ArgumentNullException(nameof(renewalCommissionCalc));
			_riskPremiumCalc = riskPremiumCalc ?? throw new ArgumentNullException(nameof(riskPremiumCalc));
			_riskRateCalc = riskRateCalc ?? throw new ArgumentNullException(nameof(riskPremiumCalc));
		}

		public bool RiskIsValid(int age, decimal sumAssured, RatingEngineDispatcher dispatcher)
		{
			bool validRisk = true;

			if (age < 18 || age > 65)
			{
				dispatcher.OnInvalidAge();
				validRisk = false;
			}

			if (sumAssured < 25000 || sumAssured > 500000)
			{
				dispatcher.OnInvalidSumAssured();
				validRisk = false;
			}

			return validRisk;

		}

		public void Run(int age, decimal sumAssured, RiskRateBands riskRateBands, RatingEngineDispatcher dispatcher)
		{

			if(!RiskIsValid(age, sumAssured, dispatcher))
			{
				return;
			}

			RiskRate riskRate = _riskRateCalc.GetRiskRate(age, sumAssured, riskRateBands);

			if (riskRate.NotAvailable)
			{
				dispatcher.OnNotAvailble();
				return;
			}

			bool miniumgrossPremiumMet = false;
			decimal adjustedSumAssured = sumAssured;
			decimal grossPremium;
			do
			{
				grossPremium = this.CalculateGrossPremium(adjustedSumAssured, riskRate);
				miniumgrossPremiumMet = grossPremium >= 2;
				if (!miniumgrossPremiumMet)
				{
					adjustedSumAssured += 5000;
				}
				
			} while (!miniumgrossPremiumMet);

			dispatcher.OnRiskCalculated(
				new RatingResult() { GrossPremium = grossPremium, SumAssured = adjustedSumAssured });

		}

		private decimal CalculateGrossPremium(decimal sumAssured, RiskRate riskRate)
		{
			decimal riskPremium = _riskPremiumCalc.Calculate(riskRate.Rate.Value, sumAssured);
			decimal renewalCommission = _renewalCommissionCalc.Calculate(riskPremium);
			decimal netPremium = _netPremiumCalc.Calculate(riskPremium, renewalCommission);
			decimal initialCommission = _initialCommissionCalc.Calculate(netPremium);
			decimal grossPremium = _grossPremiumCalc.Calculate(netPremium, initialCommission);
			return grossPremium;
		}
	}
}
