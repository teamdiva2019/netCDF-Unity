from netCDF4 import Dataset
import numpy as np
from datetime import datetime, timedelta
import matplotlib.pyplot as plt
from mpl_toolkits.basemap import Basemap

import os


f = ''
# If time.txt doesn't exist, then create it and write 0 to it.
if not os.path.isfile('time.txt'):
    f = open('time.txt', 'a+')
    f.write('0')
    f.close()
    f = open('time.txt', 'r+')
else:
    f = open('time.txt', 'r+')

index = int(f.read())

myNCFile = 'mslp.2002.nc'
fh = Dataset(myNCFile, mode='r')

lons = fh.variables['lon'][:]
lats = fh.variables['lat'][:]
time = fh.variables['time'][:]
mslp = fh.variables['mslp'][:]
mslpUnits = fh.variables['mslp'].units

# Probably should try and parse later....
start = datetime.strptime('1800-01-01 00:00:00', '%Y-%m-%d %H:%M:%S')
# Add the hours specified in the time array
newtime = start + timedelta(hours=time[index])

# Clear contents of file
f.seek(0)
f.truncate()
# Save the next index
f.write(str(index + 1))

# Create scatter plot at time index and latitude '36' - equator
plt.scatter(lons, mslp[index][36])
# Set labels, title, and save image
plt.xlabel('Longitude')
plt.ylabel('Pressure')
plt.title('Sea Level Pressure at Equator at ' + '{:%Y-%m-%d %H:%M:%S}'.format(newtime))
plt.savefig('..\\Resources\\Plot.png')

f.close()

# lon_0 = lons.mean()
# lat_0 = lats.mean()
#
# m = Basemap(width=5000000, height=3500000, resolution='l',
#             projection='stere', lat_ts=40, lon_0=lon_0, lat_0=lat_0)
# lon, lat = np.meshgrid(lons, lats)
# xi, yi = m(lon, lat)
#
# # Plot Data
# cs = m.pcolor(xi,yi,np.squeeze(mslp[10]))
#
# # Add Grid Lines
# m.drawparallels(np.arange(-80., 81., 10.), labels=[1,0,0,0], fontsize=10)
# m.drawmeridians(np.arange(-180., 181., 10.), labels=[0,0,0,1], fontsize=10)
#
# # Add Coastlines, States, and Country Boundaries
# m.drawcoastlines()
# m.drawstates()
# m.drawcountries()
#
# # Add Colorbar
# cbar = m.colorbar(cs, location='bottom', pad="10%")
# cbar.set_label(mslpUnits)
#
# # Add Title
# plt.title('MSLP at Time = 0')
#
# plt.show()
