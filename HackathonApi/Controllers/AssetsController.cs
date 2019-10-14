using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackathonApi.Mediator;
using HackathonApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class AssetsController : ControllerBase
{
    static AssetList assetList;
    private readonly IMediator _mediator;

    static AssetsController()
    {
        assetList = new AssetList();
        //assetList.Data = new List<Asset>
        //{
        //    new Asset
        //    {
        //        Id = Guid.NewGuid(),
        //        AssetTag = "P0001",
        //        Name = "Dell Latitude E7450",
        //        OwnedBy = "Jane Doe",
        //        Location = new Location
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "New York Office",
        //            Street = "1535 West 32nd Street",
        //            City = "New York",
        //            State = "NY",
        //            Zip = "10101"
        //        },
        //        Status = "Active",
        //        SupportGroup = "IT",
        //        AssignmentGroup = "Marketing"
        //    },
        //    new Asset
        //    {
        //        Id = Guid.NewGuid(),
        //        AssetTag = "P0002",
        //        Name = "Dell 32 inch monitor",
        //        OwnedBy = "Jane Doe",
        //        Location = new Location
        //        {
        //            Name = "New York Office",
        //            Street = "1535 West 32nd Street",
        //            City = "New York",
        //            State = "NY",
        //            Zip = "10101"
        //        },
        //        Status = "Active",
        //        SupportGroup = "IT",
        //        AssignmentGroup = "Marketing"
        //    },
        //    new Asset
        //    {
        //        Id = Guid.NewGuid(),
        //        AssetTag = "P0003",
        //        Name = "Logitech Keyboard",
        //        OwnedBy = "Jane Doe",
        //        Location = new Location
        //        {
        //            Name = "New York Office",
        //            Street = "1535 West 32nd Street",
        //            City = "New York",
        //            State = "NY",
        //            Zip = "10101"
        //        },
        //        Status = "Active",
        //        SupportGroup = "IT",
        //        AssignmentGroup = "Marketing"
        //    },
        //    new Asset
        //    {
        //        Id = Guid.NewGuid(),
        //        AssetTag = "P0001",
        //        Name = "Logitech Wireless Mouse",
        //        OwnedBy = "Jane Doe",
        //        Location = new Location
        //        {
        //            Name = "New York Office",
        //            Street = "1535 West 32nd Street",
        //            City = "New York",
        //            State = "NY",
        //            Zip = "10101"
        //        },
        //        Status = "Active",
        //        SupportGroup = "IT",
        //        AssignmentGroup = "Marketing"
        //    }
        //};

    }

    public AssetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a list of Assets
    /// </summary>
    /// <returns><see cref="AssetList"/></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(AssetList))]
    [ProducesResponseType(404, Type = typeof(void))]
    public async Task<IActionResult> GetAssets()
    {
        var assets = await _mediator.Send(new GetAssetsRequest());
        if (assets.Data == null || !assets.Data.Any())
        {
            return NotFound($"Assets not found.");
        }
        return Ok(assets);
    }

    /// <summary>
    /// Gets a specific Asset
    /// </summary>
    /// <param name="assetTag"></param>
    /// <returns><see cref="Asset"/></returns>
    [HttpGet("{assetTag}")]
    [ProducesResponseType(200, Type = typeof(Asset))]
    [ProducesResponseType(404, Type = typeof(void))]
    public async Task<IActionResult> GetAsset([FromRoute] string assetTag)
    {
        //var asset = assetList.Data.FirstOrDefault(a => a.AssetTag == assetTag);
        var asset = await _mediator.Send(new GetAssetRequest(assetTag));
        if (asset == null)
        {
            return NotFound($"Asset with asset tag {assetTag} not found.");
        }

        return Ok(asset);
    }

    /// <summary>
    /// Updates an Asset
    /// </summary>
    /// <param name="assetTag"></param>
    /// <param name="putRequest"></param>
    /// <returns><see cref="Asset"/></returns>
    [HttpPut("{assetTag}")]
    [ProducesResponseType(200, Type = typeof(Asset))]
    [ProducesResponseType(404, Type = typeof(void))]
    public async Task<IActionResult> UpdateAsset([FromRoute] string assetTag, [FromBody] AssetPutRequest putRequest)
    {
        var asset = assetList.Data.FirstOrDefault(a => a.AssetTag == assetTag);
        if (asset == null)
        {
            return NotFound($"Asset with asset tag {assetTag} not found.");
        }
        asset.Name = putRequest.Name;
        asset.Status = putRequest.Status;
        asset.SupportGroup.Id = putRequest.SupportGroupId;
        asset.AssignmentGroup = putRequest.AssignmentGroup;
        asset.Location.Id = putRequest.LocationId;
        asset.SubLocation.Id = putRequest.LocationId;
        asset.Status = putRequest.Status;

        return Ok(asset);
    }
}
