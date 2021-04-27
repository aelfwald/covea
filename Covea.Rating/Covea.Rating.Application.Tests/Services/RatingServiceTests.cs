using Covea.Framework;
using Covea.Rating.Domain;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Covea.Rating.Application
{
	public class RatingServiceTests
	{


		[Fact()]
		public void When_age_is_invalid_then_notification_is_raised()
		{
			//Arrange
			Mock<IRetrieveRiskRateBandsQuery> riskRatesQuery = new Mock<IRetrieveRiskRateBandsQuery>();
			Mock<IRatingEngine> ratingEngine = new Mock<IRatingEngine>();

			ratingEngine.Setup(m => m.Run(
			   It.IsAny<int>(),
			   It.IsAny<decimal>(),
			   It.IsAny<RiskRateBands>(),
			   It.IsAny<RatingEngineDispatcher>()))
				.Callback<int,decimal,RiskRateBands,RatingEngineDispatcher>((v, w, x, y) =>
						y.OnInvalidAge());
			

			RatingService cut = new RatingService(
				ratingEngine.Object,
				riskRatesQuery.Object
				);

			RatingServiceDispatcher dispatcher = new RatingServiceDispatcher();

			var monitoredDispatcher = dispatcher.Monitor();

			//Act
			cut.CalculateGrossPremium(10, 2, dispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(dispatcher.InvalidInputEvent));

		}


		[Fact()]
		public void When_sum_assured_is_invalid_then_notification_is_raised()
		{
			//Arrange
			Mock<IRetrieveRiskRateBandsQuery> riskRatesQuery = new Mock<IRetrieveRiskRateBandsQuery>();
			Mock<IRatingEngine> ratingEngine = new Mock<IRatingEngine>();

			ratingEngine.Setup(m => m.Run(
			   It.IsAny<int>(),
			   It.IsAny<decimal>(),
			   It.IsAny<RiskRateBands>(),
			   It.IsAny<RatingEngineDispatcher>()))
				.Callback<int, decimal, RiskRateBands, RatingEngineDispatcher>((v, w, x, y) =>
						   y.OnInvalidSumAssured());


			RatingService cut = new RatingService(
				ratingEngine.Object,
				riskRatesQuery.Object
				);

			RatingServiceDispatcher dispatcher = new RatingServiceDispatcher();

			var monitoredDispatcher = dispatcher.Monitor();

			//Act
			cut.CalculateGrossPremium(10, 2, dispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(dispatcher.InvalidInputEvent));

		}

		[Fact()]
		public void When_risk_rate_is_not_available_then_notification_is_raised()
		{
			//Arrange
			Mock<IRetrieveRiskRateBandsQuery> riskRatesQuery = new Mock<IRetrieveRiskRateBandsQuery>();
			Mock<IRatingEngine> ratingEngine = new Mock<IRatingEngine>();

			ratingEngine.Setup(m => m.Run(
			   It.IsAny<int>(),
			   It.IsAny<decimal>(),
			   It.IsAny<RiskRateBands>(),
			   It.IsAny<RatingEngineDispatcher>()))
				.Callback<int, decimal, RiskRateBands, RatingEngineDispatcher>((v, w, x, y) =>
						   y.OnNotAvailble());


			RatingService cut = new RatingService(
				ratingEngine.Object,
				riskRatesQuery.Object
				);

			RatingServiceDispatcher dispatcher = new RatingServiceDispatcher();

			var monitoredDispatcher = dispatcher.Monitor();

			//Act
			cut.CalculateGrossPremium(10, 2, dispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(dispatcher.NotAvailableEvent));

		}


		[Fact()]
		public void When_gross_premium_calculated_then_notification_is_raised()
		{
			//Arrange
			Mock<IRetrieveRiskRateBandsQuery> riskRatesQuery = new Mock<IRetrieveRiskRateBandsQuery>();
			Mock<IRatingEngine> ratingEngine = new Mock<IRatingEngine>();

			ratingEngine.Setup(m => m.Run(
			   It.IsAny<int>(),
			   It.IsAny<decimal>(),
			   It.IsAny<RiskRateBands>(),
			   It.IsAny<RatingEngineDispatcher>()))
				.Callback<int, decimal, RiskRateBands, RatingEngineDispatcher>((v, w, x, y) =>
						   y.OnRiskCalculated(new RatingResult()));


			RatingService cut = new RatingService(
				ratingEngine.Object,
				riskRatesQuery.Object
				);

			RatingServiceDispatcher dispatcher = new RatingServiceDispatcher();

			var monitoredDispatcher = dispatcher.Monitor();

			//Act
			cut.CalculateGrossPremium(10, 2, dispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(dispatcher.RiskCalculatedEvent));

		}


		[Fact()]
		public void When_error_occurs_then_notification_is_raised()
		{
			//Arrange
			Mock<IRetrieveRiskRateBandsQuery> riskRatesQuery = new Mock<IRetrieveRiskRateBandsQuery>();
			Mock<IRatingEngine> ratingEngine = new Mock<IRatingEngine>();

			ratingEngine.Setup(m => m.Run(
			   It.IsAny<int>(),
			   It.IsAny<decimal>(),
			   It.IsAny<RiskRateBands>(),
			   It.IsAny<RatingEngineDispatcher>()))
				.Callback<int, decimal, RiskRateBands, RatingEngineDispatcher>((v, w, x, y) =>
						   throw new Exception());


			RatingService cut = new RatingService(
				ratingEngine.Object,
				riskRatesQuery.Object
				);

			RatingServiceDispatcher dispatcher = new RatingServiceDispatcher();

			var monitoredDispatcher = dispatcher.Monitor();

			//Act
			cut.CalculateGrossPremium(10, 2, dispatcher);

			//Assert
			monitoredDispatcher.Should()
				.Raise(nameof(dispatcher.ErrorEvent));

		}




	}
}
