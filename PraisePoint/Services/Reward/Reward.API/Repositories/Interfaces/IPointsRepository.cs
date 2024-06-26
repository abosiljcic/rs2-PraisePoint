﻿using Reward.API.Entities;

namespace Reward.API.Repositories.Interfaces
{
    public interface IPointsRepository
    {
        Task<IEnumerable<Points>> GetAllPoints();

    }
}
