using HackathonApi.Models;
using MediatR;

namespace HackathonApi.Mediator
{
    public class GetAssetRequest : IRequest<Asset>
    {
        public GetAssetRequest(string assetTag)
        {
            AssetTag = assetTag;
        }

        public string AssetTag { get; }
    }
}
