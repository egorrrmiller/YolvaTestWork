using YolvaTestWork.GeoServices.OSM.Enums;
using YolvaTestWork.GeoServices.OSM.Models;
using YolvaTestWork.Models;

namespace YolvaTestWork.GeoServices.OSM.TypePolygon;

public interface IPolygon
{
    public Task<bool> CanExecute(OsmTypePolygonEnum osmTypePolygonEnum);

    public Task<string> Execute(GeoJson? geoJson, GeoPolygonModel geoPolygonModel);
}