using Application.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostLogic postLogic;
        public PostController(IPostLogic postLogic)
        {
            this.postLogic = postLogic;
        }
        [HttpPost]
        public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDto dto)
        {
            try
            {
                Post created = await postLogic.CreateAsync(dto);
                return Created($"/post/{created.Id}", created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
     [FromQuery] string? titleContains)
        {
            try
            {
                SearchPostParametersDto parameters = new(userName, userId, titleContains);
                var posts = await postLogic.GetAsync(parameters);
                return Ok(posts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
        {
            try
            {
                await postLogic.UpdateAsync(dto);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await postLogic.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostBasicDto>> GetById([FromRoute] int id)
        {
            try
            {
                PostBasicDto result = await postLogic.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
