using Covea.Framework;
using System;

namespace Covea.Rating.Application
{
	public class RatingServiceDispatcher
	{
		public event EventHandler<EventArgs<RatingResultDto>> RiskCalculatedEvent;

		public event EventHandler ErrorEvent;

		public event EventHandler<EventArgs<string>> InvalidInputEvent;

		public event EventHandler NotAvailableEvent;

		internal void OnRiskCalculated(RatingResultDto dto)
		{
			this.RiskCalculatedEvent?.Invoke(this, EventArgs<RatingResultDto>.Create(dto));
		}

		internal void OnError()
		{
			this.ErrorEvent?.Invoke(this, EventArgs.Empty);
		}

		internal void OnNotAvailble()
		{
			this.NotAvailableEvent?.Invoke(this, EventArgs.Empty);
		}

		internal void OnInvalidInput(string message)
		{
			this.InvalidInputEvent?.Invoke(this, EventArgs<string>.Create(message));
		}


	}
}
