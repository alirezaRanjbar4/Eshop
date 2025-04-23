﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eshop.Share.Helpers.Resource
{
    public class ResourceKey
    {
        public string? EnumName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public List<string>? ResourceKeys { get; set; }
        public ResourceKey()
        {

        }

        public ResourceKey(string enumName, string notificationKey)
        {
            ResourceKeys = ResourceKeys ?? new List<string>();
            ResourceKeys.Add(notificationKey);
            EnumName = enumName;
        }

    }
}
