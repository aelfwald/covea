using Covea.Rating.Application;
using Covea.Rating.DI;
using System;

namespace Covea.Rating.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				RunProgram();
			}
			catch (Exception)
			{
				System.Console.WriteLine("*************************");
				System.Console.WriteLine("FATAL ERROR");
				System.Console.WriteLine("*************************");
			}
		}

		private static void RunProgram()
		{
			IRatingService ratingService = Resolver.ResolveRatingService();
			RatingServiceDispatcher ratingServiceDispatcher = new RatingServiceDispatcher();
			WireUpDispatcherEvents(ratingServiceDispatcher);

			do
			{
				int age;
				decimal sumAssured;

				bool ageOk = false;
				do
				{
					System.Console.WriteLine("Please enter a valid age:");
					ageOk = int.TryParse(System.Console.ReadLine(), out age);
					if (!ageOk)
					{
						System.Console.WriteLine("NOT A VALID NUMBER");
					}
				} while (!ageOk);

				bool sumAssuredOk;
				do
				{
					System.Console.WriteLine("Please enter a valid amount to assure:");
					sumAssuredOk = decimal.TryParse(System.Console.ReadLine(), out sumAssured);
					if (!sumAssuredOk)
					{
						System.Console.WriteLine("NOT A VALID AMOUNT");
					}
				} while (!sumAssuredOk);

				ratingService.CalculateGrossPremium(age, sumAssured, ratingServiceDispatcher);

				System.Console.WriteLine("");
				System.Console.WriteLine("*************************");
				System.Console.WriteLine("Press any key to continue");
				System.Console.WriteLine("*************************");
				System.Console.ReadKey();

			} while (true);
		}

		private static void WireUpDispatcherEvents(RatingServiceDispatcher ratingServiceDispatcher)
		{
			ratingServiceDispatcher.ErrorEvent += (sender, e) =>
			{
				System.Console.WriteLine("ERROR!!!");
			};

			ratingServiceDispatcher.RiskCalculatedEvent += (sender, e) =>
			{
				System.Console.WriteLine("*************************");
				System.Console.WriteLine("Risk Calculated!!!");
				System.Console.WriteLine($"Age: {e.Data.Age}");
				System.Console.WriteLine($"Sum Assured: {e.Data.SumAssured.ToString("C")}");
				System.Console.WriteLine($"Gross Premium: {e.Data.GrossPremium.ToString("C")}");
				System.Console.WriteLine("*************************");
			};

			ratingServiceDispatcher.InvalidInputEvent += (sender, e) =>
			{
				System.Console.WriteLine("*************************");
				System.Console.WriteLine($"{e.Data}");
				System.Console.WriteLine("*************************");
			};

			ratingServiceDispatcher.NotAvailableEvent += (sender, e) =>
			{
				System.Console.WriteLine("*************************");
				System.Console.WriteLine("Insurance not available.");
				System.Console.WriteLine("*************************");
			};
		}
	}
}
