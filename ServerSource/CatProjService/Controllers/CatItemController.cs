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
    public class CatItemController : TableController<CatItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CatProjContext context = new CatProjContext();
            DomainManager = new EntityDomainManager<CatItem>(context, Request);
        }

        // GET tables/CatItem
        public IQueryable<CatItem> GetAllCatItems()
        {
            return Query();
        }

        // GET tables/CatItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<CatItem> GetCatItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/CatItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<CatItem> PatchCatItem(string id, Delta<CatItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/CatItem
        public async Task<IHttpActionResult> PostCatItem(CatItem item)
        {
            CatItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/CatItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCatItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}