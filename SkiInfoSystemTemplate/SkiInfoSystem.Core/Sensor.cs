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

        private double _lastMeasurement;
        public int Id { get; }
        public int SlopeId { get; }
        public MeasurementType MeasurementType { get; }

        private List<Measurement> _measurements;

        public Sensor(int id, int slopeId, MeasurementType measurementType)
        {
            Id = id;
            SlopeId = slopeId;
            MeasurementType = measurementType;
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime time)
        {
            foreach (Measurement measurement in _measurements)
            {
                if(measurement.Timestamp == time)
                {
                    _lastMeasurement = measurement.Value;
                    OnMeasurementOccured(this, measurement.Value);
                }

            }
        }

        public void OnMeasurementOccured(object sender, double measurement)
        {
            if(measurement != _lastMeasurement)
            {
                MeasurementOccured?.Invoke(this, measurement);
            }
        }

    }
}
