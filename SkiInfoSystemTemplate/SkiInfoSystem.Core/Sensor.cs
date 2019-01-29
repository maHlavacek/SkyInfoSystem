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

        private double _currentValue;

        public double LastValue { get; private set; }
       // private double _currentValue;

       // private CsvFileDataProvider _measurements;
        public IEnumerable<Measurement> Measurements { get;private set; }

        public Sensor(int id, int slopeId, MeasurementType measurementType)
        {

            Id = id;
            SlopeId = slopeId;
            MeasurementType = measurementType;
            CsvFileDataProvider _measurements = new CsvFileDataProvider();
            IEnumerable<Measurement> measurements = _measurements.GetMeasurmentsForSensor(id);

            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime time)
        {
            foreach (Measurement measurement in Measurements)
            {
                if(measurement.Timestamp == time)
                {
                    _currentValue = measurement.Value;

                    if(_currentValue != LastValue)
                    {
                        MeasurementOccured?.Invoke(this, measurement.Value);
                        LastValue = measurement.Value;
                    }
                }
            }
        }
    }
}
