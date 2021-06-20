﻿using eShopSolution.ViewModels.Catalog.Products.Dtos;
using eShopSolution.ViewModels.Common;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<PagedResult<ProductViewModel>> ListAllPaging(GetManageProductPagingRequest request);
        Task<bool> UpdatePrice(int productId , decimal newPrice);
        Task<bool> UpdateStock(int productId , int addedQuantity);
        Task AddViewCount(int productId);
    }
}
