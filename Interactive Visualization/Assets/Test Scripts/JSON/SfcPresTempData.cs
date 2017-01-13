/* This class, for testing purposes, represents a data point with a lat, lon, temp, and pres.
 * The objects of this class are deserialized (read in) from data.json, and each of the objects represents a data point from the test file
 *  sfc_pres_temp.nc.
 * In the future, we might just want to make a more generic data point class and use interfaces, so a data point can just implement all the
 *  different variables it has data for.
 * */

using System;

[System.Serializable]
public class SfcPresTempData
{
    // fields: for test purposes, we are assuming certain units. But later we can add fields so users can customize their units.
    public float lat; // latitude. Presumably in degrees N of the Equator.
    public float lon; // longitude. Presumably in degrees E of the Prime Meridian.
    public float alt = 5; // altitude. We are arbitrarily setting this equal to 5 for testing purposes, because this is a 2D data set.
    public float pres; // pressure. Presumably in hPa.
    public float temp; // temperature. Presumably in degrees Celsius.

    /*
    // apparently this is how C# does getters. But I couldn't get it to work for some reason:
    public float Lat { get { return lat; } }
    public float Lon { get { return lon; } }
    public float Alt { get { return alt; } }
    public float Pres { get { return pres; } }
    public float Temp { get { return temp; } }
    */
} // end class SfcTempPresData
