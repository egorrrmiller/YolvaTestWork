using YolvaTestWork.Enums;

namespace YolvaTestWork.Models;

public class GeoPolygon
{
    public string Address { get; set; }
    public int DotPolygon { get; set; }  
    public string FileName { get; set; }
    public GeoServicesEnum GeoService { get; set; }
}