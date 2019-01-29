using System;
using System.Collections.Generic;

namespace SkiInfoSystem.Core
{
    public class Controller
    {
        private static Controller _instance;


        public static Controller Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Controller();
                }
                return _instance;
            }
        }

        private Controller()
        {
            CsvDataProvider.Instance.GetSlops();
        }

        public void CreateSlops(EventHandler<string> ConditionChanged)
        {
            List<Slope> slops = CsvDataProvider.Instance.GetSlops() as List<Slope>;
            foreach (Slope slope in slops)
            {
                slope.ActualConditionChanged += ConditionChanged;
                slope.InitializeSensors();
            }
        }

        public IEnumerable<Measurement> GetMeasurementsForSensor(int sensorId)
        {
            return CsvDataProvider.Instance.GetMeasurmentsForSensor(sensorId);
        }

        public IEnumerable<Sensor> GetSensorsForSlope(int slopeId)
        {
            return CsvDataProvider.Instance.GetSensorsForSlope(slopeId);
        }

        public IEnumerable<Slope> GetSlopes()
        {
            return CsvDataProvider.Instance.GetSlops();
        }
        
    }
}
