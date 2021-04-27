using Covea.Framework;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Domain
{
	public class RatingServiceTests
	{


		public static IEnumerable<object[]> AgeInvalidTestParameters
		{
			get
			{
				yield return new object[] { -1 };
				yield return new object[] { 15 };
				yield return new object[] { 17 };
				yield return new object[] { 66 };
				yield return new object[] { 70 };
			}
		}

		[Theory()]
		[MemberData(nameof(AgeInvalidTestParameters))]
		public void When_age_is_invalid_then_client_is_notified(int age)
		{
			//Arrange
			RiskRateBands riskRateBands = RiskRateBands.Create(new List<RiskRate>());

			RatingEngine cut = new RatingEngineCutBuilder()
				.Build();

			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();

			var monitoredDispatcher = ratingEngineDispatcher.Monitor();

			//Act
			cut.Run(age, 25000, riskRateBands, ratingEngineDispatcher);

			//Assert
			monitoredDispatcher.Should().Raise(nameof(ratingEngineDispatcher.InvalidAgeEvent));


		}


		public static IEnumerable<object[]> SumAssuredInvalidTestParameters
		{
			get
			{
				yield return new object[] { 19000 };
				yield return new object[] { 24999.99 };
				yield return new object[] { 500000.01 };
				yield return new object[] { 800000 };
			}
		}

		[Theory()]
		[MemberData(nameof(SumAssuredInvalidTestParameters))]
		public void When_sum_assured_is_invalid_then_client_is_notified(decimal sumAssured)
		{
			//Arrange
			RiskRateBands riskRateBands = RiskRateBands.Create(new List<RiskRate>());

			RatingEngine cut = new RatingEngineCutBuilder()
				.Build();

			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();

			var monitoredDispatcher = ratingEngineDispatcher.Monitor();

			//Act
			cut.Run(20, sumAssured, riskRateBands, ratingEngineDispatcher);

			//Assert
			monitoredDispatcher.Should().Raise(nameof(ratingEngineDispatcher.InvalidSumAssuredEvent));


		}


		[Fact]
		public void When_age_and_sum_assured_are_unavailable_then_client_is_notified()
		{
			//Arrange
			RiskRateBands riskRateBands = RiskRateBands.Create(new List<RiskRate>());

			RatingEngine cut = new RatingEngineCutBuilder()
				.WithRiskRateCalcReturns(UnRateableRiskRate.Create(25000, RiskAge.LessThanOrEqualToThirty))
				.Build();
			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();

			var monitoredDispatcher = ratingEngineDispatcher.Monitor();

			//Act
			cut.Run(20, 25000, riskRateBands, ratingEngineDispatcher) ;

			//Assert
			monitoredDispatcher.Should().Raise( nameof(ratingEngineDispatcher.NotAvailableEvent));


		}


		[Fact]
		public void When_gross_premium_is_2_then_gross_premium_returned_and_sum_assured_not_adjusted()
		{
			//Arrange
			RiskRateBands riskRateBands = RiskRateBands.Create(new List<RiskRate>());

			RatingEngine cut = new RatingEngineCutBuilder()
				.WithRiskRateCalcReturns(RateableRiskRate.Create(25000, RiskAge.LessThanOrEqualToThirty, 0.0172M))
				.WithRiskPremiumCalcReturns(50000, 0.82M)
				.WithRenewalCommissionCalcReturns(0.82M, 0.25M)
				.WithNetPremiumCalcReturns(0.82M, 0.25M, 1.07M)
				.WithInitialCommissionCalcReturns(1.07M, 2.17M)
				.WithGrossPremiumCalcReturns(1.07M, 2.17M, 2.0M)
				.Build();
			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();

			var monitoredDispatcher = ratingEngineDispatcher.Monitor();

			//Act
			cut.Run(20, 50000, riskRateBands, ratingEngineDispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(ratingEngineDispatcher.RiskCalculatedEvent))
				.WithArgs<EventArgs<RatingResult>>(e => e.Data.SumAssured == 50000 && e.Data.GrossPremium == 2.0M);
		}






		[Fact]
		public void When_gross_premium_is_greater_than_2_then_gross_premium_returned_and_sum_assured_not_adjusted()
		{
			//Arrange
			RiskRateBands riskRateBands = RiskRateBands.Create(new List<RiskRate>());

			RatingEngine cut = new RatingEngineCutBuilder()
				.WithRiskRateCalcReturns(RateableRiskRate.Create(25000, RiskAge.LessThanOrEqualToThirty, 0.0172M))
				.WithRiskPremiumCalcReturns(50000, 0.82M)
				.WithRenewalCommissionCalcReturns(0.82M, 0.25M)
				.WithNetPremiumCalcReturns(0.82M, 0.25M, 1.07M)
				.WithInitialCommissionCalcReturns(1.07M, 2.17M)
				.WithGrossPremiumCalcReturns(1.07M, 2.17M, 3.24M)
				.Build();
			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();

			var monitoredDispatcher = ratingEngineDispatcher.Monitor();

			//Act
			cut.Run(20, 50000, riskRateBands, ratingEngineDispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(ratingEngineDispatcher.RiskCalculatedEvent))
				.WithArgs<EventArgs<RatingResult>>(e => e.Data.SumAssured == 50000 && e.Data.GrossPremium == 3.24M);
		}



		[Fact]
		public void When_gross_premium_is_less_than_2_then_gross_premium_is_minimise_and_sum_assured_adjusted()
		{
			//Arrange
			RiskRateBands riskRateBands = RiskRateBands.Create(new List<RiskRate>());

			RatingEngine cut = new RatingEngineCutBuilder()
				.WithRiskRateCalcReturns(RateableRiskRate.Create(25000, RiskAge.LessThanOrEqualToThirty, 0.0172M))
				.WithRiskPremiumCalcReturns(25000, 0.43M)
				.WithRenewalCommissionCalcReturns(0.43M, 0.01M)
				.WithNetPremiumCalcReturns(0.43M, 0.01M, 0.44M)
				.WithInitialCommissionCalcReturns(0.44M, 0.89M)
				.WithGrossPremiumCalcReturns(0.44M, 0.89M, 1.33M)
				.WithRiskPremiumCalcReturns(50000, 0.82M)
				.WithRenewalCommissionCalcReturns(0.82M, 0.25M)
				.WithNetPremiumCalcReturns(0.82M, 0.25M, 1.07M)
				.WithInitialCommissionCalcReturns(1.07M, 2.17M)
				.WithGrossPremiumCalcReturns(1.07M, 2.17M, 3.24M)
				.Build();
			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();

			var monitoredDispatcher = ratingEngineDispatcher.Monitor();

			//Act
			cut.Run(20, 25000, riskRateBands, ratingEngineDispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(ratingEngineDispatcher.RiskCalculatedEvent))
				.WithArgs<EventArgs<RatingResult>> ( e => e.Data.SumAssured == 50000 && e.Data.GrossPremium == 3.24M) ;
		}




	}
}
