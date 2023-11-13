using ValuationApi.Models;

namespace ValuationApi.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValuationRepository
    {
        public List<Valuation> GetAllValuations();
        public List<Valuation> GetValuations(string iMO);
        public void AddValuation(List<Valuation> valuations);
        public void InActivateAllValuations();
    }
}

