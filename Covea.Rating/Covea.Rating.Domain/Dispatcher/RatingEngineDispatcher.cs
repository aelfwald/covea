

using Covea.Framework;
using System;

namespace Covea.Rating.Domain
{
	public class RatingEngineDispatcher
	{
		public event EventHandler<EventArgs<RatingResult>> RiskCalculatedEvent;

		public event EventHandler NotAvailableEvent;

		public event EventHandler InvalidAgeEvent;

		public event EventHandler InvalidSumAssuredEvent;

		public void OnRiskCalculated(RatingResult ratingResult )
		{
			this.RiskCalculatedEvent?.Invoke(this, EventArgs<RatingResult>.Create(ratingResult));
		}

		public void OnNotAvailble()
		{
			this.NotAvailableEvent?.Invoke(this, EventArgs.Empty);
		}

		public void OnInvalidAge()
		{
			this.InvalidAgeEvent?.Invoke(this, EventArgs.Empty);
		}

		public void OnInvalidSumAssured()
		{
			this.InvalidSumAssuredEvent?.Invoke(this, EventArgs.Empty);
		}

	}
}
