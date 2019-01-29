using SkiInfoSystem.Utils;
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
      

        public List<Sensor> Sensors { get; set; }

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

        private int _avgTemperatur;
        private int _oldAvgTemperatur;

        private int _avgSolarRadiation;
        private int _oldAvgSolarRadiation;

        private int _avgWindIntensity;
        private int _oldAvgWindIntensity;

        public event EventHandler<string> ActualConditionChanged;

        public Slope(int id, string name)
        { 
            Id = id;
            Name = name;
        }

        public void InitializeSensors()
        {
            Sensors = Controller.Instance.GetSensorsForSlope(Id) as List<Sensor>;
            foreach (Sensor sensor in Sensors)
            {
                sensor.MeasurementOccured += Sensor_MeasurementOccured;
                sensor.Start();
            }
        }

        private void Sensor_MeasurementOccured(object sender, double value)
        {
            Sensor sensor = sender as Sensor;
            _oldAvgSolarRadiation = _avgSolarRadiation;
            _oldAvgTemperatur = _avgTemperatur;
            _oldAvgWindIntensity = _avgWindIntensity;
      
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
            
            CheckIfConditionHasChanged(_oldAvgSolarRadiation, _avgSolarRadiation);
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

        private void CheckIfConditionHasChanged(int oldCondition, int newCondition)
        {
            if(oldCondition != newCondition)
            {
                OnActualConditionChanged(oldCondition , newCondition);
            }
        }

        public void OnActualConditionChanged(int oldCondition, int newCondition)
        {
            DateTime dateTime = FastClock.Instance.Time;
            SlopeCondition oldCon = (SlopeCondition)Enum.Parse(typeof(SlopeCondition), Convert.ToString(oldCondition));
            SlopeCondition newCon = (SlopeCondition)Enum.Parse(typeof(SlopeCondition), Convert.ToString(newCondition));
            string massege = String.Format($"{ dateTime,10} - { Name,10} { oldCon,80} => { newCon,5}");

            ActualConditionChanged?.Invoke(this, massege);
        }


    }
}
