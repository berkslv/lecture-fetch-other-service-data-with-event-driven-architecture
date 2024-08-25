using Organizations.Domain.Contracts.Product;
using Refit;

namespace Organizations.Application.Interfaces;

public interface IProductService
{
    Task<IApiResponse<GetProductByIdQueryResponse>> GetProductById(Guid id);
}