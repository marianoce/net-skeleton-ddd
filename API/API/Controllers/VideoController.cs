using System.Net;
using Application.Features.Videos.Commands;
using Application.Features.Videos.Commands.DeleteVideo;
using Application.Features.Videos.Commands.UpdateVideo;
using Application.Features.Videos.Querys.GetVideoListQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetVideo")]
        [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
        {
            var query = new GetVideoListQuery(username);
            var videos = await _mediator.Send(query);

            return Ok(videos); 
        }

        [HttpPost(Name = "CreateVideo")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CreateVideo([FromBody] CreateVideoCommand command)
        {
            return Ok(await _mediator.Send(command)); 
        }

        [HttpPost(Name = "UpdateVideo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateVideo([FromBody] UpdateVideoCommand command)
        {
            await _mediator.Send(command);
            return NoContent(); 
        }

        [HttpPost("{id}", Name = "DeleteVideo")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteVideo(int id)
        {
            var command = new DeleteVideoCommand() { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}