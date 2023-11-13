using ValuationApi.DB;
using ValuationApi.Models;

namespace ValuationApi.Repository
{
    /// <summary>
    /// Coefficients db operaitons class
    /// </summary>
    public class CoefficientRepository : ICoefficientRepository
    {
        /// <summary>
        /// Add new coefficients
        /// </summary>
        /// <param name="coefficients"></param>
        public void AddCoefficients(List<Coefficient> coefficients)
        {
            try
            {
                using var context = new CustomDBContext();
                removeAllCoefficients();
                context.Coefficients.AddRange(coefficients);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// List all coefficients
        /// </summary>
        /// <returns></returns>
        public List<Coefficient> GetAllCoefficients()
        {
            try
            {
                using var context = new CustomDBContext();
                var list = context.Coefficients
                    .ToList();

                if (list != null && list.Count > 0)
                {
                    return list;
                }
                return new List<Coefficient>(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void removeAllCoefficients()
        {
            using var context = new CustomDBContext();
            var list = context.Coefficients
                .ToList();

            if (list != null && list.Count > 0)
            {
                context.Coefficients.RemoveRange(list);
            }
        }
    }
}

