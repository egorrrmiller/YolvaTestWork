using Newtonsoft.Json;
using YolvaTestWork.Enums;
using YolvaTestWork.GeoServices.OSM.Enums;
using YolvaTestWork.GeoServices.OSM.Models;
using YolvaTestWork.GeoServices.OSM.TypePolygon;
using YolvaTestWork.Models;

namespace YolvaTestWork.GeoServices.OSM;

public class OpenStreetMap : IGeoService
{
    private readonly HttpClient _httpClient = new();
    private readonly IEnumerable<IPolygon> _polygon;

    public OpenStreetMap(IEnumerable<IPolygon> polygon)
    {
        _polygon = polygon;
    }

    public Task<bool> CanExecute(GeoPolygonModel geoPolygonModel)
    {
        return Task.FromResult(geoPolygonModel.GeoService == GeoServicesEnum.OpenStreetMap);
    }

    public async Task<string> Execute(GeoPolygonModel geoPolygonModel)
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62");

        var httpResponse = await _httpClient
            .GetAsync(
                $"https://nominatim.openstreetmap.org/search?q={geoPolygonModel.Address}&format=json&polygon_geojson=1")
            .GetAwaiter().GetResult().Content.ReadAsStringAsync();

        var geoJson = JsonConvert.DeserializeObject<List<OpenStreetMapJsonModel>>(httpResponse)?.FirstOrDefault()
            ?.GeoJson;
        var typePolygonEnum = Enum.Parse<OsmTypePolygonEnum>(geoJson!.Type);


        return await _polygon.FirstOrDefault(prop => prop.CanExecute(typePolygonEnum).Result)
            ?.Execute(geoJson, geoPolygonModel)!;
    }
}