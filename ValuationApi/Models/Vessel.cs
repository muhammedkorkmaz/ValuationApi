using FluentValidation;
using System;

namespace ValuationApi.Models
{
    /// <summary>
    /// Vesel prop
    /// </summary>
    [Serializable]
    public class Vessel
    {
        /// <summary>
        /// Id number
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// IMO number
        /// </summary>
        
		public string IMO { get; set; }

        /// <summary>
        /// Vessel type enum
        /// </summary>
        public VesselType VesselType { get; set; }

        /// <summary>
        /// Year of build
        /// </summary>
        public short YearOfBuild { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public int Size { get; set; }
    }

    /// <summary>
    /// Validator
    /// </summary>
    public class VesselValidator : AbstractValidator<Vessel>
    {
        public VesselValidator()
        {
            RuleFor(x => x.IMO)
                .NotEmpty().WithMessage("IMO number can not be empty.")
                .MaximumLength(10).WithMessage("Maximum IMO number lenght must be 10 character.")
                .Matches(@"\b[IMO]\w+[0-9]{7}$").WithMessage("It is not a valid IMO number.");

            RuleFor(x => x.YearOfBuild)
              .NotEmpty().WithMessage("Year Of Build can not be empty.");

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size can not be empty.")

                .GreaterThan(25000).When(x => x.VesselType == VesselType.DryBulk)
                .WithMessage("Dry Bulk size should be between 25000 and 125000")

                .LessThan(125000).When(x => x.VesselType == VesselType.DryBulk)
                .WithMessage("Dry Bulk size should be between 25000 and 125000")

                .GreaterThan(35000).When(x => x.VesselType == VesselType.OilTanker)
                .WithMessage("Oil Tanker size should be between 35000 and 75000")

                .LessThan(75000).When(x => x.VesselType == VesselType.OilTanker)
                .WithMessage("Oil Tanker size should be between 35000 and 75000")

                .GreaterThan(20000).When(x => x.VesselType == VesselType.ContainerShip && x.YearOfBuild < 2018)
                .WithMessage("Container Ship that builded before 2018 size should be between 20000 and 45000")

                .LessThan(45000).When(x => x.VesselType == VesselType.ContainerShip && x.YearOfBuild < 2018)
                .WithMessage("Container Ship that builded before 2018 size should be between 20000 and 45000")

                .GreaterThan(20000).When(x => x.VesselType == VesselType.ContainerShip && x.YearOfBuild >= 2018)
                .WithMessage("Container Ship that builded after 2018 size should be between 20000 and 55000")

                .LessThan(55000).When(x => x.VesselType == VesselType.ContainerShip && x.YearOfBuild >= 2018)
                .WithMessage("Container Ship that builded after 2018 size should be between 20000 and 55000");
        }
    }

    /// <summary>
    /// Vessel types
    /// </summary>
	public enum VesselType
    {
        DryBulk,
        OilTanker,
        ContainerShip
    }
}

