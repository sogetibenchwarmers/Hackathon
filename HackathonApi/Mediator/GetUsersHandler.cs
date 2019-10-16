using AutoMapper;
using HackathonApi.Models;
using HackathonApi.Models.Options;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackathonApi.Mediator
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, UserList>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetUsersHandler(IOptions<ServiceNowOptions> options, IMapper mapper, IMemoryCache cache)
        {
            _options = options.Value;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<UserList> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            const string groupEndpoint = "sys_user";

            if (!_cache.TryGetValue(groupEndpoint, out IEnumerable<User> users))
            {
                var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) };
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.BuildAuthHeader());
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(groupEndpoint, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var listResult = (await response.Content.ReadAsAsync<ServiceNowListResult<ServiceNowUser>>()).Result;
                    if (listResult != null && listResult.Any())
                    {
                        var userList = new List<User>();
                        foreach (var snUser in listResult)
                        {
                            userList.Add(_mapper.Map<User>(snUser));
                        }
                        users = userList;
                    }
                    _cache.Set(groupEndpoint, users);
                }
            }

            return new UserList { Data = users };
        }
    }
}
