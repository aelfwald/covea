using Covea.Rating.Domain;
using System;

namespace Covea.Rating.Application
{
	public class RatingService : IRatingService
	{
		private readonly IRatingEngine _ratingEngine;
		private readonly IRetrieveRiskRateBandsQuery _retrieveRiskRateBandsQuery;

		public RatingService(
			IRatingEngine ratingEngine,
			IRetrieveRiskRateBandsQuery retrieveRiskRateBandsQuery
			)
		{
			_ratingEngine = ratingEngine ?? throw new ArgumentNullException(nameof(ratingEngine));
			_retrieveRiskRateBandsQuery = retrieveRiskRateBandsQuery ?? throw new ArgumentNullException(nameof(retrieveRiskRateBandsQuery));
		}

		public void CalculateGrossPremium(int age, decimal sumInsured, RatingServiceDispatcher dispatcher)
		{
			if (dispatcher == null)
			{
				throw new ArgumentNullException(nameof(dispatcher));
			}

			RatingEngineDispatcher ratingEngineDispatcher = new RatingEngineDispatcher();
			WireUpDispatcher(age, dispatcher, ratingEngineDispatcher);

			try
			{
				RiskRateBands riskRateBands = _retrieveRiskRateBandsQuery.RunQuery();
				_ratingEngine.Run(age, sumInsured, riskRateBands, ratingEngineDispatcher);
			}
			catch (Exception)
			{
				dispatcher.OnError();
			}

		}

		private static void WireUpDispatcher(int age, RatingServiceDispatcher dispatcher, RatingEngineDispatcher ratingEngineDispatcher)
		{
			ratingEngineDispatcher.RiskCalculatedEvent += (sender, e) =>
			{
				dispatcher.OnRiskCalculated(new RatingResultDto()
				{
					Age = age,
					SumAssured = e.Data.SumAssured,
					GrossPremium = e.Data.GrossPremium
				});
			};


			ratingEngineDispatcher.InvalidAgeEvent += (sender, e) =>
			{
				dispatcher.OnInvalidInput("Age must be a positive number between 18 and 65.");
			};

			ratingEngineDispatcher.InvalidSumAssuredEvent += (sender, e) =>
			{
				dispatcher.OnInvalidInput("Sum assured must a positive amount between £25,000 and £500,000.");
			};

			ratingEngineDispatcher.NotAvailableEvent += (sender, e) =>
			{
				dispatcher.OnNotAvailble();
			};
		}
	}
}
