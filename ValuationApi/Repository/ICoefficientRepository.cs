using ValuationApi.Models;

namespace ValuationApi.Repository
{
    public interface ICoefficientRepository
	{
        public List<Coefficient> GetAllCoefficients();
        public void AddCoefficients(List<Coefficient> coefficients);
    }
}

