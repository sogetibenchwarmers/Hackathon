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
    public class GetLocationsHandler : IRequestHandler<GetLocationsRequest, LocationList>
    {
        private readonly ServiceNowOptions _options;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetLocationsHandler(IOptions<ServiceNowOptions> options, IMapper mapper, IMemoryCache cache)
        {
            _options = options.Value;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<LocationList> Handle(GetLocationsRequest request, CancellationToken cancellationToken)
        {
            const string locationEndpoint = "cmn_location";
            const string leafLocationsKey = "LeafLocations";

            if (_cache.TryGetValue(leafLocationsKey, out List<Location> leafLocations))
            {
                return new LocationList { Data = leafLocations };
            }

            if (!_cache.TryGetValue(locationEndpoint, out IEnumerable<ServiceNowLocation> locations))
            {
                var client = new HttpClient { BaseAddress = new Uri(_options.ServiceNowHost) };
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.BuildAuthHeader());
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(locationEndpoint, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    locations = (await response.Content.ReadAsAsync<ServiceNowLocationsResult>()).Result;
                    var left = 0;
                    foreach (var loc in locations.Where(l => l.Parent?.Value == null))
                    {
                        left = TraverseHierarchy(loc, locations, left);
                    }
                    var test = new List<string>();
                    _cache.Set(locationEndpoint, locations.OrderBy(l => l.Left));
                }
            }

            leafLocations = new List<Location>();
            foreach (var loc in locations.Where(l => l.isLeaf))
            {
                leafLocations.Add(_mapper.Map<Location>(loc));
            }
            _cache.Set(leafLocationsKey, leafLocations);
            return new LocationList { Data = leafLocations };
        }

        private int TraverseHierarchy(ServiceNowLocation location, IEnumerable<ServiceNowLocation> fullList, int left)
        {
            location.Left = ++left;
            var children = fullList.Where(l => l.Parent?.Value == location.Id);
            ServiceNowLocation lastChild = null;
            if (children != null && children.Any())
            {
                foreach (var loc in children)
                {
                    left = TraverseHierarchy(loc, fullList, left);
                    lastChild = loc;
                }
            }
            location.Right = lastChild == null ? ++left : lastChild.Right + 1;
            return location.Right;
        }
    }
}
