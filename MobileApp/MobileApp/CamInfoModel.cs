using Newtonsoft.Json;

namespace MobileApp
{
    public class CamInfoModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("IsSoundOn")]
        public bool IsSoundOn { get; set; }
        [JsonProperty("AttachedToServer")]
        public string AttachedToServer { get; set; }
    }
}
