using Newtonsoft.Json;
using YolvaTestWork.GeoServices.OSM.Enums;
using YolvaTestWork.GeoServices.OSM.Models;
using YolvaTestWork.Models;

namespace YolvaTestWork.GeoServices.OSM.TypePolygon;

public class MultiPolygonOsm : IPolygon
{
    public async Task<bool> CanExecute(OsmTypePolygonEnum osmTypePolygonEnum)
    {
        return osmTypePolygonEnum == OsmTypePolygonEnum.MultiPolygon;
    }

    public async Task<string> Execute(GeoJson? geoJson, GeoPolygonModel geoPolygonModel)
    {
        List<object> polygon = new();
        List<List<List<object>>> result = new();
        foreach (var coordinate in geoJson?.Coordinates!)
        {
            var coordinateJson =
                JsonConvert.DeserializeObject<List<List<List<object>>>>(coordinate.ToString()!);
            foreach (var _ in coordinateJson!)
            {
                for (var i = 0; i < _.Count; i += geoPolygonModel.DotPolygon - 1)
                    polygon.Add(_[i]);

                result.Add(new List<List<object>> {polygon});

                polygon = new List<object>();
            }
        }

        return JsonConvert.SerializeObject(result);
    }
}