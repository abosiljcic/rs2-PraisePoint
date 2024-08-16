﻿using MediatR;

namespace Reward.API.Features.Commands.NewPoints
{
    public class NewPointsCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public int CompanyBudget { get; set; }
    }
}