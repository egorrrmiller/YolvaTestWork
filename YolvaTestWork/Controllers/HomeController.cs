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
    public async Task<IActionResult> GetPolygon(string address, int dotPolygon, string fileName, GeoServicesEnum geoService)
    {
        var geoPolygon = new GeoPolygonModel()
        {
            Address = address,
            DotPolygon = dotPolygon,
            FileName = fileName,
            GeoService = geoService
        };
        
        var result = await _geoPolygons.FirstOrDefault(ex => ex.CanExecute(geoPolygon).Result)?.Execute(geoPolygon)!;

        await System.IO.File.WriteAllTextAsync($"{geoPolygon.FileName}.json", result);
        Response.Headers.Add("Content-Disposition", $"attachment; filename={geoPolygon.FileName}.json");
        return new FileContentResult(await System.IO.File.ReadAllBytesAsync($"{geoPolygon.FileName}.json"), "application/json");
    }
}