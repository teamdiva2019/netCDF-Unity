/*
 * Another test with JSON deserialization, this time using some data from one of UCAR's made-up NetCDFs.
 * I extracted the data from sfc_pres_temp.nc and put it into data.json by modifying UCAR's Java code.
 * You can find my modified version of UCAR's Java code at: *\netCDF-Unity\NetCDF Java\src\Sfc_pres_temp_rd.java
 * You can find UCAR's original code UCAR's original code, which does not write to a JSON, at:  http://www.unidata.ucar.edu/software/netcdf/examples/programs/
 */

using UnityEngine;
using System.Reflection; // so it can find the location of its executing assembly. This allows us to use relative paths.
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class JsonDeserializationTest2 : MonoBehaviour // all Unity Scripts must derive from something called "MonoBehaviour" for some reason
{
    
    public Text scoreText;
    private ArrayList dataSpheres = new ArrayList(); // data pts
    private int PRES_MAX = 1000; // our pressure data points can have values from 900 to 1000.

    // Use this for initialization
    void Start()
    {
        // Put my sphere in the middle:
        transform.position = new Vector3(0, 0, 0);

        // First, to get our objects, we need to read in the entire json as text, then deserialize it.
        // Note that location of executing assemebly is: *\netCDF-Unity\Interactive Visualization\Library\ScriptAssemblies\Assembly-CSharp.dll
        // the .dll itself is a folder.
        string dataLoc = Assembly.GetExecutingAssembly().Location + "\\..\\..\\..\\Assets\\Test Scripts\\JSON\\data.json";
        List<SfcPresTempData> dataSet = JsonConvert.DeserializeObject<List<SfcPresTempData>>(File.ReadAllText(dataLoc));

        // printing player attributes
        scoreText.text = "";
        foreach (SfcPresTempData d in dataSet)
        {
            scoreText.text += "Lat: " + d.lat + ", Lon: " + d.lon+ ", Pres: " + d.pres+ ", Temp: " + d.temp+ '\n';
            GeneratePt(d.lat, d.lon, d.alt, d.pres);
        } // end foreach

    } // end method Start()

    // this method generates a sphere representing data based on the parameters it receives. This only reflects temperature.
    void GeneratePt(float lat, float lon, float alt, float pres)
    {
        float rho, theta, phi;
        rho = (float)alt;
        theta = (float)lon * 180 / Mathf.PI; // note the conversion to rad
        phi = (90 - (float)lat) * 180 / Mathf.PI; // note the conversion to rad

        float r = rho * Mathf.Sin(phi);
        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);
        float z = rho * Mathf.Cos(phi);

        GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tmp.transform.position = new Vector3(x, y, z);
        // tmp.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F); // note that the Fs indicate floats; without them, it's a double.
        Renderer rd = (Renderer)tmp.GetComponent("Renderer");
        rd.material.color = new Color((float)(pres - 900) / (PRES_MAX - 900), 1 - (float)(pres - 900) / (PRES_MAX - 900), 0, 0.5F); // green for low levels, red for high.
        dataSpheres.Add(tmp);
    } // end method GeneratePt()

} // end program JsonDeserializationTest2
