using ConstructionQualityControl.Data.Models;
using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public static class MapperHelper
    {
        public static Order MapRecursiveToNewOrder(OrderCreateDto dto, User user, City city)
        {
            var order = new Order();
            order.CreationDate = DateTime.Now;
            order.IsRoot = dto.IsRoot;
            order.IsStarted = false;
            order.User = user;
            order.City = city;
            order.PrePaid = dto.PrePaid;
            order.PostPaid = dto.PostPaid;
            order.Demands = dto.Demands;

            if (dto.SubOrders?.Count > 0)
            {
                foreach (var subOrder in dto.SubOrders)
                {
                    order.SubOrders.Add(MapRecursiveToNewOrder(subOrder, user, city));
                }
            }

            return order;
        }
    }
}
