﻿using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface IAssignmentService
{
    Task<AssignmentDTO?> GetAssignmentByIdAsync(int id);
}
