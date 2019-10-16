using AutoMapper;
using HackathonApi.Models;
using HackathonApi.Models.Options;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class GetAssetHandler : IRequestHandler<GetAssetRequest, Asset>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMediator _mediator;

        public GetAssetHandler(IOptions<ServiceNowOptions> options, IMediator mediator)
        {
            _options = options.Value;
            _mediator = mediator;
        }
        public async Task<Asset> Handle(GetAssetRequest request, CancellationToken cancellationToken)
        {
            var assetEndpoint = $"alm_asset?asset_tag={request.AssetTag}";

            var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) };
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.BuildAuthHeader());
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.GetAsync(assetEndpoint, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            var snAssets = await response.Content.ReadAsAsync<ServiceNowAssets>();
            if (snAssets == null || !snAssets.Result.Any())
            {
                return null;
            }
            var snAsset = snAssets.Result.FirstOrDefault();

            return await _mediator.Send(new TransformResponseToAssetRequest(client, snAsset));
        }
    }
}
