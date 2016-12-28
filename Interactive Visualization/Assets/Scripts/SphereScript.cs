using UnityEngine;
using System.Collections;
using System.IO;

public class SphereScript : MonoBehaviour {

    private float phi = 0;
    private float rho = 1;
    private float theta = 0;
    private ArrayList s = new ArrayList(); // contains all the spheres representing data points.
	
	// once per frame, move a tiny bit around unit sphere
	void Update () {
        phi += Mathf.PI / 180;

        float r = rho * Mathf.Sin(phi);
        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);
        float z = rho * Mathf.Cos(phi);

        if (phi <= 2 * Mathf.PI)
        { // if phi is less than 2pi
            GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tmp.transform.position = new Vector3(x, y, z);
            tmp.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
            Renderer rd = (Renderer)tmp.GetComponent("Renderer");
            rd.material.color = new Color(Random.value, Random.value, Random.value, Random.value);
            s.Add(tmp);            
        } // end if
    } // end function Update()

    // this makes the script delay for d seconds. Note that function names are capitalized.
    IEnumerator Wait(float d)
    {
        yield return new WaitForSeconds(d);
    } // end function Wait()

    // this function reads the file data.txt to populate Arraylists for lat, lon, alt, and data.
    void ReadData(ArrayList lat, ArrayList lon, ArrayList alt, ArrayList data)
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("..\\..\\data.csv"))
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
            System.Console.WriteLine("The file could not be read:");
            System.Console.WriteLine(e.Message);
        } // end catch

    } // end function ReadData

}
