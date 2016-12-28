using System;
using System.Collections;
using System.IO;

namespace CSVReadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("data.csv"))
                {

                    string line;
                    string[] row;
                    sr.ReadLine(); // cut out first line
                    ArrayList lat, lon, alt, data;
                    lat = new ArrayList();
                    lon = new ArrayList();
                    alt = new ArrayList();
                    data = new ArrayList();
                    while ((line = sr.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        lat.Add(float.Parse(row[0]));
                        lon.Add(float.Parse(row[1]));
                        alt.Add(float.Parse(row[2]));
                        data.Add(float.Parse(row[3]));
                    } // end while

                    Console.Write("lat: ");
                    foreach (float f in lat) // for each loop to see if it read everything properly
                        Console.Write(f + " ");
                    Console.WriteLine();

                } // end using. Automatically closes file.
            } // end try
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            } // end catch

            Console.WriteLine("Press any key to continue.");
            // Console.ReadKey();
        }
    }
}
