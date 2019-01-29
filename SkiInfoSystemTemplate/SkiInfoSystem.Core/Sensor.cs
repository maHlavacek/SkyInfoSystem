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
        public Queue<Measurement> Measurements { get;private set; }
        private Measurement _lastMeasurement;

        public Sensor(int id, int slopeId, MeasurementType measurementType)
        {
            Id = id;
            SlopeId = slopeId;
            MeasurementType = measurementType;           
        }

        public void Start()
        {
            List<Measurement> list = Controller.Instance.GetMeasurementsForSensor(Id) as List<Measurement>;
            Measurements = new Queue<Measurement>(list);
          
            _lastMeasurement = Measurements.Dequeue();
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime time)
        {
            if(_lastMeasurement.Timestamp <= time)
            {
                OnMeasurementOccured(_lastMeasurement.Value);

                if(Measurements.Count != 0)
                {
                    Measurements.Dequeue();
                }
                else
                {
                    FastClock.Instance.OneMinuteIsOver -= Instance_OneMinuteIsOver;
                }
            }                     
        }

        public void OnMeasurementOccured(double value)
        {
            MeasurementOccured?.Invoke(this, value);
        }
    }
}
