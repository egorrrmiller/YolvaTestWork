using YolvaTestWork.Models;

namespace YolvaTestWork.GeoServices;

public interface IGeoService
{
    Task<bool> CanExecute(GeoPolygonModel geoPolygonModel);
    Task<string> Execute(GeoPolygonModel geoPolygonModel);
}