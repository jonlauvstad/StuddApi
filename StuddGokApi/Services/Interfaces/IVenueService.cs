﻿using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IVenueService
{
    Task<IEnumerable<VenueDTO>> GetAllVenuesAsync((DateTime from, DateTime to)? availableFromTo = null);

    Task<VenueDTO?> GetVenueByIdAsync(int id);
}
