The folder Interactive Visualization is a Unity project attempting to visualize standard scatter plots of netCDF files at the moment. The intended functionality is that the user would click on the plot in Unity, and the next timestamped plot would be generated and shown. The actual plotting of the data takes place in Python 3.5. These files are in the Assets/ReadnCDF folder. A generated plot is saved in the Resources directory of the Unity project.
A word of recommendation: If anyone checks this out and decides to work on it, it would be preferred if all the file references and local instead of static. In other words, refrain from referencing files as "C:/Users/[username]/...." because then it will not work on others' work environments. Instead, try and do referencing such as "../Scripts/....." where the '..' signifies parent directory.

Also if anyone is interested in checking out the Roll a Ball Tutorial given on the Unity site that folder is there as well.

~README created by Mughil~


12.28.16
I messed around with spherical coordinates a bit and created a scene called SphereCoordsTest in netCDF-Unity/Interactive Visualization/Assets/_Scenes. You guys are welcome to check it out. It uses a Script I wrote that can be found in */Assets/Scripts. ~Naeem

1.8.16
Tried to fiddle around with having to read netCDF with C# but to no avail, even with all of the converted .dll's from the .jar library and through IKVM (a method to convert .jar to .dll). Thus, I plan to transition to using Javascript mode. I added a skeleon Javascript file and the .dll's I used.
~Mughil