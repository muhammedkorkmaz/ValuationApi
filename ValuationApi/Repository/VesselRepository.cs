using ValuationApi.DB;
using ValuationApi.Models;

namespace ValuationApi.Repository
{
    /// <summary>
    /// Vessels database operations
    /// </summary>
	public class VesselRepository : IVesselRepository
    {
        public void AddVessel(Vessel vessel)
        {
            try
            {
                using var context = new CustomDBContext();

                context.Vessels.Add(vessel);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all vessels from db
        /// </summary>
        /// <returns></returns>
        public List<Vessel> GetAllVessels()
        {
            try
            {
                using var context = new CustomDBContext();
                var list = context.Vessels
                    .ToList();

                if (list != null && list.Count > 0)
                {
                    return list;
                }
                return new List<Vessel>(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get vessel by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vessel GetVessel(string iMO)
        {
            try
            {
                using var context = new CustomDBContext();
                var vessel = context.Vessels.FirstOrDefault(x => x.IMO == iMO);

                if (vessel != null)
                {
                    return vessel;
                }

                return new Vessel();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update vessel
        /// </summary>
        public void Update(Vessel vessel)
        {
            try
            {
                using var context = new CustomDBContext();

                context.Vessels.Update(vessel);

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

