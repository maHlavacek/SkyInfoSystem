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

        private IEnumerable<Sensor> _sensors;

        private const int PERFECT_MAX_TEMP = 8;
        private const int PERFECT_MIN_TEMP = -8;
        private const int OK_MAX_TEMP = 13;
        private const int OK_MIN_TEMP = -18;

        private const int PERFECT_MAX_WIND = 7;
        private const int PERFECT_MIN_WIND = 0;
        private const int OK_MAX_WIND = 10;
        private const int OK_MIN_WIND = 7;

        private const int PERFECT_MAX_SOLAR = 230;
        private const int PERFECT_MIN_SOLAR = 200;
        private const int OK_MAX_SOLAR = 245;
        private const int OK_MIN_SOLAR = 190;

        private double _avgTemperatur;
        private double _avgSolarRadiation;
        private double _avgWindIntensity;

        public Slope(int id, string name)
        { 
            Id = id;
            Name = name;
            CsvDataProvider sensors = new CsvDataProvider();
            _sensors = sensors.GetSensorsForSlope(id);

            foreach (Sensor sensor in _sensors)
            {
                sensor.MeasurementOccured += Sensor_MeasurementOccured;
            }

        }

        private void Sensor_MeasurementOccured(object sender, double value)
        {
            Sensor currentSensor = sender as Sensor;
            foreach (Sensor sensor in _sensors)
            {
                if(sensor.MeasurementType == MeasurementType.SolarRadiation)
                {
                    _avgSolarRadiation = GetSlopeCondition(value, PERFECT_MIN_SOLAR, PERFECT_MAX_SOLAR, OK_MIN_SOLAR, OK_MAX_SOLAR);
                }

                if (sensor.MeasurementType == MeasurementType.Temperature)
                {
                    _avgTemperatur = GetSlopeCondition(value, PERFECT_MIN_TEMP, PERFECT_MAX_TEMP, OK_MIN_TEMP, OK_MAX_TEMP);
                }
                if (sensor.MeasurementType == MeasurementType.WindIntensity)
                {
                    _avgSolarRadiation = GetSlopeCondition(value, PERFECT_MIN_WIND, PERFECT_MAX_WIND, OK_MIN_WIND, OK_MAX_WIND);
                }
            }
        }


        private int GetSlopeCondition(double avg, int perfectMin,int perfectMax,int okMin,int okMax)
        {
            if(avg == 0)
            {
                return (int)SlopeCondition.Unknown;
            }
            if(avg >= perfectMin && avg < perfectMax)
            {
                return (int)SlopeCondition.Perfect;
            }
            if (avg >= okMin && avg < okMax)
            {
                return (int)SlopeCondition.Ok;
            }
            else
            {
                return (int)SlopeCondition.Bad;
            }

        }
    }
}
