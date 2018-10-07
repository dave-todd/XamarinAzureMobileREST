using Microsoft.Azure.Mobile.Server;

namespace CatProjService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }
        public bool Complete { get; set; }
        public string OS { get; set; }
    }
}