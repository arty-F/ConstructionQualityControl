using ConstructionQualityControl.Data.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionQualityControl.Data.Initialization
{
    /// <summary>
    /// Provides seed data to context.
    /// </summary>
    public class ContextInitializer
    {
        private readonly List<(string city, string region, double latitude, double longitude)> data;
        private Dictionary<string, Region> regionMap = new Dictionary<string, Region>();
        private List<object> cities = new List<object>();

        public ContextInitializer()
        {
            data = new InitializationDataProvider().GetData();
            ParseRegions();
            ParseCities();
        }

        /// <summary>
        /// Return regions seed collection.
        /// </summary>
        public IEnumerable<Region> GetRegions()
        {
            return regionMap.Values;
        }

        /// <summary>
        /// Return city seed collection as anonymous object kind of {id, name, lat, lng, regionId}.
        /// </summary>
        public IEnumerable<object> GetCitiesAsAnonObj()
        {
            return cities;
        }

        private void ParseRegions()
        {
            var uniqRegions = data.Select(d => d.region).Distinct();

            for (int i = 0; i < uniqRegions.Count(); i++)
                regionMap.Add(uniqRegions.ElementAt(i), new Region { Id = i + 1, Name = uniqRegions.ElementAt(i) });
        }

        private void ParseCities()
        {
            for (int i = 0; i < data.Count; i++)
            {
                cities.Add(new
                {
                    Id = i + 1,
                    Name = data[i].city,
                    Latitude = data[i].latitude,
                    Longitude = data[i].longitude,
                    RegionId = regionMap[data[i].region].Id
                });
            }
        }
    }
}
