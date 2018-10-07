using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using CatProjService.DataObjects;
using CatProjService.Models;

namespace CatProjService.Controllers
{
    public class EleItemController : TableController<EleItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CatProjContext context = new CatProjContext();
            DomainManager = new EntityDomainManager<EleItem>(context, Request);
        }

        // GET tables/EleItem
        public IQueryable<EleItem> GetAllEleItems()
        {
            return Query();
        }

        /* // get all EleItem where CatId == parameter
        // GET tables/EleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IQueryable<EleItem> GetSubsetEleItems(string id)
        {
            return Query().Where<EleItem>(a => a.CatId == id);
        }
        */

        // GET tables/EleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EleItem> GetEleItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/EleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<EleItem> PatchEleItem(string id, Delta<EleItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/EleItem
        public async Task<IHttpActionResult> PostEleItem(EleItem item)
        {
            EleItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/EleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEleItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}