using HackathonApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class GetUsersRequest : IRequest<UserList>
    {
    }
}
