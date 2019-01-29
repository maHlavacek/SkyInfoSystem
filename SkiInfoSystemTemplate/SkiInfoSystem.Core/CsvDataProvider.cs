using SkiInfoSystem.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiInfoSystem.Core
{
    class CsvDataProvider : IDataProvider
    {
        private const string FileNameForMeasurements = "Measurements.csv";
        private const string FileNameForSensors = "Sensors.csv";
        private const string FileNameForSlopes = "Slopes.csv";
        private static CsvDataProvider _instance;

        private IEnumerable<Measurement> _measurements;
        private IEnumerable<Sensor> _sensors;
        private IEnumerable<Slope> _slopes;


        public static CsvDataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CsvDataProvider();
                }
                return _instance;
            }
        }

        public List<Measurement> GetMeasurementsFromCsv()
        {
            string[] lines = GetAllLines(FileNameForMeasurements);
            string[] columns;
            List<Measurement> measurements = new List<Measurement>();
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                DateTime dateTime = DateTime.Parse(columns[0] + " " + columns[1]);
                int sensId = int.Parse(columns[1]);
                double value = double.Parse(columns[2]);

                Measurement measurement = new Measurement(dateTime, sensId, value);
                measurements.Add(measurement);
            }
            return measurements;
        }


        public IEnumerable<Measurement> GetMeasurmentsForSensor(int sensorId)
        {
            return _measurements.Where(sId => sId.SensorId == sensorId);
        }


        public List<Sensor> GetSensorsFromCsv()
        {
            string[] lines = GetAllLines(FileNameForSensors);
            string[] columns;
            List<Sensor> sensors = new List<Sensor>();
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                int iD = int.Parse(columns[0]);
                int slopId = int.Parse(columns[1]);
                MeasurementType type = (MeasurementType)Enum.Parse(typeof(MeasurementType), columns[2]);

                Sensor sensor = new Sensor(iD, slopId, type);
                sensors.Add(sensor);
            }
            return sensors;
        }

        public IEnumerable<Sensor> GetSensorsForSlope(int slopeId)
        {
            return _sensors.Where(w => w.SlopeId == slopeId);
        }


        public List<Slope> GetSlopsFromCsv()
        {
            string[] lines = GetAllLines(FileNameForSensors);
            string[] columns;
            List<Slope> slopes = new List<Slope>();
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                int iD = int.Parse(columns[0]);
                string name = columns[1];
                Slope slope = new Slope(iD, name);

                slopes.Add(slope);
            }
            return slopes;
        }

        public IEnumerable<Slope> GetSlops()
        {
            return _slopes;
        }

        public string[] GetAllLines(string filename)
        {
            string path;
            path = MyFile.GetFullNameInApplicationTree(filename);
            if (File.Exists(path))
            {
                return File.ReadAllLines(path, Encoding.Default);
            }
            else return null;
        }
    }
}
