using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class RiskPremiumCalcTests
	{
		public static IEnumerable<object[]> RiskRateCalcTestParameters
		{
			get
			{
				yield return new object[] { 25000, 0.0152, 0.38 };
				yield return new object[] { 25000, 0.1043, 2.61 };
				yield return new object[] { 50000, 0.0165, 0.82 };
				yield return new object[] { 100000, 0.0154, 1.54 };
			}
		}

		[Theory()]
		[MemberData(nameof(RiskRateCalcTestParameters))]
		public void When_risk_premium_is_risk_rate_times_by_the_result_of_sum_assured_divided_by_1000(
			decimal riskRate,
			decimal sumAssured,
			decimal expectedRiskPremium)
		{

			//Arrange
			RiskPremiumCalc cut = new RiskPremiumCalc();

			//Act
			decimal riskPremium = cut.Calculate(riskRate, sumAssured);

			//Assert
			riskPremium.Should().Be(expectedRiskPremium);


		}
	}
}
