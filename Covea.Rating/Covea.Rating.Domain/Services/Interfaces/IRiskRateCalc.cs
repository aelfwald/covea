namespace Covea.Rating.Domain
{
	public interface IRiskRateCalc
	{
		RiskRate GetRiskRate(int age, decimal sumAssured, RiskRateBands riskRateBands);
	}
}
