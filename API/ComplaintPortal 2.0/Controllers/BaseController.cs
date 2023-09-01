using ComplaintPortalEntities;
using ComplaintPortalServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComplaintPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseController<T> : ControllerBase where T : BaseEntity, new()
    {
        protected BaseService<T> service;
        public BaseController(BaseService<T> baseService)
        {
            service = baseService;
        }


        #region Default Routes
        [HttpGet]
        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await service.GetAll();
            return result;
        }

        [HttpPost]
        public virtual async Task<T> AddOrUpdate(T model)
        {      
            var result = await service.Insert(model);
            return result;
        }

        [HttpPost]
        public virtual async Task<IEnumerable<T>> AddMany(List<T> model)
        {
            var result = await service.InsertMany(model);
            return result;
        }

        [HttpGet]
        public async Task<T> Get(Guid id)
        {
            var result = await service.Find(id);
            return result;
        }

        [HttpPost]
        public async Task<T> Delete(Guid id)
        {
            var data = await service.Find(id);
            await service.Delete(id);
            return data;
        }
        #endregion
    }
}
