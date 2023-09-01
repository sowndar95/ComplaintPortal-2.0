using ComplaintPortalEntities;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalServices
{
    public sealed class EnumerationService : BaseService<Enumerations>
    {
        public EnumerationService(ApplicationSettings applicationSettings) : base(applicationSettings) 
        {
        }

        public async Task<IEnumerable<Enumerations>> Get(string? group)
        {

            if (!group.IsNullOrEmpty())
            {
                return await this.Find(f => f.Group == group);
            }
            else
            {
                return await this.GetAll();
            }
        }

        public async Task<IEnumerable<Enumerations>> LoadInitialSeedData()
        {
            List<Enumerations> lookups = new List<Enumerations>();

            lookups.AddRange(LoadFromDictionary(EnumDictionary.Department, ComplaintPortalEnumerations.Department));

            FilterDefinition<Enumerations> filterDefinition = Builders<Enumerations>.Filter.Empty;
            await _collection.DeleteManyAsync(filterDefinition);
            var result = await this.InsertMany(lookups);
            return lookups;
        }

        private List<Enumerations> LoadFromDictionary(Dictionary<string, string> items, string group)
        {
            List<Enumerations> data = new List<Enumerations>();
            foreach (var item in items)
            {
                data.Add(new Enumerations
                {
                    Code = item.Key,
                    Description = item.Value,
                    Group = group,
                });
            }
            return data;
        }
    }
}
