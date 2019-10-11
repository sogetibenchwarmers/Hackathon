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
    public class GetGroupsHandler : IRequestHandler<GetGroupsRequest, SupportGroupsList>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetGroupsHandler(IOptions<ServiceNowOptions> options, IMapper mapper, IMemoryCache cache)
        {
            _options = options.Value;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<SupportGroupsList> Handle(GetGroupsRequest request, CancellationToken cancellationToken)
        {
            const string groupEndpoint = "sys_user_group";

            if (!_cache.TryGetValue(groupEndpoint, out IEnumerable<SupportGroup> groups))
            {
                using (var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) })
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.BuildAuthHeader());
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var response = await client.GetAsync(groupEndpoint, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var snGroups = (await response.Content.ReadAsAsync<ServiceNowSupportGroupsResult>()).Result;
                        if (snGroups != null && snGroups.Any())
                        {
                            var groupList = new List<SupportGroup>();
                            foreach (var snGroup in snGroups)
                            {
                                groupList.Add(_mapper.Map<SupportGroup>(snGroup));
                            }
                            groups = groupList;
                        }
                        _cache.Set(groupEndpoint, groups);
                    }
                }
            }

            return new SupportGroupsList { Data = groups };
        }
    }
}
