using ValuationApi.DB;
using ValuationApi.Models;

namespace ValuationApi.Repository
{
    public class ValuationRepository : IValuationRepository
    {
        /// <summary>
        /// Add new valuation to the db
        /// </summary>
        /// <param name="valuation"></param>
        public void AddValuation(List<Valuation> valuations)
        {
            try
            {
                using var context = new CustomDBContext();
                context.Valuations.AddRange(valuations);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all valuations from db
        /// </summary>
        /// <returns></returns>
        public List<Valuation> GetAllValuations()
        {
            try
            {
                using var context = new CustomDBContext();
                var list = context.Valuations.Where(x => x.RecordStatus == 1)
                    .ToList();

                if (list != null && list.Count > 0)
                {
                    return list;
                }
                return new List<Valuation>(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get valuation by id from db
        /// </summary>
        /// <param name="iMONo"></param>
        /// <returns></returns>
        public List<Valuation> GetValuations(string iMO)
        {
            try
            {
                using var context = new CustomDBContext();
                var valuations = context.Valuations.Where(x => x.IMO == iMO && x.RecordStatus == 1).ToList();

                if (valuations != null)
                {
                    return valuations;
                }

                return new List<Valuation>(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inactivate all valuations. Will be called when coefficients changed
        /// </summary>
        public void InActivateAllValuations()
        {
            try
            {
                using var context = new CustomDBContext();
                foreach (var item in context.Valuations.ToList())
                {
                    item.RecordStatus = 0;
                    context.Valuations.Update(item);
                }

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

