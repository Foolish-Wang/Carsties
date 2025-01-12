using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController] // 指定该类是一个 API 控制器，自动启用模型验证和路由特性
[Route("api/auctions")] // 指定该控制器的基础路由路径为 "api/auctions"
public class AuctionsController : ControllerBase
{
    private readonly AuctionDbContext _context; // 数据库上下文，用于与数据库交互
    private readonly IMapper _mapper; // AutoMapper，用于对象映射

    // 通过依赖注入获取数据库上下文和 AutoMapper 实例
    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet] // HTTP GET 方法，用于获取所有拍卖信息
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        // 从数据库中获取所有拍卖，并按物品的 "Make" 属性排序
        var auctions = await _context.Auctions
            .Include(x => x.Item) // 包括关联的 Item 数据
            .OrderBy(x => x.Item.Make) // 按 "Make" 排序
            .ToListAsync();

        // 使用 AutoMapper 将拍卖实体列表映射为拍卖 DTO 列表并返回
        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")] // HTTP GET 方法，用于根据拍卖 ID 获取特定的拍卖信息
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        // 根据 ID 从数据库获取拍卖信息，包括关联的 Item 数据
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        // 如果找不到拍卖，则返回 404 NotFound 响应
        if (auction == null) return NotFound();

        // 使用 AutoMapper 将拍卖实体映射为拍卖 DTO 并返回
        return _mapper.Map<AuctionDto>(auction);
    }

    [HttpPost] // HTTP POST 方法，用于创建新拍卖
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        // 使用 AutoMapper 将 DTO 映射为实体对象
        var auction = _mapper.Map<Auction>(auctionDto);

        // TODO: 将当前用户作为卖家（硬编码为 "test"，需改为动态获取当前用户）
        auction.Seller = "test";

        // 将新拍卖添加到数据库上下文
        _context.Auctions.Add(auction);

        // 保存更改并检查是否成功
        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not save changes to the DB");

        // 返回 201 Created 响应，并包含新创建的拍卖信息
        return CreatedAtAction(nameof(GetAuctionById), 
            new { auction.Id }, _mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")] // HTTP PUT 方法，用于更新拍卖信息
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        // 根据 ID 获取拍卖信息，包括关联的 Item 数据
        var auction = await _context.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        // 如果找不到拍卖，则返回 404 NotFound 响应
        if (auction == null) return NotFound();

        // TODO: 检查卖家是否为当前用户（未实现）

        // 更新拍卖的属性（如果 DTO 中的属性不为空，则覆盖原有值）
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

        // 保存更改并检查是否成功
        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok(); // 返回 200 OK 响应

        return BadRequest("Problem saving changes"); // 返回 400 BadRequest 响应
    }

    [HttpDelete("{id}")] // HTTP DELETE 方法，用于删除拍卖
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        // 根据 ID 获取拍卖信息
        var auction = await _context.Auctions.FindAsync(id);

        // 如果找不到拍卖，则返回 404 NotFound 响应
        if (auction == null) return NotFound();

        // TODO: 检查卖家是否为当前用户（未实现）

        // 从数据库上下文中删除拍卖
        _context.Auctions.Remove(auction);

        // 保存更改并检查是否成功
        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not update DB"); // 返回 400 BadRequest 响应

        return Ok(); // 返回 200 OK 响应
    }
}
