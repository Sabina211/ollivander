using Catalog.Models;

namespace Catalog.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly WandsContext _wandsContext;

        public CatalogRepository(WandsContext wandsContext)
        {
            _wandsContext = wandsContext;
        }

        public async void AddWand(Wand wand)
        {
            _wandsContext.Wands.Add(wand);
            await _wandsContext.SaveChangesAsync();
        }

        public List<Wand> GetWands()
        {
            var wands = _wandsContext.Wands.ToList();
            return wands;
        }
    }
}
