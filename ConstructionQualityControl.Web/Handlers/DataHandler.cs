using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Web.Handlers
{
    public class DataHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DataHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CommentReadDto> AddCommentAsync(int id, CommentCreateDto commentDto)
        {
            if (commentDto.Text.Length == 0)
                throw new Exception();

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
                throw;
            }

            return mapper.Map<CommentReadDto>(comment);
        }

        public async Task<IEnumerable<ReportReadDto>> AddReportsAsync(int id, ReportCreateDto[] reportsDto)
        {
            if (reportsDto.Length == 0)
                throw new Exception();

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
            catch (Exception)
            {
                throw;
            }

            return mapper.Map<List<ReportReadDto>>(reports);
        }
    }
}
