using SkiInfoSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiInfoSystem.Core
{
    internal class Sensor
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
        public IEnumerable<Measurement> Measurements { get;private set; }

        private double _lastValeu;

        public Sensor(int id, int slopeId, MeasurementType measurementType)
        {
            Id = id;
            SlopeId = slopeId;
            MeasurementType = measurementType;
            Measurements = CsvDataProvider.Instance.GetMeasurmentsForSensor(id);
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime time)
        {
            double avg;                
            avg = Measurements.Where(w => w.Timestamp == time && _lastValeu != w.Value).Average(a => a.Value);
            OnMeasurementOccured(avg);                             
        }

        public void OnMeasurementOccured(double value)
        {
            MeasurementOccured?.Invoke(this, value);
        }
    }
}
