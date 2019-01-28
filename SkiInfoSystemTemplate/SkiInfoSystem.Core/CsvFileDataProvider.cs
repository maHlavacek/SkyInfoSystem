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
            string[] lines = GetAllLines(FileNameForMeasurements);
            string[] columns;
            IEnumerable<Measurement> iEnumMeasurement = new List<Measurement>();
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                DateTime time = DateTime.Parse(columns[0] + " " + columns[1]);
                int sensId = int.Parse(columns[2]);
                double value = double.Parse(columns[3]);

                Measurement measurement = new Measurement(time,sensId,value);
                iEnumMeasurement.Append(measurement).Where(w => w.SensorId == sensId);
            }
            return iEnumMeasurement;
        }

        public IEnumerable<Sensor> GetSensorsForSlope(int slopeId)
        {
            string[] lines = GetAllLines(FileNameForSensors);
            string[] columns;
            IEnumerable<Sensor> sensors = new List<Sensor>();
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                int iD = int.Parse(columns[0]);
                int slopId = int.Parse(columns[1]);
                MeasurementType type = (MeasurementType)Enum.Parse(typeof(MeasurementType), columns[3]);

                Sensor sensor = new Sensor(iD, slopId, type);
                sensors.Append(sensor).Where(w => w.SlopeId == slopeId);
            }
            return sensors;
        }

        public IEnumerable<Slope> GetSlops()
        {
            string[] lines = GetAllLines(FileNameForSlopes);
            string[] columns;
            IEnumerable<Slope> slopes = new List<Slope>();
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                int iD = int.Parse(columns[0]);
                string name = columns[1];
                Slope slope = new Slope(iD,name);

                slopes.Append(slope);
            }
            return slopes;
        }

        public string[] GetAllLines(string filename)
        {
            string path = MyFile.GetFullNameInApplicationTree(filename);

            if(File.Exists(path))
            {
                return File.ReadAllLines(path, Encoding.Default);
            }
            return null;
        }     
    }
}
