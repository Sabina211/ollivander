using Catalog.Models;

namespace Catalog.Repositories
{
    public interface ICatalogRepository
    {
        public void AddWand(Wand wand);
        public List<Wand> GetWands();
    }
}
