using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatProjService.DataObjects
{
    public class EleItem : EntityData
    {
        public string CatId { get; set; }
        public string Name { get; set; }
        public string OS { get; set; }
    }
}