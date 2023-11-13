namespace ValuationApi.Models
{
    /// <summary>
    /// Valuation props
    /// </summary>
    [Serializable]
    public class Valuation
    {
        /// <summary>
        /// Valuation id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// IMO number (Releated to vessel entity)
        /// </summary>
        public required string IMO { get; set; }

        /// <summary>
        /// Year (There will be three kind of years according to assestment: 2020,2021,2022)
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Calculated age, according to the Year prop
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Coeefficient type (Fair Market Value, Operating Costs, Scrap Price etc.)
        /// </summary>
        public required string ValueType { get; set; }

        /// <summary>
        /// Value of A used for existing valuation
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Value of B used for existing valuation
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Value of constant used for existing valuation
        /// </summary>
        public int Constant { get; set; }

        /// <summary>
        /// Record status (1: Active, 0:Inactive)
        /// </summary>
        public int RecordStatus { get; set; }

        /// <summary>
        /// Record Date
        /// </summary>
        public DateTime RecordDate { get; set; }

    }
}

