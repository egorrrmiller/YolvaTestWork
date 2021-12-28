using Newtonsoft.Json;
using YolvaTestWork.Enums;
using YolvaTestWork.Models;

namespace YolvaTestWork.GeoServices.OSM;

public class OpenStreetMap : IGeoService
{
    private readonly xNet.HttpRequest _httpRequest = new();

    public Task<bool> CanExecute(GeoPolygonModel geoPolygonModel)
    {
        return Task.FromResult(geoPolygonModel.GeoService == GeoServicesEnum.OpenStreetMap);
    }

    public Task<string> Execute(GeoPolygonModel geoPolygonModel)
    {
        _httpRequest.UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62";
        var httpResponse = _httpRequest
            .Get($"https://nominatim.openstreetmap.org/search?q={geoPolygonModel.Address}&format=json&polygon_geojson=1")
            .ToString();

        var json = JsonConvert.DeserializeObject<List<OpenStreetMapJsonModel>>(httpResponse)?.FirstOrDefault()?.Geojson;
        var typePolygonEnum = Enum.Parse<OsmTypePolygonEnum>(json!.Type);

        object results;

        switch (typePolygonEnum)
        {
            case OsmTypePolygonEnum.Polygon:
                List<object> polygon = new();
                var coordinates =
                    JsonConvert.DeserializeObject<List<List<object>>>(json.Coordinates[0].ToString()!);
                for (int i = 0; i < coordinates!.Count; i += geoPolygonModel.DotPolygon - 1)
                    polygon.Add(coordinates[i]);

                results = polygon;
                break;

            case OsmTypePolygonEnum.MultiPolygon:
                List<object> coords = new();
                List<List<List<object>>> result = new();
                foreach (var coordinate in json.Coordinates)
                {
                    var coordinateJson =
                        JsonConvert.DeserializeObject<List<List<List<object>>>>(coordinate.ToString()!);
                    foreach (var _ in coordinateJson!)
                    {
                        for (int i = 0; i < _.Count; i += geoPolygonModel.DotPolygon - 1)
                            coords.Add(_[i]);

                        result.Add(new List<List<object>> {coords});

                        coords = new List<object>();
                    }
                }

                results = result;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Task.FromResult(JsonConvert.SerializeObject(results));
    }
}