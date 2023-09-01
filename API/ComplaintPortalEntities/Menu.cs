using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public sealed class Menu : BaseEntity
    {
        [JsonPropertyName("text")]
        public string MenuName { get; set; } = null!;
        public string MenuDescription { get; set; } = null!;

        [JsonPropertyName("link")]
        public string MenuUrl { get; set; } = null!;

        [JsonPropertyName("icon")]
        public string CssClass { get; set; } = null!;
        public Permissions Permission { get; set; } = Permissions.None;

    }
}
