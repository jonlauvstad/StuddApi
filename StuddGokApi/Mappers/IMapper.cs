﻿namespace StuddGokApi.Mappers;

public interface IMapper<TModel, TDto>
{
    TDto MapToDTO(TModel model);
    TModel MapToModel(TDto dto);
}
