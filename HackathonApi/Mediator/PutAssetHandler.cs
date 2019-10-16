using AutoMapper;
using HackathonApi.Models;
using HackathonApi.Models.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class PutAssetHandler : IRequestHandler<PutAssetRequest, Asset>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PutAssetHandler(IOptions<ServiceNowOptions> options, IMapper mapper, IMediator mediator)
        {
            _options = options.Value;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Asset> Handle(PutAssetRequest request, CancellationToken cancellationToken)
        {
            var assetsEndpoint = $"alm_asset/{request.PutRequest.Id.ToUuid()}";

            var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) };

            var patchRequest = _mapper.Map<ServiceNowAssetPatchRequest>(request.PutRequest);
            var content = JsonConvert.SerializeObject(patchRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, assetsEndpoint)
            {
                Content = byteContent
            };
            httpRequest.Headers.Add("Authorization", "Basic " + _options.BuildAuthHeader());
            httpRequest.Headers.Add("Accept", "application/json");
            httpRequest.Content.Headers.Add("Content-Type", "application/json");

            var response = await client.SendAsync(httpRequest, cancellationToken);
            var result = await response.Content.ReadAsAsync<ServiceNowAssetResult>();
            if (result == null)
            {
                return null;
            }

            return await _mediator.Send(new TransformResponseToAssetRequest(client, result.Result));
        }
    }
}
