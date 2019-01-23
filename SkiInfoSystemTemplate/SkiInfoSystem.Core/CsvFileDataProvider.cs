using SkiInfoSystem.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiInfoSystem.Core
{
    class CsvFileDataProvider : IDataProvider
    {
        const string FileNameForMeasurements = "measurements.csv";
        const string FileNameForSensors = "sensors.csv";
        const string FileNameForSlopes = "slopes.csv";
        public IEnumerable<Measurement> GetMeasurmentsForSensor(int sensorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sensor> GetSensorsForSlope(int slopeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Slope> GetSlops()
        {
            throw new NotImplementedException();
        }
       
    }
}
