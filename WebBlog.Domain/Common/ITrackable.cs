using System.Text.Json.Serialization;

namespace WebBog.Domain.Common
{
    public interface ITrackable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
