using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private readonly DataHandler handler;

        public DataController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            handler = new DataHandler(unitOfWork, mapper);
        }

        [HttpPost("Comment/{id}")]
        public async Task<ActionResult<CommentReadDto>> AddComment(int id, CommentCreateDto commentDto)
        {
            try
            {
                return Ok(await handler.AddCommentAsync(id, commentDto));
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpPost("Report/{id}")]
        public async Task<ActionResult<IEnumerable<ReportReadDto>>> AddReports(int id, ReportCreateDto[] reportsDto)
        {
            try
            {
                return Ok(await handler.AddReportsAsync(id, reportsDto));
            }
            catch (Exception) { return BadRequest(); }
        }
    }
}