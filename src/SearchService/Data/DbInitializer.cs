using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public class DbInitializer
{
    // 初始化数据库方法，负责数据库的配置和数据初始化
    public static async Task InitDb(WebApplication app)
    {
        // 初始化 MongoDB 数据库连接
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        // 创建索引以支持文本搜索
        await DB.Index<Item>()
            .Key(x => x.Make,  KeyType.Text) // 创建针对 "Make" 属性的文本索引
            .Key(x => x.Model, KeyType.Text) // 创建针对 "Model" 属性的文本索引
            .Key(x => x.Color, KeyType.Text) // 创建针对 "Color" 属性的文本索引
            .CreateAsync();                  // 异步执行索引创建

        // 获取数据库中现有的 Item 数据数量
        var count = await DB.CountAsync<Item>();

        // 创建一个作用域，用于解析依赖服务
        using var scope = app.Services.CreateScope();

        // 获取 AuctionSvcHttpClient 服务实例，用于调用拍卖服务 API
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

        // 从拍卖服务获取物品数据列表
        var items = await httpClient.GetItemsForSearchDb();

        // 打印从拍卖服务返回的物品数量到控制台
        Console.WriteLine(items.Count + " returned from the auction service");

        // 如果从拍卖服务返回了数据，则将其保存到数据库中
        if (items.Count > 0) await DB.SaveAsync(items);
    }
}