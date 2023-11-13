using System;
using ValuationApi.Models;

namespace ValuationApi.Helper
{
    /// <summary>
    /// Validations class
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Validate vessel properties values
        /// </summary>
        /// <param name="vessel"></param>
        /// <returns></returns>
        public static string ValidateVessel(Vessel vessel)
        {
            try
            {
                int sizeRangeStart = 0;
                int sizeRangeEnd = 0;

                switch (vessel.VesselType)
                {
                    case VesselType.DryBulk:
                        sizeRangeStart = 25000;
                        sizeRangeEnd = 125000;
                        break;

                    case VesselType.OilTanker:
                        sizeRangeStart = 35000;
                        sizeRangeEnd = 75000;
                        break;

                    case VesselType.ContainerShip:
                        if (vessel.YearOfBuild < 2018)
                        {
                            sizeRangeStart = 20000;
                            sizeRangeEnd = 45000;
                        }
                        else
                        {
                            sizeRangeStart = 20000;
                            sizeRangeEnd = 55000;
                        }
                        break;
                    default:
                        break;
                }

                if (vessel.Size < sizeRangeStart || vessel.Size > sizeRangeEnd)
                {
                    return "Invalid size!";
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

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

