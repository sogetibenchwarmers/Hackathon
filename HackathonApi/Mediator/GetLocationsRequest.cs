using HackathonApi.Models;
using MediatR;

namespace HackathonApi.Mediator
{
    public class GetLocationsRequest : IRequest<LocationList>
    {
    }
}
