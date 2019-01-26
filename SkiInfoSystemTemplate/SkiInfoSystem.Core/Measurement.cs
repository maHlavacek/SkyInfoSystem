using System;

namespace SkiInfoSystem.Core
{
    /// <summary>
    /// Beschreibt eine Einzelmessung eines Sensors.
    /// </summary>
    public class Measurement 
    {
        /// <summary>
        /// Id des Sensors
        /// </summary>
        public int SensorId { get; }

        /// <summary>
        /// Wert der Messung
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Zeitpunkt der Messung (gerechnet vom Startzeitpunkt).
        /// </summary>
        public DateTime Timestamp { get; }

        public Measurement(DateTime timestamp, int sensorId, double value)
        {
            Timestamp = timestamp;
            SensorId = sensorId;
            Value = value;
        }

    }
}
