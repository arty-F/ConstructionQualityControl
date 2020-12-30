using ConstructionQualityControl.Data.Models;
using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public static class MapperHelper
    {
        public static Order MapRecursiveToNewOrder(OrderCreateDto dto, User user)
        {
            var order = new Order();
            order.CreationDate = DateTime.Now;
            order.IsRoot = dto.IsRoot;
            order.User = user;
            order.IsActive = true;
            order.Demands = dto.Demands;

            if (dto.SubOrders?.Count > 0)
            {
                foreach (var subOrder in dto.SubOrders)
                {
                    order.SubOrders.Add(MapRecursiveToNewOrder(subOrder, user));
                }
            }

            return order;
        }
    }
}
