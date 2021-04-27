using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class RiskRateCalcTests
	{
		[Fact]
		public void When_age_and_sum_assured_match_defined_risk_rate_then_risk_rate_is_returned()
		{

			//Arrange
			RiskRate riskRate = RateableRiskRate.Create(25000, RiskAge.LessThanOrEqualToThirty, 0.5M);

			RiskRateBands riskRateBands = RiskRateBands.Create(
				new List<RiskRate>
				{
					riskRate
				});
			RiskRateCalc cut = new RiskRateCalc();

			//Act
			RiskRate result = cut.GetRiskRate(20, 25000, riskRateBands) ;

			//Assert
			result.Should().Be(riskRate);

		}

		public static IEnumerable<object[]> RiskRateCalcTestParameters
		{
			get
			{
				yield return new object[] { 30000, 0.01706 };
				yield return new object[] { 40000, 0.01678 };
				yield return new object[] { 125000, 0.015225 };
				yield return new object[] { 250000, 0.01455 };
				yield return new object[] { 500000, 0.0146 };
			}
		}

		[Theory()]
		[MemberData(nameof(RiskRateCalcTestParameters))]
		public void When_age_and_sum_assured_do_not_match_risk_rate_then_calculated_risk_rate_is_returned(
			decimal sumAssured,
			decimal expectedRiskRateValue)
		{

			//Arrange

			RiskRateBands riskRateBands = RiskRateBands.Create(
				new List<RiskRate>
				{
					RateableRiskRate.Create(25000, RiskAge.LessThanOrEqualToThirty, 0.0172M),
					RateableRiskRate.Create(50000, RiskAge.LessThanOrEqualToThirty, 0.0165M),
					RateableRiskRate.Create(100000, RiskAge.LessThanOrEqualToThirty, 0.0154M),
					RateableRiskRate.Create(200000, RiskAge.LessThanOrEqualToThirty, 0.0147M),
					RateableRiskRate.Create(300000, RiskAge.LessThanOrEqualToThirty, 0.0144M),
					RateableRiskRate.Create(500000, RiskAge.LessThanOrEqualToThirty, 0.0146M),
				});
			RiskRateCalc cut = new RiskRateCalc();

			//Act
			RiskRate result = cut.GetRiskRate(20, sumAssured, riskRateBands);

			//Assert
			result.Rate.Value.Should().Be(expectedRiskRateValue);

		}

	}
}
