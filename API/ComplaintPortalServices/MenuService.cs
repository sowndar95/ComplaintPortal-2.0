using ComplaintPortalEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalServices
{
    public sealed class MenuService : BaseService<Menu>
    {
        public MenuService(ApplicationSettings applicationSettings) : base(applicationSettings)
        {
        }

        public async Task<long> DeleteAllMenu()
        {
            FilterDefinition<Menu> filterDefinition = Builders<Menu>.Filter.Empty;
            var result = await _collection.DeleteManyAsync(filterDefinition);
            return result.DeletedCount;
        }
    }
}
