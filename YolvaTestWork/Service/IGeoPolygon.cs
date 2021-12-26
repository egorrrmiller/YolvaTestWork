using YolvaTestWork.Models;

namespace YolvaTestWork.Service;

public interface IGeoPolygon
{
    Task<bool> CanExecute(GeoPolygon geoPolygon);
    Task<string> Execute(GeoPolygon geoPolygon);
}