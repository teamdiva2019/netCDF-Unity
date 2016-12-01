The folder Interactive Visualization is a Unity project attempting to visualize standard scatter plots of netCDF files at the moment. The intended functionality is that the user would click on the plot in Unity, and the next timestamped plot would be generated and shown. The actual plotting of the data takes place in Python 3.5. These files are in the Assets/ReadnCDF folder. A generated plot is saved in the Resources directory of the Unity project.
A word of recommendation: If anyone checks this out and decides to work on it, it would be preferred if all the file references and local instead of static. In other words, refrain from referencing files as "C:/Users/[username]/...." because then it will not work on others' work environments. Instead, try and do referencing such as "../Scripts/....." where the '..' signifies parent directory.

Also if anyone is interested in checking out the Roll a Ball Tutorial given on the Unity site that folder is there as well.

~README created by Mughil~
