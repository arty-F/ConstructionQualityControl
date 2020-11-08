using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConstructionQualityControl.Data.Initialization
{
    public class InitializationDataProvider
    {
        private readonly string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Russia_cities.txt");

        public void GetRegions()
        {
            var data = new List<(string city, string region, double latitude, double longitude)>();

            IEnumerable<string> lines;

            try
            {
                lines = File.ReadLines(path);
            }
            catch (Exception)
            {
                throw new Exception($"Error on reading file: {path}.");
            }

            string regionPattern = @"(?<=\s)(.*?)(?=\d)";   //All between the first space and the first digit
            string cityPattern = @"^\S*";                   //All before space
            string valuePattern = @"[0-9]*\.?[0-9]+";       //Numbers with point

            foreach (var line in lines)
            {
                string region, city;
                double latitude, longitude;

                try
                {
                    region = Regex.Match(line, regionPattern).Value.Trim();
                    city = Regex.Match(line, cityPattern).Value;
                    var values = Regex.Matches(line, valuePattern);
                    latitude = double.Parse(values[0].Value);
                    longitude = double.Parse(values[1].Value);
                }
                catch (Exception)
                {
                    throw new Exception($"Error on parsing line: {line}.");
                }

                data.Add((city, region, latitude, longitude));
            }
        }
    }
}
