using Newtonsoft.Json;
using YolvaTestWork.GeoServices.OSM.Enums;
using YolvaTestWork.GeoServices.OSM.Models;
using YolvaTestWork.Models;

namespace YolvaTestWork.GeoServices.OSM.TypePolygon;

public class OnePolygonOsm : IPolygon
{
    public async Task<bool> CanExecute(OsmTypePolygonEnum osmTypePolygonEnum)
    {
        return osmTypePolygonEnum == OsmTypePolygonEnum.Polygon;
    }

    public async Task<string> Execute(GeoJson? geoJson, GeoPolygonModel geoPolygonModel)
    {
        List<object> polygon = new();
        var coordinates =
            JsonConvert.DeserializeObject<List<List<object>>>(geoJson?.Coordinates[0].ToString()!);
        for (var i = 0; i < coordinates!.Count; i += geoPolygonModel.DotPolygon - 1)
            polygon.Add(coordinates[i]);

        return JsonConvert.SerializeObject(polygon);
    }
}