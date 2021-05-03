using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Web.Handlers
{
    public class OrderHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateOrderAsync(OrderCreateDto orderDto)
        {
            var city = await unitOfWork.GetRepository<City>().GetByIdAsync(orderDto.City.Id);
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(orderDto.User.Id);
            var order = MapperHelper.MapRecursiveToNewOrder(orderDto, user, city);

            try
            {
                await unitOfWork.GetRepository<Order>().AddAsync(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderReadDto> GetOrderAsync(int id, IEnumerable<Claim> userClaims)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null)
                throw new Exception();

            var userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));
            if (order.User.Id != userId && order.WorkOffers.FirstOrDefault()?.Worker.Id != userId)
                throw new UnauthorizedAccessException();

            return mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderRootReadDto>> GetOrdersAsync(IEnumerable<Claim> userClaims)
        {
            var role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            IEnumerable<Order> orders = role.Value switch
            {
                "Customer" => await GetCustomerOrdersAsync(userId),
                "Builder" => await GetAllUnstartedOrdersAsync(),
                _ => throw new Exception("Unsupported user role.")
            };

            return mapper.Map<List<OrderRootReadDto>>(orders);
        }

        private async Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId)
        {
            return await unitOfWork.GetRepository<Order>().GetAsync(o => o.User.Id == customerId && o.IsRoot == true);
        }

        private async Task<IEnumerable<Order>> GetAllUnstartedOrdersAsync()
        {
            return await unitOfWork.GetRepository<Order>().GetAsync(o => o.IsRoot && !o.IsStarted);
        }

        public async Task<IEnumerable<OrderRootReadDto>> GetWorksByCityIdAsync(int id)
        {
            var orders = await unitOfWork.GetRepository<Order>().GetAsync(o => o.IsRoot && !o.IsStarted && o.City.Id == id);
            return mapper.Map<List<OrderRootReadDto>>(orders);
        }

        public async Task<IEnumerable<OrderRootReadDto>> GetWorksAsync()
        {
            var orders = await unitOfWork.GetRepository<Order>().GetAsync(o => o.IsRoot && !o.IsStarted);
            return mapper.Map<List<OrderRootReadDto>>(orders);
        }

        public async Task<WorkReadDto> GetWorkByIdAsync(int id)
        {
            var work = await unitOfWork.GetRepository<Order>().GetFirstOrDefaultAsync(o => o.Id == id);
            return mapper.Map<WorkReadDto>(work);
        }

        public async Task AddOfferAsync(int id, WorkOfferCreateDto offerDto)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null || order.IsStarted)
                throw new Exception();

            var offer = mapper.Map<WorkOffer>(offerDto);
            offer.Worker = await unitOfWork.GetRepository<User>().GetByIdAsync(offer.Worker.Id);
            offer.Date = DateTime.Now;
            order.WorkOffers.Add(offer);

            try
            {
                unitOfWork.GetRepository<Order>().Update(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ConfirmOfferAsync(int id, WorkOfferReadDto offerDto, IEnumerable<Claim> userClaims)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);

            if (order.User.Id != int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value))
                throw new UnauthorizedAccessException();

            order.WorkOffers.RemoveAll(o => o.Id != offerDto.Id);
            order.IsStarted = true;
            if (order.SubOrders.Count > 0)
                order.SubOrders.FirstOrDefault().IsStarted = true;

            try
            {
                unitOfWork.GetRepository<Order>().Update(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderRootReadDto>> GetConfirmedWorksForUserAsync(IEnumerable<Claim> userClaims)
        {
            var userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var works = await unitOfWork.GetRepository<Order>().GetAsync(ord => ord.WorkOffers.FirstOrDefault().Worker.Id == userId && ord.IsStarted);
            return mapper.Map<List<OrderRootReadDto>>(works);
        }

        public async Task<OrderReadDto> ConfirmOrderAsync(int id, OrderReadDto orderDto, IEnumerable<Claim> userClaims)
        {
            var rootOrder = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(orderDto.Id);
            var userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (order.User.Id != userId)
                throw new UnauthorizedAccessException();

            order.IsCompleted = true;

            Order nextOrder = null;
            foreach (var o in rootOrder.SubOrders)
            {
                if (nextOrder != null)
                    break;

                if (!o.IsStarted)
                {
                    nextOrder = o;
                    break;
                }

                foreach (var so in o.SubOrders)
                {
                    if (!so.IsStarted)
                    {
                        nextOrder = so;
                        break;
                    }
                }
            }

            if (nextOrder != null)
                nextOrder.IsStarted = true;
            else
                rootOrder.IsCompleted = true;

            try
            {
                unitOfWork.GetRepository<Order>().Update(rootOrder);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return mapper.Map<OrderReadDto>(rootOrder);
        }

        public async Task DeleteRootOrderAsync(int id, IEnumerable<Claim> userClaims)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (order.User.Id != userId || order.IsStarted)
                throw new UnauthorizedAccessException();

            try
            {
                var repo = unitOfWork.GetRepository<Order>();

                foreach (var o in order.SubOrders)
                {
                    foreach (var so in o.SubOrders)
                        await repo.DeleteByIdAsync(so.Id);

                    await repo.DeleteByIdAsync(o.Id);
                }
                await unitOfWork.GetRepository<Order>().DeleteByIdAsync(id);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}