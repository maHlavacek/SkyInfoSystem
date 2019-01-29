using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiInfoSystem.Core
{
    /// <summary>
    /// Repräsentiert einen Berghang/Piste
    /// </summary>
    public class Slope
    {
        public int Id { get; }
        public string Name { get; }

        private IEnumerable<Sensor> _sensors;



        public Slope(int id, string name)
        { 
            Id = id;
            Name = name;
            CsvDataProvider sensors = new CsvDataProvider();
            _sensors = sensors.GetSensorsForSlope(id);

            foreach (Sensor sensor in _sensors)
            {
                sensor.MeasurementOccured += Sensor_MeasurementOccured1;
            }

        }

        private void Sensor_MeasurementOccured1(object sender, double e)
        {
            throw new NotImplementedException();
        }

    }
}
