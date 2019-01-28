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
        public IEnumerable<Sensor> Sensor { get; private set; }

        private CsvFileDataProvider _sensors;

        public Slope(int id, string name)
        {
            Id = id;
            Name = name;
            _sensors = new CsvFileDataProvider();
            Sensor = _sensors.GetSensorsForSlope(id);
        }

    }
}
