namespace SkiInfoSystem.Core
{
    /// <summary>
    /// Type einer Messung
    /// </summary>
    public enum MeasurementType
    {
        SolarRadiation = 10,
        Temperature = 20,
        WindIntensity = 30
    }

    /// <summary>
    /// Bedingungen am Berghang
    /// </summary>
    public enum SlopeCondition
    {
        Unknown = 0,
        Bad = 10,
        Ok = 20,
        Perfect = 30
    }
}
