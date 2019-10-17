using HackathonApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class TransformResponseToAssetRequest : IRequest<Asset>
    {
        public TransformResponseToAssetRequest(HttpClient client, ServiceNowAsset snAsset)
        {
            Client = client;
            SnAsset = snAsset;
        }

        public HttpClient Client { get; }
        public ServiceNowAsset SnAsset { get; }
    }
}
