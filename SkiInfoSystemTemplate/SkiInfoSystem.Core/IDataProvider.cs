using System.Collections.Generic;

namespace SkiInfoSystem.Core
{
    internal interface IDataProvider
    {
        IEnumerable<Slope> GetSlops();
        IEnumerable<Sensor> GetSensorsForSlope(int slopeId);
        IEnumerable<Measurement> GetMeasurmentsForSensor(int sensorId);
    }
}
