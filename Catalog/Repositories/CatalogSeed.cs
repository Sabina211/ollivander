using Catalog.Models;
using System.Data.SqlClient;

namespace Catalog.Repositories
{
    public class CatalogSeed
    {
        public static void CreateData(WandsContext wandsContext)
        {
            wandsContext.Database.EnsureCreated();
            if (wandsContext.Wands.Any())
            { 
                var test = wandsContext.Wands.First();
                return; }

            var wands = new List<Wand> { 
            new Wand("Karl", 1000, 1, "wood", "core", 100),
            new Wand("Mark", 2500, 1, "wood", "core", 200)
            };
            using (var trans = wandsContext.Database.BeginTransaction())
            {
                foreach (var wand in wands)
                {
                    wandsContext.Wands.Add(wand);
                }
                wandsContext.SaveChanges();
                trans.Commit();
            }
        }

    }
}
