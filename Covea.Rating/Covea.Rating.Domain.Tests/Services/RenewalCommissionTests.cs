using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class RenewalCommissionCalcTests
	{
		public static IEnumerable<object[]> RenewalCommissionCalcTestParameters
		{
			get
			{
				yield return new object[] { 0.38, 0.01 };
				yield return new object[] { 2.60, 0.08 };
				yield return new object[] { 0.83, 0.02 };
				yield return new object[] { 1.54, 0.05 };
			}
		}

		[Theory()]
		[MemberData(nameof(RenewalCommissionCalcTestParameters))]
		public void Ensure_renewal_commission_is_risk_premium_times_3_percent(
			decimal riskPremium,
			decimal expectedRenewalCommission)
		{

			//Arrange
			RenewalCommissionCalc cut = new RenewalCommissionCalc();

			//Act
			decimal renewalCommission = cut.Calculate(riskPremium);

			//Assert
			renewalCommission.Should().Be(expectedRenewalCommission);


		}
	}
}
