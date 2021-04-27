using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class InitialCommissionCalcTests
	{
		public static IEnumerable<object[]> InitialCommissionCalcTestParameters
		{
			get
			{
				yield return new object[] { 0.44, 0.89 };
				yield return new object[] { 2.67, 5.42 };
				yield return new object[] { 0.84, 1.71 };
				yield return new object[] { 1.58, 3.21 };
			}
		}

		[Theory()]
		[MemberData(nameof(InitialCommissionCalcTestParameters))]
		public void Ensure_initial_commission_risk_premium_times_203_percent(
			decimal riskPremium,
			decimal expectedInitialCommission)
		{

			//Arrange
			InitialCommissionCalc cut = new InitialCommissionCalc();

			//Act
			decimal initialCommission = cut.Calculate(riskPremium);

			//Assert
			initialCommission.Should().Be(expectedInitialCommission);


		}
	}
}
