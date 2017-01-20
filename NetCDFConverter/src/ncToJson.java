/**
 * Created by mughi on 1/18/2017.
 */

import ucar.ma2.*;
import ucar.nc2.*;
import org.kopitubruk.util.json.*;

import java.io.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*
Assuming that all the netCDF files have:
    time (double),
    latitude (double),
    longitude (double),
    one or more actual DATA variables that are 3-dimensional
Well anyways, this is what appears to be the structure of the files of the
Climate Forecast System Reanalysis (CFSR) data.
Also assuming that the data is in (time, lat, long).
~Mughil
 */

public class ncToJson {
    public static void main(String[] args) {
        // Change the line below for your own netCDF file
        String fileName = new File("NetCDF Files/pressfc201012.nc").getAbsolutePath();
        NetcdfFile ncfile = null;
        try {
            ncfile = NetcdfFile.open(fileName);
        } catch (IOException e) {
            e.printStackTrace();
        }
        Variable t = ncfile.findVariable("time");
        Variable lat = ncfile.findVariable("latitude");
        Variable longit = ncfile.findVariable("longitude");
        // Create the array to hold the data and remove the
        // time, latitude, and longitude
        List<Variable> data = ncfile.getVariables();
        data.remove(t);
        data.remove(lat);
        data.remove(longit);

        // Get the array of times, latitudes, and longitudes
        Array timeArr = null;
        Array latitudeArr = null;
        Array longitudeArr = null;
        try {
            timeArr = t.read();
            latitudeArr = lat.read();
            longitudeArr = longit.read();
        } catch (IOException e) {
            e.printStackTrace();
        }
        Index timeIndex = timeArr.getIndex();


        System.out.println("Reading data into arrays...");
        // This will have the JSON data.
        List<HashMap<String, Object>> maps = new ArrayList<>();

        while (timeIndex.currentElement() != timeIndex.getSize() - 1) {
            HashMap<String, Object> timeMap = new HashMap<>();
            timeMap.put("time", timeArr.getDouble(timeIndex));

            List<HashMap<String, Object>> dataAtTime = new ArrayList<>();
            // Reset indices
            Index latIndex = latitudeArr.getIndex();
            Index longIndex = longitudeArr.getIndex();
            while (latIndex.currentElement() != latIndex.getSize() - 1) {
                while (longIndex.currentElement() != longIndex.getSize() - 1) {
                    HashMap<String, Object> dd = new HashMap<>();
                    dd.put("latitude", latitudeArr.getDouble(latIndex));
                    dd.put("longitude", longitudeArr.getDouble(longIndex));
                    try {
                        for (Variable v : data) {
                            dd.put(v.getName(), v.read(timeIndex.currentElement() + ", " +
                                    latIndex.currentElement() + ", " +
                                    longIndex.currentElement()));
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                    } catch (InvalidRangeException e) {
                        e.printStackTrace();
                    }
                    dataAtTime.add(dd);
                    // Increment!
                    longIndex.incr();
                }
                // Increment!
                latIndex.incr();
            }
            timeMap.put("dataAtTime", dataAtTime);

            maps.add(timeMap);

            // Increment!
            timeIndex.incr();

        }
        System.out.println("Converting to JSON...");
        String jsonData = JSONUtil.toJSON(maps);
        String outFileName = new File("Outputted JSON/test.json").getAbsolutePath();
        // Write to a JSON file
        System.out.println("Writing to file...");
        try {
            PrintWriter printWriter = new PrintWriter(outFileName, "UTF-8");
            printWriter.println(jsonData);
            printWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        //writeToJSON();

    }

    public static void writeToJSON() {
        List<HashMap<String, Object>> maps = new ArrayList<>();
        HashMap<String, Object> map1 = new HashMap<>();
        map1.put("time", 78);

        List<HashMap<String, Double>> dataAtTime1 = new ArrayList<>();
        HashMap<String, Double> dd1 = new HashMap<>();
        dd1.put("latitude", 90.0);
        dd1.put("longitude", -37.0);
        dd1.put("PRES_surface", 873.0);
        HashMap<String, Double> dd2 = new HashMap<>();
        dd2.put("latitude", 89.0);
        dd2.put("longitude", -34.0);
        dd2.put("PRES_surface", 473.0);
        dataAtTime1.add(dd1);
        dataAtTime1.add(dd2);

        map1.put("dataAtTime", dataAtTime1);

        HashMap<String, Object> map2 = new HashMap<>();
        map2.put("time", 80);
        List<HashMap<String, Double>> dataAtTime2 = new ArrayList<>();
        dataAtTime2.add(dd1);
        dataAtTime2.add(dd2);
        map2.put("dataAtTime", dataAtTime2);

        maps.add(map1);
        maps.add(map2);

        String jsonData = JSONUtil.toJSON(maps);
        String fileName = new File("Outputted JSON/test.json").getAbsolutePath();
        // Write to a JSON file
        try {
            PrintWriter printWriter = new PrintWriter(fileName, "UTF-8");
            printWriter.println(jsonData);
            printWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        // Now try and read from it
        try {
            BufferedReader br = new BufferedReader(new FileReader(fileName));
            ArrayList<Object> readJSON = (ArrayList<Object>) JSONParser.parseJSON(br);
            //System.out.println(readJSON);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
