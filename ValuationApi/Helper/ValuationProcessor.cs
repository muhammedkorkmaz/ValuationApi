using System;
using ValuationApi.Models;

namespace ValuationApi.Helper
{
    /// <summary>
    /// Valuation operations class
    /// </summary>
    public static class ValuationProcessor
    {
        private static readonly int[] _years = { 2020, 2021, 2022 };

        /// <summary>
        /// Valueate vessel for each coefficient and year that defined constantly
        /// </summary>
        /// <param name="coefficients"></param>
        /// <param name="vessel"></param>
        /// <returns></returns>
        public static List<Valuation> Valuate(List<Coefficient> coefficients, Vessel vessel)
        {
            try
            {
                List<Valuation> valuations = new(coefficients.Count * _years.Length);

                foreach (var coefficient in coefficients)
                {
                    foreach (int year in _years)
                    {
                        int age = year - vessel.YearOfBuild;

                        valuations.Add(new Valuation()
                        {
                            IMO = vessel.IMO,
                            Year = year,
                            Age = age,
                            Value = (coefficient.A * vessel.Size) - (coefficient.B * age) + coefficient.Constant,
                            ValueType = coefficient.Type,
                            A = coefficient.A,
                            B = coefficient.B,
                            Constant = coefficient.Constant,
                            RecordStatus = 1,
                            RecordDate = DateTime.Now
                        });
                    }
                }

                return valuations;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

