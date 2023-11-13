using ValuationApi.Models;

namespace ValuationApi.Repository
{
    public interface IVesselRepository
    {
        public void AddVessel(Vessel vessel);
        public Vessel GetVessel(string iMO);
        public List<Vessel> GetAllVessels();
    }
}

