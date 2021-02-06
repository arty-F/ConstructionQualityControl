using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DataController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("Comment/{id}")]
        public async Task<ActionResult<CommentReadDto>> AddComment(int id, CommentCreateDto commentDto)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);

            var comment = mapper.Map<Comment>(commentDto);
            comment.Date = DateTime.Now;
            comment.User = await unitOfWork.GetRepository<User>().GetByIdAsync(commentDto.User.Id);
            order.Comments.Add(comment);

            try
            {
                unitOfWork.GetRepository<Order>().Update(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<CommentReadDto>(comment));
        }

        [HttpPost("Report/{id}")]
        public async Task<ActionResult<IEnumerable<ReportReadDto>>> AddReports(int id, ReportCreateDto[] reportsDto)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(reportsDto[0].User.Id);

            var reports = mapper.Map<List<Report>>(reportsDto.ToList());
            foreach (var r in reports)
            {
                r.User = user;
                r.CreationDate = DateTime.Now;
            }

            order.Reports.AddRange(reports);

            try
            {
                unitOfWork.GetRepository<Order>().Update(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<List<ReportReadDto>>(reports));
        }
    }
}