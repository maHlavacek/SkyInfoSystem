using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiInfoSystem.Utils;

namespace SkiInfoSystem.Core
{
    public class CsvDataProvider : IDataProvider
    {

        private IEnumerable<Slope> _slopesStore;
        private IEnumerable<Measurement> _measurementsStore;
        private IEnumerable<Sensor> _sensorsStore;
        private const string FileNameForSlopes = "slopes.csv";
        private const string FileNameForMEasurements = "measurements.csv";
        private const string FileNameForSensors = "sensors.csv";
        private static CsvDataProvider _instance;


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
        private CsvDataProvider()
        {
            _slopesStore = CreateSlopesList();
            _sensorsStore = CreateSensorList();
            _measurementsStore = CreateMeasurementsList();

        }

        private List<Sensor> CreateSensorList()
        {
            List<Sensor> sensors = new List<Sensor>();
            string fullPath = MyFile.GetFullNameInApplicationTree(FileNameForSensors);
            if (!File.Exists(fullPath)) throw new ArgumentNullException("Datei nicht vorhanden");
            string[] lines = File.ReadAllLines(fullPath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] colums = lines[i].Split(';');
                int id = Convert.ToInt32(colums[0]);
                int slopid = Convert.ToInt32(colums[1]);
                string temp = colums[2];
                MeasurementType measurementtype = (MeasurementType)Enum.Parse(typeof(MeasurementType), colums[2]);
                sensors.Add(new Sensor(id, slopid, measurementtype));
            }
            return sensors;
        }

        private List<Measurement> CreateMeasurementsList()
        {
            List<Measurement> measurements = new List<Measurement>();
            string fullPath = MyFile.GetFullNameInApplicationTree(FileNameForMEasurements);
            if (!File.Exists(fullPath)) throw new ArgumentNullException("Datei nicht vorhanden");
            string[] lines = File.ReadAllLines(fullPath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] colums = lines[i].Split(';');
                DateTime timestamp = Convert.ToDateTime(colums[0] + " " + colums[1]);
                int sensorid = Convert.ToInt32(colums[2]);
                double value = Convert.ToDouble(colums[3]);
                measurements.Add(new Measurement(timestamp, sensorid, value));
            }
            return measurements;
        }
        private List<Slope> CreateSlopesList()
        {
            List<Slope> slopes = new List<Slope>();
            string fullPath = MyFile.GetFullNameInApplicationTree(FileNameForSlopes);
            if (!File.Exists(fullPath)) throw new ArgumentNullException("Datei nicht vorhanden");
            string[] lines = File.ReadAllLines(fullPath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] colums = lines[i].Split(';');
                Int32 id = Convert.ToInt32(colums[0]);
                slopes.Add(new Slope(id, colums[1]));
            }
            return slopes;
        }


        public IEnumerable<Measurement> GetMeasurmentsForSensor(int sensorId)
        {
            if (_measurementsStore == null)
            {
                _measurementsStore = CreateMeasurementsList();
            }
            List<Measurement> measurments = new List<Measurement>();
            foreach (Measurement measurment in _measurementsStore)
            {
                if (measurment.SensorId == sensorId)
                {
                    measurments.Add(measurment);
                }
            }
            return measurments;
        }

        public IEnumerable<Slope> GetSlops()
        {
            if (_slopesStore == null)
            {
                _sensorsStore = CreateSensorList();
            }
            return _slopesStore;
        }

        public IEnumerable<Sensor> GetSensorsForSlope(int slopeId)
        {
            if (_sensorsStore == null)
            {
                _slopesStore = CreateSlopesList();
            }
            List<Sensor> sensors = new List<Sensor>();
            foreach (Sensor sensor in _sensorsStore)
            {
                if (sensor.SlopeId == slopeId)
                {
                    sensors.Add(sensor);
                }
            }
            if (sensors == null) throw new ArgumentNullException("Keine Sensoren in der Liste");
            return sensors;
        }


    }
}
