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
        try {
            // Change the line below for your own netCDF file
            String fileName = new File("NetCDF Files/pressfc201012.nc").getAbsolutePath();
            long timestampLimit = 80; // how many timestamps to write per time?
            NetcdfFile ncfile = null;
            ncfile = NetcdfFile.open(fileName);
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
            Array timeArr = t.read();
            Array latitudeArr = lat.read();
            Array longitudeArr = longit.read();
            IndexIterator timeIter = timeArr.getIndexIterator();
            long timeInd = 0;

            System.out.println("Reading data into arrays and writing...");

            List<HashMap<String, Object>> maps = new ArrayList<>();

            String jsonData = "";
            String outFileName = new File("Outputted JSON/test.json").getAbsolutePath();
            // Write to a JSON file
            PrintWriter printWriter = new PrintWriter(outFileName, "UTF-8");
            printWriter.print("["); // print the initial open bracket.

            while (timeIter.hasNext()) {
                // If maps has more than timeStampLimit then write and clear
                if (maps.size() == timestampLimit) {
                    //printWriter.print(",");
                    System.out.println();
                    System.out.println("Writing to file...");
                    jsonData = JSONUtil.toJSON(maps);
                    jsonData = jsonData.substring(1, jsonData.length() - 1); // to get rid of []
                    printWriter.println(jsonData);
                    maps.clear();
                    printWriter.print("]");
                    printWriter.close();
                    System.exit(0);
                }

                HashMap<String, Object> timeMap = new HashMap<>();
                timeMap.put("time", timeIter.next()); // incremented time

                List<HashMap<String, Object>> dataAtTime = new ArrayList<>();

                // Start a new iteration
                IndexIterator latIter = latitudeArr.getIndexIterator();
                long latInd = 0;
                while (latIter.hasNext()) {
                    Object currLat = latIter.next();
                    IndexIterator longIter = longitudeArr.getIndexIterator();
                    long longInd = 0;
                    while (longIter.hasNext()) {
                        HashMap<String, Object> dd = new HashMap<>();
                        dd.put("latitude", currLat); // incremented latitude
                        dd.put("longitude", longIter.next()); // incremented longitude
                        for (Variable v : data)
                            dd.put(v.getName(), v.read(timeInd + ", " + latInd + ", " + longInd));
                        dataAtTime.add(dd);
                        longInd++; // increment longitude index
                    }
                    latInd++; // increment latitude index
                }
                timeMap.put("dataAtTime", dataAtTime);
                maps.add(timeMap);

                timeInd++; // increment time index

                System.out.print("\r" + timeInd + "/" + timeArr.getSize() + " timestamps completed.");
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InvalidRangeException e) {
            e.printStackTrace();
        }
        //writeToJSON();

    }

    public static void writeToJSON() {
        try {
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
            jsonData = jsonData.substring(1, jsonData.length() - 1);
            String fileName = new File("Outputted JSON/test.json").getAbsolutePath();
            // Write to a JSON file
            PrintWriter printWriter = new PrintWriter(fileName, "UTF-8");
            printWriter.print("[");
            printWriter.println(jsonData);
            printWriter.print("]");
            printWriter.close();
            // Now try and read it
            BufferedReader br = new BufferedReader(new FileReader(fileName));
            ArrayList<Object> readJSON = (ArrayList<Object>) JSONParser.parseJSON(br);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
