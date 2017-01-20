The folder Interactive Visualization is a Unity project attempting to visualize standard scatter plots of netCDF files at the moment. The intended functionality is that the user would click on the plot in Unity, and the next timestamped plot would be generated and shown. The actual plotting of the data takes place in Python 3.5. These files are in the Assets/ReadnCDF folder. A generated plot is saved in the Resources directory of the Unity project.
A word of recommendation: If anyone checks this out and decides to work on it, it would be preferred if all the file references and local instead of static. In other words, refrain from referencing files as "C:/Users/[username]/...." because then it will not work on others' work environments. Instead, try and do referencing such as "../Scripts/....." where the '..' signifies parent directory.

Also if anyone is interested in checking out the Roll a Ball Tutorial given on the Unity site that folder is there as well.

~README created by Mughil~


12.28.16
I messed around with spherical coordinates a bit and created a scene called SphereCoordsTest in netCDF-Unity/Interactive Visualization/Assets/_Scenes. You guys are welcome to check it out. It uses a Script I wrote that can be found in */Assets/Scripts. ~Naeem

1.8.16
Tried to fiddle around with having to read netCDF with C# but to no avail, even with all of the converted .dll's from the .jar library and through IKVM (a method to convert .jar to .dll). Thus, I plan to transition to using Javascript mode. I added a skeleon Javascript file and the .dll's I used.
~Mughil

1.20.16
Large update this time :). I completely aimed to generalize the NetCDF to JSON converter. To do that I took care of organizational things such deleting the old NetCDF Java folder (sorry Naeem) and created the new project folder NetCDFConverter. You would open this folder as a project in IntelliJ if you want to edit the source code. In this folder, we have NetCDF Files, which houses the netCDF files you want to convert, and Outputted JSON, which houses the converted JSON. Additionally, a new dependency has been added called JSONUtil-1.10.4.jar which is needed to quickly convert Java Objects to JSON. A quick
explanation: basically I utilized ArrayLists and HashMaps to create the tree structure and had the library parse it and generate a String which I could simply write.
Now to the assumptions I have made: I have assumed that the netCDF files have a time, latitude, longitude and that the actual data of the file is indexed by [time][latitude][longitude]. Also assumed that they are of type Double (makes things a bit more efficient when reading).
If you add in your own netCDF file to the NetCDF Files folder make sure to change the first line of the main method. Once written, the JSON file will be called "test.json" and when you view it won't look pretty because it's all in one line but it will work as intended when you try and read from it again using JSONUtils. One final note is that
this just a preliminary setup as I'll do lots of cleaning of the code later to make it more efficient. Anyways thanks for reading this far :).
~Mughil