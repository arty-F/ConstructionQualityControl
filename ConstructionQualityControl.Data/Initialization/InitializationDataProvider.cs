using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConstructionQualityControl.Data.Initialization
{
    internal class InitializationDataProvider
    {
        private readonly string path = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "Initialization", "Russia_cities.txt");
        private readonly string regionPattern = @"(?<=\s)(.*?)(?=\d)";   //All between the first space and the first digit
        private readonly string cityPattern = @"^\S*";                   //All before space
        private readonly string valuePattern = @"[0-9]*\.?[0-9]+";       //Numbers with point

        /// <summary>
        /// Return tupple of (city, region, lat, lng).
        /// </summary>
        internal List<(string city, string region, double latitude, double longitude)> GetData()
        {
            IEnumerable<string> lines;

            try
            {
                lines = File.ReadLines(path);
            }
            catch (Exception)
            {
                throw new Exception($"Error on reading file: {path}.");
            }

            var data = new List<(string city, string region, double latitude, double longitude)>(lines.Count());

            foreach (var line in lines)
            {
                string region, city;
                double latitude, longitude;

                try
                {
                    region = Regex.Match(line, regionPattern).Value.Trim();
                    city = Regex.Match(line, cityPattern).Value;
                    var values = Regex.Matches(line, valuePattern);
                    latitude = double.Parse(values[0].Value, CultureInfo.InvariantCulture);
                    longitude = double.Parse(values[1].Value, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    throw new Exception($"Error on parsing line: {line}.");
                }

                data.Add((city, region, latitude, longitude));
            }

            return data;
        }
    }
}
