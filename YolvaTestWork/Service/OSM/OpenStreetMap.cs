using Newtonsoft.Json;
using YolvaTestWork.Enums;
using YolvaTestWork.Models;

namespace YolvaTestWork.Service.OSM;

public class OpenStreetMap : IGeoPolygon
{
    private readonly xNet.HttpRequest _httpRequest = new ();
    public Task<bool> CanExecute(GeoPolygon geoPolygon)
    {
        return Task.FromResult(geoPolygon.GeoService == GeoServicesEnum.OpenStreetMap);
    }

    public Task<string> Execute(GeoPolygon geoPolygon)
    {
        _httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 Edg/96.0.1054.62";
        var httpResponse = _httpRequest.Get($"https://nominatim.openstreetmap.org/search?q={geoPolygon.Address}&format=json&polygon_geojson=1").ToString();
        
        var json = JsonConvert.DeserializeObject<List<OpenStreetMapJsonModel>>(httpResponse)?.FirstOrDefault()?.Geojson;

        object results = null!;
        
        if (json!.Type == TypePolygonEnum.Polygon.ToString())
        {
            List<object> polygon = new();
            var coordinates =
                JsonConvert.DeserializeObject<List<List<object>>>(json.Coordinates[0].ToString()!);
            for (int i = 0; i < coordinates!.Count; i += geoPolygon.DotPolygon - 1)
                polygon.Add(coordinates[i]);

            results = polygon;
        }
        else if (json.Type == TypePolygonEnum.MultiPolygon.ToString())
        {
            List<object> coords = new();
            List<List<List<object>>> result = new();
            foreach (var coordinate in json.Coordinates)
            {
                var coordinateJson = JsonConvert.DeserializeObject<List<List<List<object>>>>(coordinate.ToString()!);
                foreach (var _ in coordinateJson!)
                {
                    for (int i = 0; i < _.Count; i += geoPolygon.DotPolygon - 1)
                        coords.Add(_[i]);
                        
                    result.Add(new List<List<object>> { coords });

                    coords = new List<object>();
                }
            }

            results = result;
        }
        
        return Task.FromResult(JsonConvert.SerializeObject(results));
    }
}