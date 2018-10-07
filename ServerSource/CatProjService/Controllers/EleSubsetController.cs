using CatProjService.DataObjects;
using CatProjService.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http.Controllers;

namespace CatProjService.Controllers
{
    public class EleSubsetController : TableController<EleItem>
    {

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CatProjContext context = new CatProjContext();
            DomainManager = new EntityDomainManager<EleItem>(context, Request);
        }

        // get all EleItem where CatId == parameter
        // GET tables/EleSubset/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IQueryable<EleItem> GetSubsetEleItems(string id)
        {
            return Query().Where<EleItem>(a => a.CatId == id);
        }
        
    }
}
