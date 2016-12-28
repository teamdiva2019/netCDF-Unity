using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Reflection;

public class SphereScript : MonoBehaviour {

    private ArrayList lat = new ArrayList(); // latitudes
    private ArrayList lon = new ArrayList(); // longitudes
    private ArrayList alt = new ArrayList(); // altitudes
    private ArrayList data = new ArrayList(); // data pts

    public Text cnsl; // this is the console. If there are errors, I will write them here.

    private int DATA_MAX = 10; // our data points can have values from 0 to 10.

    private ArrayList pts = new ArrayList(); // contains the actual spheres in 3-space

    // reads in all the data to start with and stores it in the ArrayLists
    void Start ()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        cnsl.text = "Test text."; // clear console
        cnsl.transform.position = new Vector3(0, 0, 0);
        ReadData();
        GeneratePts();
    } // end function Start

    // creates the data points
    void GeneratePts () {
        for (int i = 0; i < lat.Count; i++)
        {

            float rho, theta, phi;
            rho = (float)alt[i];
            theta = (float)lon[i] * 180/Mathf.PI; // note the conversion to rad
            phi = (90 - (float)lat[i]) * 180/Mathf.PI; // note the conversion to rad

            float r = rho * Mathf.Sin(phi);
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);
            float z = rho * Mathf.Cos(phi);

            GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tmp.transform.position = new Vector3(x, y, z);
            // tmp.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F); // note that the Fs indicate floats; without them, it's a double.
            Renderer rd = (Renderer)tmp.GetComponent("Renderer");
            rd.material.color = new Color((float)data[i]/DATA_MAX, 1 - (float)data[i] / DATA_MAX, 0, 0.5F); // green for low levels, red for high.
            pts.Add(tmp);

        } // end for
    } // end function GeneratePts()

    // this makes the script delay for d seconds. Note that function names are capitalized.
    IEnumerator Wait(float d)
    {
        yield return new WaitForSeconds(d);
    } // end function Wait()

    // this function reads the file data.txt to populate Arraylists for lat, lon, alt, and data.
    void ReadData()
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            // Note that location of executing assemebly is: *\netCDF-Unity\Interactive Visualization\Library\ScriptAssemblies\Assembly-CSharp.dll
            // Note also that the .dll is a folder containing the assembly, so that's why we need to go three levels up from it.
            using (StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().Location+"\\..\\..\\..\\Assets\\Scripts\\data.csv"))
            {

                string line;
                string[] row;
                sr.ReadLine(); // cut out first line
                
                // clearing ou
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

                System.Console.Write("lat: ");
                foreach (float f in lat) // for each loop to see if it read everything properly
                    System.Console.Write(f + " ");
                System.Console.WriteLine();

            } // end using. Automatically closes file.
        } // end try
        catch (System.Exception e)
        {
            // Let the user know what went wrong.
            cnsl.text = "File not found.\nLocation of Executing Assembly (for reference): "+ Assembly.GetExecutingAssembly().Location;
        } // end catch

    } // end function ReadData

}
