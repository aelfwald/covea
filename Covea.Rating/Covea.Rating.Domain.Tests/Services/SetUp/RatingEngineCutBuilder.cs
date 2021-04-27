using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covea.Rating.Domain
{
	internal class RatingEngineCutBuilder
	{
		Mock<IGrossPremiumCalc> _grossPremiumCalc = new Mock<IGrossPremiumCalc>();
		Mock<IInitialCommissionCalc> _initialCommissionCalc = new Mock<IInitialCommissionCalc>();
		Mock<INetPremiumCalc> _netPremiumCalc = new Mock<INetPremiumCalc>();
		Mock<IRenewalCommissionCalc> _renewalCommissionCalc = new Mock<IRenewalCommissionCalc>();
		Mock<IRiskPremiumCalc> _riskPremiumCalc = new Mock<IRiskPremiumCalc>();
		Mock<IRiskRateCalc> _riskRateCalc = new Mock<IRiskRateCalc>();

		internal RatingEngineCutBuilder WithRiskPremiumCalcReturns(
			decimal sumAssured,
			decimal riskPremium)
		{
			_riskPremiumCalc
				.Setup(m =>
					m.Calculate(
						It.IsAny<decimal>(),
						It.Is<decimal>(d => d == sumAssured)))
				.Returns(riskPremium);
			return this;
		}

		internal RatingEngineCutBuilder WithRenewalCommissionCalcReturns(
			decimal riskPremium,
			decimal initialCommission)
		{
			_renewalCommissionCalc
				.Setup(m =>
					m.Calculate(
						It.Is<decimal>(d => d == riskPremium)))
				.Returns(initialCommission);
			return this;
		}



		internal RatingEngineCutBuilder WithInitialCommissionCalcReturns(
			decimal netPremium,
			decimal initialCommission)
		{
			_initialCommissionCalc
				.Setup(m =>
					m.Calculate(
						It.Is<decimal>(d => d == netPremium)))
				.Returns(initialCommission);
			return this;
		}

		internal RatingEngineCutBuilder WithGrossPremiumCalcReturns(
			decimal netPremium,
			decimal initialCommission,
			decimal grossPremium)
		{
			_grossPremiumCalc
				.Setup(m =>
					m.Calculate(
						It.Is<decimal>(d => d == netPremium),
						It.Is<decimal>(d => d == initialCommission)))
				.Returns(grossPremium);
			return this;
		}

		internal RatingEngineCutBuilder WithNetPremiumCalcReturns(
			decimal riskPremium, 
			decimal renewalCommission,
			decimal netPremium)
		{
			_netPremiumCalc
				.Setup(m =>
					m.Calculate(
						It.Is<decimal>(d => d == riskPremium),
						It.Is<decimal>(d => d == renewalCommission)))
				.Returns(netPremium);
			return this;
		}


		internal RatingEngineCutBuilder WithRiskRateCalcReturns(RiskRate riskRate)
		{
			_riskRateCalc
				.Setup(m => 
					m.GetRiskRate(
						It.IsAny<int>(), 
						It.IsAny<decimal>(), 
						It.IsAny<RiskRateBands>()))
				.Returns(riskRate);
			return this;
		}


		internal RatingEngine Build()
		{
			return new RatingEngine(
				_grossPremiumCalc.Object,
				_initialCommissionCalc.Object,
				_netPremiumCalc.Object,
				_renewalCommissionCalc.Object,
				_riskPremiumCalc.Object,
				_riskRateCalc.Object);
		}


	}
}
