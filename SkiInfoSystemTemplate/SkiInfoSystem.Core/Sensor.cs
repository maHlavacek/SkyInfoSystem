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

        private CsvFileDataProvider _measurements;
        public IEnumerable<Measurement> Measurements { get;private set; }

        public Sensor(int id, int slopeId, MeasurementType measurementType)
        {
            Id = id;
            SlopeId = slopeId;
            MeasurementType = measurementType;
            _measurements = new CsvFileDataProvider();
            IEnumerable<Measurement> measurements = _measurements.GetMeasurmentsForSensor(id);
        }
    }
}
