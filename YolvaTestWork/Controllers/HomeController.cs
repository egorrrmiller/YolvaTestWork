using Microsoft.AspNetCore.Mvc;
using YolvaTestWork.Enums;
using YolvaTestWork.Models;
using YolvaTestWork.Service;

namespace YolvaTestWork.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    private readonly IEnumerable<IGeoPolygon> _geoPolygons;


    public HomeController(IEnumerable<IGeoPolygon> geoPolygons)
    {
        _geoPolygons = geoPolygons; }

    [HttpGet]
    [Route("get/polygon")]
    /*public async Task<IActionResult> GetPolygon([FromBody] GeoPolygon geoPolygon)*/
    public async Task<IActionResult> GetPolygon(string address, int dotPolygon, string fileName, GeoServicesEnum geoService)
    {
        var geoPolygon = new GeoPolygon()
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