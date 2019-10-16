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
        public PutAssetRequest(string assetTag, AssetPutRequest putRequest)
        {
            AssetTag = assetTag;
            PutRequest = putRequest;
        }

        public string AssetTag { get; }
        public AssetPutRequest PutRequest { get; }
    }
}
