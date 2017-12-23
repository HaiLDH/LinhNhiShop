using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Data.Repositories;
using LinhNhiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNhiShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetails);
    }


    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
        }

        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
