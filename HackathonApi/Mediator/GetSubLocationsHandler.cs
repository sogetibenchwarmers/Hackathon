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
    public class GetSubLocationsHandler : IRequestHandler<GetSubLocationsRequest, SubLocationList>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetSubLocationsHandler(IOptions<ServiceNowOptions> options, IMapper mapper, IMemoryCache cache)
        {
            _options = options.Value;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<SubLocationList> Handle(GetSubLocationsRequest request, CancellationToken cancellationToken)
        {
            const string groupEndpoint = "cmn_department";

            if (!_cache.TryGetValue(groupEndpoint, out IEnumerable<SubLocation> subLocations))
            {
                var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) };
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.BuildAuthHeader());
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(groupEndpoint, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var listResult = (await response.Content.ReadAsAsync<ServiceNowListResult<ServiceNowDepartment>>()).Result;
                    if (listResult != null && listResult.Any())
                    {
                        var slList = new List<SubLocation>();
                        foreach (var snDept in listResult)
                        {
                            slList.Add(_mapper.Map<SubLocation>(snDept));
                        }
                        subLocations = slList;
                    }
                    _cache.Set(groupEndpoint, subLocations);
                }
            }

            return new SubLocationList { Data = subLocations };
        }
    }
}
