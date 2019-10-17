using HackathonApi.Models;
using MediatR;

namespace HackathonApi.Mediator
{
    public class GetSubLocationsRequest : IRequest<SubLocationList>
    {
    }
}
