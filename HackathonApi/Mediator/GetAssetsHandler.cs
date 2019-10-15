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
    public class GetAssetsHandler : IRequestHandler<GetAssetsRequest, AssetList>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMapper _mapper;

        public GetAssetsHandler(IOptions<ServiceNowOptions> options, IMapper mapper)
        {
            _options = options.Value;
            _mapper = mapper;
        }

        public async Task<AssetList> Handle(GetAssetsRequest request, CancellationToken cancellationToken)
        {
            const string assetsEndpoint = "alm_asset";

            var result = new AssetList();
            var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) };
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.BuildAuthHeader());
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.GetAsync(assetsEndpoint, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return result;
            }
            var snAssets = await response.Content.ReadAsAsync<ServiceNowAssets>();
            if (snAssets != null && snAssets.Result.Any())
            {
                var assets = new List<Asset>();
                foreach (var snAsset in snAssets.Result)
                {
                    assets.Add(_mapper.Map<Asset>(snAsset));
                }
                result.Data = assets;
            }
            return result;
        }
    }
}
