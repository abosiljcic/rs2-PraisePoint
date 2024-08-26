﻿using MediatR;
using Ordering.Application.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Queries.ViewModels;

namespace Ordering.Application.Features.Orders.Queries.GetListOfOrders
{
    public class GetListOfOrdersQueryHandler : IRequestHandler<GetListOfOrdersQuery, List<OrderViewModel>>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderViewModelFactory _factory;

        public GetListOfOrdersQueryHandler(IOrderRepository repository, IOrderViewModelFactory factory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<List<OrderViewModel>> Handle(GetListOfOrdersQuery request, CancellationToken _)
        {
            var orderList = await _repository.GetOrdersByUsername(request.Username);
            return orderList.Select(order => _factory.CreateViewModel(order)).ToList();
        }
    }
}
