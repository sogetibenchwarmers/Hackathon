using HackathonApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class PutAssetRequest : IRequest<Asset>
    {
        public PutAssetRequest(AssetPutRequest putRequest)
        {
            PutRequest = putRequest;
        }

        public AssetPutRequest PutRequest { get; }
    }
}
