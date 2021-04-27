using Covea.Rating.Domain;
using System;

namespace Covea.Rating.Application
{
	public interface IRatingService
	{
		void CalculateGrossPremium(int age, decimal sumInsured, RatingServiceDispatcher dispatcher);
	}
}
