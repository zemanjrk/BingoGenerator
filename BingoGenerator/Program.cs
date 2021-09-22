using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BingoGenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using FileStream fs = File.OpenRead("dict.json");
            var words = await JsonSerializer.DeserializeAsync<List<string>>(fs);
            int repeat = 40;
            string htmlString = "";
            htmlString += "<style>";
            htmlString += "body { font-family: Times New Roman; } td {text-align: center;} table, td, th {border:1px solid black;} table {border-collapse: collapse;} table {margin-bottom:0.5cm;}";
            htmlString += "</style>";
            for (int i = 0; i < repeat; i++)
            {
                var shuffled = words.OrderBy(x => Guid.NewGuid()).Take(25).ToList();
                htmlString += "<table>";

                int index = 0;
                foreach (var word in shuffled)
                {
                    if (index == 5)
                    {
                        htmlString += "</tr>";
                        index = 0;
                    }
                    else if (index == 0)
                    {
                        htmlString += "<tr>";
                    }
                    htmlString += "<td>" + word + "</td>";
                    index++;
                }

                htmlString += "</table><br><br>";
            }
            File.WriteAllText("index.html", htmlString);
        }
    }
}
