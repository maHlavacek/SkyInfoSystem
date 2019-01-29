using System;
using System.Collections.Generic;

namespace SkiInfoSystem.Core
{
    public class Controller
    {


        public List<Slope> Slopes { get; private set; }

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
        }

        public void GetSlopes()
        {
            Slopes = CsvDataProvider.Instance.GetSlops() as List<Slope>;
        }

        public void GetSensorsForSlope()
        {
            foreach (Slope slope in Slopes)
            {
                slope.Sensors = CsvDataProvider.Instance.GetSensorsForSlope(slope.Id) as List<Sensor>;
            }
        }
    
        


    }
}
