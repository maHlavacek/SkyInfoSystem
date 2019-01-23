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
        public List<Sensor> ListOfSensor { get; set; }
        public Slope(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
    }
}
