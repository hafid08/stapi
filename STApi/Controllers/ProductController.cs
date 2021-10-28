using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STApi.Commands;
using STApi.Data;
using STApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace STApi.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger,IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {

                    return Ok(new ResponseFailed("File not selected."));
                }
                string uniqueFileName = $"{ Guid.NewGuid()}-{file.FileName}";
                Response data = new Response();
                string pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                string pathFull = Path.Combine(pathFolder, uniqueFileName);
                using (FileStream stream = new FileStream(pathFull, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new ResponseSuccess(pathFull));
            }
            catch (Exception ex)
            {
                return Ok(new ResponseFailed(ex.Message));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                GetProduct request = new GetProduct();
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseFailed(ex.Message));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                GetProduct request = new GetProduct(id);
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseFailed(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                DeleteProduct request = new DeleteProduct(id);
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseFailed(ex.Message));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Product body)
        {
            try
            {
                SaveProduct request = new SaveProduct(body);
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseFailed(ex.Message));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Product body)
        {
            try
            {
                SaveProduct request = new SaveProduct(body);
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseFailed(ex.Message));
            }
        }
    }
}
