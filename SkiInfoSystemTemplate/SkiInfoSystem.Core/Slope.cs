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
        public IEnumerable<Sensor> Sensors { get; private set; }

        public SlopeCondition SlopeCondition { get; private set; }

        private double _avgTemperatur;
        private double _avgWindIntensity;
        private double _avgSolarRadiation;

        private const int PERFEKT_MIN_TEMPERATURE = -8;
        private const int PERFEKT_MAX_TEMPERATURE = 8;
        private const int OK_MIN_TEMPERATURE = -18;
        private const int OK_MAX_TEMPERATURE = 13;

        private const int PERFEKT_MIN_SOLARRADIATION = 200;
        private const int PERFEKT_MAX_SOLARRADIATION = 230;
        private const int OK_MIN_SOLARRADIATION = 190;
        private const int OK_MAX_SOLARRADIATION = 245;

        private const int PERFEKT_MIN_WINDINTENSITY = 0;
        private const int PERFEKT_MAX_WINDINTENSITY = 7;
        private const int OK_MIN_WINDINTENSITY = 7;
        private const int OK_MAX_WINDINTENSITY = 10;


        public event EventHandler<string> ConditionsUpdated;
        //private CsvFileDataProvider _sensors;

        public Slope(int id, string name)
        {
            Id = id;
            Name = name;
            CsvFileDataProvider sensors = new CsvFileDataProvider();
            Sensors = sensors.GetSensorsForSlope(id);
            RegisterForMeasurementOccured();
        }

        public void RegisterForMeasurementOccured()
        {
            foreach (Sensor sensor in Sensors)
            {
                sensor.MeasurementOccured += OnMeasurementOccured;
            }
        }

        public void CalcualteAvgForSensors()
        {
            _avgSolarRadiation = Sensors.Where(type => type.MeasurementType == MeasurementType.SolarRadiation).Average(v => v.LastValue);
            _avgTemperatur = Sensors.Where(type => type.MeasurementType == MeasurementType.Temperature).Average(v => v.LastValue);
            _avgWindIntensity = Sensors.Where(type => type.MeasurementType == MeasurementType.WindIntensity).Average(v => v.LastValue);

            UpdateConditions(_avgSolarRadiation,_avgTemperatur,_avgWindIntensity);
           
        }

        private void OnMeasurementOccured(object sender, double value)
        {
            CalcualteAvgForSensors();
        }

        public int GetSlopeCondition(double avg, int perfektMin, int perfektMax, int okMin, int okMax)
        {
            SlopeCondition slopeCondition;
            if(avg == 0)
            {
                slopeCondition = SlopeCondition.Unknown;
            }
            else if (avg >= perfektMin && avg < perfektMax)
            {
                slopeCondition = SlopeCondition.Perfect;
            }
            else if (avg >= okMin && avg < okMax)
            {
                slopeCondition = SlopeCondition.Ok;
            }
            else
            {
                slopeCondition = SlopeCondition.Bad;
            }
            return (int)slopeCondition;
        }

        private void UpdateConditions(params double[] avg)
        {
            int tempCondition = GetSlopeCondition(_avgTemperatur, PERFEKT_MIN_TEMPERATURE, PERFEKT_MAX_TEMPERATURE, OK_MIN_TEMPERATURE, OK_MAX_TEMPERATURE);
            int solarCondition = GetSlopeCondition(_avgSolarRadiation, PERFEKT_MIN_SOLARRADIATION, PERFEKT_MAX_SOLARRADIATION, OK_MIN_SOLARRADIATION, OK_MAX_SOLARRADIATION);
            int windCondition = GetSlopeCondition(_avgWindIntensity, PERFEKT_MIN_WINDINTENSITY, PERFEKT_MAX_WINDINTENSITY, OK_MIN_WINDINTENSITY, OK_MAX_WINDINTENSITY);

            int slopeCondition = Math.Min(tempCondition, solarCondition);
            slopeCondition = Math.Min(slopeCondition, windCondition);

            SlopeCondition = (SlopeCondition)Enum.Parse(typeof(SlopeCondition), slopeCondition.ToString());

            OnConditionUpdated(SlopeCondition.ToString());
        //    ConditionsUpdated?.Invoke(this, massege);
        }

        public void OnConditionUpdated(string massege)
        {
            ConditionsUpdated?.Invoke(this, massege);
        }
        public override string ToString()
        {
            return $"Slope {this.Name} SlopeCondition: {SlopeCondition}";
        }
    }
}
