using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using SkiInfoSystem.Utils;

namespace SkiInfoSystem.Core
{
    public class Controller
    {
        #region Fields
        private List<Slope> slopes;
        const string FileNameForMeasurements = "measurements.csv";
        const string FileNameForSensors = "sensors.csv";
        const string FileNameForSlopes = "slopes.csv";

        #endregion
        public Controller()
        {

        }

        #region Methods
        public List<Measurement> ReadMeasurementsFromCsv()
        {
            string[] lines = GetAllLines(FileNameForMeasurements);
            string[] column;
            List<Measurement> measurements = new List<Measurement>();
            string date;
            string time;
            int iD;
            double value;


            for (int i = 0; i < lines.Length; i++)
            {
                column = lines[i].Split(';');
                date = column[0];
                time = column[1];
                DateTime.TryParse(date + " " + time, out DateTime timeStamp);
                iD = int.Parse(column[2]);
                value = double.Parse(column[3]);
                measurements.Add(new Measurement(timeStamp, iD, value));
            }
            return measurements;
        }

        public List<Sensor> ReadSensorsFromCsv()
        {

            string[] lines = GetAllLines(FileNameForSensors);
            string[] columns;
            List<Sensor> sensors = new List<Sensor>();
            int iD;
            int slopeId;
            MeasurementType type;

            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                iD = int.Parse(columns[0]);
                slopeId = int.Parse(columns[1]);
                type = (MeasurementType)Enum.Parse(typeof(MeasurementType), columns[2]);
                sensors.Add(new Sensor(iD, slopeId, type));
            };
            return sensors;
        }

        public string[] GetAllLines(string filename)
        {
            string path = MyFile.GetFullNameInApplicationTree(filename);
            if (File.Exists(path))
            {
                return null;
            }
            return File.ReadAllLines(path, Encoding.Default);
        }
        #endregion


    }
}
