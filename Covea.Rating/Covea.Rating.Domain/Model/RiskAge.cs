namespace Covea.Rating.Domain
{
	public class RiskAge
	{
		public static RiskAge LessThanOrEqualToThirty = new RiskAge();

		public static RiskAge ThirtyOneToFifty = new RiskAge();

		public static RiskAge GreaterThanFifty = new RiskAge();

		public static explicit operator RiskAge(int age)
		{
			if (age <= 30)
			{
				return LessThanOrEqualToThirty;
			}
			else if (age > 30 && age <= 50)
			{
				return ThirtyOneToFifty;
			}
			else
			{
				return GreaterThanFifty;
			}

		}
	}
}
