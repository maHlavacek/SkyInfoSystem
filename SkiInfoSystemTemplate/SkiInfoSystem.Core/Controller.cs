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
