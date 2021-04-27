using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class NetPremiumCalcTests
	{
		public static IEnumerable<object[]> NetPremiumCalcTestParameters
		{
			get
			{
				yield return new object[] { 0.43, 0.01, 0.44 };
				yield return new object[] { 2.60, 0.07, 2.67 };
				yield return new object[] { 0.82, 0.02, 0.84 };
				yield return new object[] { 1.54, 0.04, 1.58 };
			}
		}

		[Theory()]
		[MemberData(nameof(NetPremiumCalcTestParameters))]
		public void Ensure_net_premium_risk_premium_plus_renewal_commission(
			decimal riskPremium,
			decimal renewalCommission,
			decimal expectedNetPremium)
		{

			//Arrange
			NetPremiumCalc cut = new NetPremiumCalc();

			//Act
			decimal netPremium = cut.Calculate(riskPremium, renewalCommission);

			//Assert
			netPremium.Should().Be(expectedNetPremium);


		}
	}
}
