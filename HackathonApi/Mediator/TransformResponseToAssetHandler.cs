using AutoMapper;
using HackathonApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class TransformResponseToAssetHandler : IRequestHandler<TransformResponseToAssetRequest, Asset>
    {
        private readonly IMapper _mapper;

        public TransformResponseToAssetHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<Asset> Handle(TransformResponseToAssetRequest request, CancellationToken cancellationToken)
        {
            var client = request.Client;
            var snAsset = request.SnAsset;

            var asset = _mapper.Map<Asset>(snAsset);

            if (snAsset.Location != null && !string.IsNullOrWhiteSpace(snAsset.Location.Link))
            {
                var locationUri = new Uri(snAsset.Location.Link);
                var locationResponse = await client.GetAsync(locationUri, cancellationToken);
                if (locationResponse.IsSuccessStatusCode)
                {
                    var snLocation = await locationResponse.Content.ReadAsAsync<ServiceNowLocationResult>();
                    asset.Location = _mapper.Map<Location>(snLocation.Result);
                }
            }

            if (snAsset.SupportGroup != null && !string.IsNullOrWhiteSpace(snAsset.SupportGroup.Link))
            {
                var sgUri = new Uri(snAsset.SupportGroup.Link);
                var sgResponse = await client.GetAsync(sgUri, cancellationToken);
                if (sgResponse.IsSuccessStatusCode)
                {
                    var snSupportGroup = await sgResponse.Content.ReadAsAsync<ServiceNowSupportGroupResult>();
                    asset.SupportGroup = _mapper.Map<SupportGroup>(snSupportGroup.Result);
                }
            }

            if (snAsset.OwnedBy != null && !string.IsNullOrWhiteSpace(snAsset.OwnedBy.Link))
            {
                var userUri = new Uri(snAsset.OwnedBy.Link);
                var userResponse = await client.GetAsync(userUri, cancellationToken);
                if (userResponse.IsSuccessStatusCode)
                {
                    var snUser = await userResponse.Content.ReadAsAsync<ServiceNowUserResult>();
                    asset.OwnedBy = _mapper.Map<User>(snUser.Result);
                }
            }
            return asset;
        }
    }
}
