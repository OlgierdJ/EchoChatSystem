//using Echo.Domain.Shared.Entities.Enums;
//using CoreLib.Interfaces;
//using CoreLib.Interfaces.Bases;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace DomainCoreApi.Controllers.Bases
//{
//    [Authorize]
//    public class BaseEntityController<T, IdType> : ControllerBase
//        where T : class, IEntity<IdType>, new()
//    {
//        protected readonly IEntityService<T, IdType> _service;
//        protected readonly IPushNotificationService _notificationService;

//        public BaseEntityController(IEntityService<T, IdType> service, IPushNotificationService notificationService)
//        {
//            _service = service;
//            _notificationService = notificationService;
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(IdType id)
//        {
//            try
//            {
//                var result = await _service.DeleteAsync(id);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                await _notificationService.NotifyClients(result, EntityAction.Delete);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(T entity)
//        {
//            try
//            {
//                var result = await _service.AddAsync(entity);
//                if (result == null)
//                {
//                    return Problem("Something went wrong. Contact an Admin / Server representative");
//                }
//                await _notificationService.NotifyClients(result, EntityAction.Create);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }

//        [HttpPut]
//        public async Task<IActionResult> Update(T entity)
//        {
//            try
//            {
//                var result = await _service.UpdateAsync(entity);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                await _notificationService.NotifyClients(result, EntityAction.Update);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }

//        [HttpPut("collection")]
//        public async Task<IActionResult> Update(IEnumerable<T> entities)
//        {
//            try
//            {
//                var result = await _service.UpdateAsync(entities);
//                if (result == null)
//                {
//                    return Problem("Failed to update list");
//                }
//                else if (!result.Any())
//                {
//                    return NoContent();
//                }
//                await _notificationService.NotifyClients(result, EntityAction.Update);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetSingle(IdType id)
//        {
//            try
//            {
//                var result = await _service.GetSingleAsync(id);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }
//        [HttpGet("{id}/Include")]
//        public async Task<IActionResult> GetSingleWithInclude(IdType id)
//        {
//            try
//            {
//                var result = await _service.GetSingleWithIncludeAsync(id);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            try
//            {
//                var result = await _service.GetAllAsync();
//                if (result == null)
//                {
//                    return Problem("Failed to load list");
//                }
//                else if (!result.Any())
//                {
//                    return NoContent();
//                }
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }
//        [HttpGet("Include")]
//        public async Task<IActionResult> GetAllWithInclude()
//        {
//            try
//            {
//                var result = await _service.GetAllWithIncludeAsync();
//                if (result == null)
//                {
//                    return Problem("Failed to load list");
//                }
//                else if (!result.Any())
//                {
//                    return NoContent();
//                }
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return Problem(ex.Message);
//            }
//        }

//        [HttpGet("Count")]
//        public async Task<IActionResult> GetCount()
//        {
//            return Ok(await _service.GetCountAsync());
//        }
//    }
//}
