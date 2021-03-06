﻿using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace CatProj
{
    public class CatItem
    {
        string id;
        string name;
        string os;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "os")]
        public string OS
        {
            get { return os; }
            set { os = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
