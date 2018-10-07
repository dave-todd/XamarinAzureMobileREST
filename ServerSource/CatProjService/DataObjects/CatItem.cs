using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace CatProjService.DataObjects
{
    public class CatItem : EntityData
    {
        public string Name { get; set; }
        public string OS { get; set; }
    }
}