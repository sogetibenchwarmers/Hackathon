﻿using System;
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
    private readonly IMediator _mediator;
    static AssetList _assetList;

    public AssetsController(IMediator mediator)
    {
        _mediator = mediator;
        _assetList = new AssetList();
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
        return Ok(await _mediator.Send(new PutAssetRequest(assetTag, putRequest)));
    }
}
