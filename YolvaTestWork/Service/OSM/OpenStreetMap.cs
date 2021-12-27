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

        List<List<object>> polygons = new();
        foreach (var _ in json!.Coordinates)
        {
            var coord = JsonConvert.DeserializeObject<List<List<object>>>(_.ToString()!);

            for (int i = 0; i < coord.Count; i += geoPolygon.DotPolygon - 1)
                polygons.Add(new List<object>() { coord[i] });
        }
        return Task.FromResult(JsonConvert.SerializeObject(polygons));
    }
}