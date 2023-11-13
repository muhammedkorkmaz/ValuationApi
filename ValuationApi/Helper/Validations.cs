using ValuationApi.Models;

namespace ValuationApi.Helper
{
    /// <summary>
    /// Validations class
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Check if any coeficient changed
        /// </summary>
        /// <param name="valuations"></param>
        /// <param name="coefficients"></param>
        /// <returns></returns>
        public static bool IsAnyCoeficientChanged(List<Valuation> valuations, List<Coefficient> coefficients)
        {
            try
            {
                //Check if coefficients of valuation changed
                foreach (var cachedValuation in valuations)
                {
                    Coefficient coefficient = coefficients.Find(x => x.Type == cachedValuation.ValueType);

                    // If any of the coefficients changed
                    if (coefficient != null
                        && (coefficient.A != cachedValuation.A
                        || coefficient.B != cachedValuation.B
                        || coefficient.Constant != cachedValuation.Constant))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

