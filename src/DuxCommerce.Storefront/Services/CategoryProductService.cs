﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuxCommerce.StoreBuilder.Catalog.UseCases;
using OrchardCore.ContentManagement;
using OrchardCore.Navigation;

namespace DuxCommerce.Storefront.Services;

public class CategoryProductService(ProductService productService, CategoryUseCases categoryUseCases)
{
    public async Task<(int, List<ContentItem>)> GetProducts(string categoryId, Pager pager)
    {
        var productIds = (await categoryUseCases.GetProductIds(categoryId)).ToList();

        var products = await productService.GetMany<ContentItem>(productIds)
            .Skip(pager.GetStartIndex())
            .Take(pager.PageSize)
            .ListAsync();

        return (productIds.Count, products.ToList());
    }
}