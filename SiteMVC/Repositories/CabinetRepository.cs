using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class CabinetRepository
    {
        private readonly ApplicationContext applicationContext;

        public CabinetRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public async Task AddNewCabinet(int? number, string description)
        {
            Cabinet cabinet = new Cabinet
            {
                Number = number,
                Description = description
            };
            applicationContext.Cabinets.Add(cabinet);
            await applicationContext.SaveChangesAsync();
        }
    }
}
