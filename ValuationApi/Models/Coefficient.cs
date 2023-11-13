namespace ValuationApi.Models
{
    /// <summary>
    /// Coefficient props
    /// </summary>
    [Serializable]
    public class Coefficient
    {
        /// <summary>
        /// Coefficient Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Coeefficient type (Fair Market Value, Operating Costs, Scrap Price etc.)
        /// </summary>
        public required string Type { get; set; }

        /// <summary>
        /// Value of A
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Value of B
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Value of constant
        /// </summary>
        public int Constant { get; set; }
    }
}

