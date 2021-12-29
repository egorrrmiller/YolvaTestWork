using System.Text;
using Microsoft.AspNetCore.Mvc;
using YolvaTestWork.Enums;
using YolvaTestWork.GeoServices;
using YolvaTestWork.Models;

namespace YolvaTestWork.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    private readonly IEnumerable<IGeoService> _geoPolygons;


    public HomeController(IEnumerable<IGeoService> geoPolygons)
    {
        _geoPolygons = geoPolygons; }

    [HttpGet]
    [Route("get/polygon")]
    public async Task<IActionResult> GetPolygon([FromQuery] GeoPolygonModel geoPolygon)
    {
        var result = await _geoPolygons.FirstOrDefault(ex => ex.CanExecute(geoPolygon).Result)?.Execute(geoPolygon)!;

        return File(Encoding.UTF8.GetBytes(result), "application/json", $"{geoPolygon.FileName}.json");
    }
}