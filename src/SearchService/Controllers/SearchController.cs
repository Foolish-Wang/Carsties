using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;

[ApiController] // 指定该类是一个 API 控制器，提供自动路由和验证功能
[Route("api/search")] // 设置该控制器的基础路由路径为 "api/search"
public class SearchController : ControllerBase
{
    [HttpGet] // 定义一个 HTTP GET 方法，用于搜索物品
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        // 创建一个分页搜索查询对象
        var query = DB.PagedSearch<Item, Item>();

        // 设置默认排序规则为按 "Make" 属性升序排列
        query.Sort(x => x.Ascending(a => a.Make));

        // 如果搜索参数中包含搜索词，则进行全文匹配，并根据文本相关度排序
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        // 根据搜索参数中的 "OrderBy" 字段设置排序规则
        query = searchParams.OrderBy switch
        {
            "make" => query.Sort(x => x.Ascending(a => a.Make)), // 按 "Make" 属性升序排列
            "new"  => query.Sort(x => x.Descending(a => a.CreatedAt)), // 按 "CreatedAt" 属性降序排列
            _      => query.Sort(x => x.Ascending(a => a.AuctionEnd)) // 默认按 "AuctionEnd" 属性升序排列
        };
        
        // 根据搜索参数中的 "FilterBy" 字段设置过滤条件
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow), // 筛选拍卖已结束的物品
            "endingSoon" => query.Match(x => x.AuctionEnd    < DateTime.UtcNow.AddHours(6) // 筛选即将结束的物品
                                             && x.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow) // 默认筛选尚未结束的物品
        };

        // 如果指定了卖家，添加卖家过滤条件
        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        // 如果指定了获胜者，添加获胜者过滤条件
        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        // 设置分页参数
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        // 执行查询并获取结果
        var result = await query.ExecuteAsync();

        // 返回查询结果，包括结果列表、总页数和总条目数
        return Ok(new
        {
            results    = result.Results, // 查询结果列表
            pageCount  = result.PageCount, // 总页数
            totalCount = result.TotalCount // 总条目数
        });
    }
}
