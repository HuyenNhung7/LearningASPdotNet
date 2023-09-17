using System.Text.Json.Serialization;

namespace LearningASPdotNet.Models
{
    // trên swagger hiện kết quả kiển {1, 2, 3}
    // Có dòng này sẽ hiện có cả tên knight, ...
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }
}