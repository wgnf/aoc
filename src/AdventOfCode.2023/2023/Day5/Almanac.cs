namespace AdventOfCode._2023._2023.Day5;

internal sealed class Almanac
{
    public List<Range> SeedRanges { get; } = [];

    public RangeMapSet SeedToSoilMap { get; } = new();

    public RangeMapSet SoilToFertilizerMap { get; } = new();

    public RangeMapSet FertilizerToWaterMap { get; } = new();

    public RangeMapSet WaterToLightMap { get; } = new();

    public RangeMapSet LightToTemperatureMap { get; } = new();

    public RangeMapSet TemperatureToHumidityMap { get; } = new();

    public RangeMapSet HumidityToLocationMap { get; } = new();

    public long GetClosestSeedLocation()
    {
        var soilRanges = SeedToSoilMap.GetRanges(SeedRanges);
        var fertilizerRanges = SoilToFertilizerMap.GetRanges(soilRanges);
        var waterRanges = FertilizerToWaterMap.GetRanges(fertilizerRanges);
        var lightRanges = WaterToLightMap.GetRanges(waterRanges);
        var temperatureRanges = LightToTemperatureMap.GetRanges(lightRanges);
        var humidityRanges = TemperatureToHumidityMap.GetRanges(temperatureRanges);
        var locationRanges = HumidityToLocationMap.GetRanges(humidityRanges);

        var closestLocationRange = locationRanges.MinBy(range => range.Start);
        if (closestLocationRange == null)
        {
            throw new InvalidOperationException("Unable to find closest location range");
        }

        return closestLocationRange.Start;
    }
}
