using HackathonApi.Models;
using MediatR;

namespace HackathonApi.Mediator
{
    public class GetAssetsRequest : IRequest<AssetList>
    {
    }
}
