using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class GrossPremiumCalcTests
	{
		public static IEnumerable<object[]> GrossPremiumCalcTestParameters
		{
			get
			{
				yield return new object[] { 0.44, 0.89, 1.33 };
				yield return new object[] { 2.67, 5.42, 8.09 };
				yield return new object[] { 0.84, 1.71, 2.55 };
				yield return new object[] { 1.58, 3.21, 4.79 };
			}
		}

		[Theory()]
		[MemberData(nameof(GrossPremiumCalcTestParameters))]
		public void Ensure_gross_premium_is_net_premium_plus_initial_commission(
			decimal netPremium,
			decimal initialCommission,
			decimal expectedGrossPremium)
		{

			//Arrange
			GrossPremiumCalc cut = new GrossPremiumCalc();

			//Act
			decimal grossPremium = cut.Calculate(netPremium, initialCommission);

			//Assert
			grossPremium.Should().Be(expectedGrossPremium);

		}
	}
}
