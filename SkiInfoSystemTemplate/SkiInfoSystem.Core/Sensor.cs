using SkiInfoSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiInfoSystem.Core
{
    public class Sensor
    {
        /// <summary>
        /// Das Ereignis MeasurementOccured wird gefeuert, wenn sich eine Wertänderung ergeben hat.
        /// Dh. sollte der gleiche Messwert wie bei der letzten Messeung ermittelt werden, so wird
        /// dieses Ereignis nicht gefeuert!
        /// </summary>
        public event EventHandler<double> MeasurementOccured;

        public int Id { get; }
        public int SlopeId { get; }
        public MeasurementType MeasurementType { get; }
    

        public Sensor(int id, int slopeId, MeasurementType measurementType)
        {
            Id = id;
            SlopeId = slopeId;
            MeasurementType = measurementType;
        }

        public void OnMeasurementOccured(object sender, double measurement)
        {
            MeasurementOccured?.Invoke(this, measurement);
        }

    }
}
