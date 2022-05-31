using Newtonsoft.Json;

namespace YolvaTestWork.GeoServices.OSM.Models;

public class GeoJson
{
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("coordinates")] public List<object> Coordinates { get; set; }
}

public class OpenStreetMapJsonModel
{
    [JsonProperty("place_id")] public int PlaceId { get; set; }

    [JsonProperty("licence")] public string Licence { get; set; }

    [JsonProperty("osm_type")] public string OsmType { get; set; }

    [JsonProperty("osm_id")] public long OsmId { get; set; }

    [JsonProperty("boundingbox")] public List<string> Boundingbox { get; set; }

    [JsonProperty("lat")] public string Lat { get; set; }

    [JsonProperty("lon")] public string Lon { get; set; }

    [JsonProperty("display_name")] public string DisplayName { get; set; }

    [JsonProperty("class")] public string Class { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("importance")] public double Importance { get; set; }

    [JsonProperty("icon")] public string Icon { get; set; }

    [JsonProperty("geojson")] public GeoJson GeoJson { get; set; }
}