using MassAddress.Logic.Helpers;
using MassAddress.Models.Api;
using MassAddress.Models.Config;
using MassAddress.Models.Google;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MassAddress.Logic.Services
{
    public class AddressService
    {
        private readonly GoogleService _googleService;

        public AddressService(IOptions<GoogleConfigModel> config, IHttpClientFactory clientFactory)
        {
            _googleService = new GoogleService(config, clientFactory);
        }

        /// <summary>
        /// Function to take in a comma, break line, or new line separated list of locations
        /// and call out to the Google Places API, parse to find the State,
        /// and return a List of the name returned from Google along with the State.
        /// </summary>
        /// <param name="input">Separeted list of locations</param>
        /// <returns></returns>
        public List<AddressDto> FindStates(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            var output = new List<AddressDto>();
            var taskList = new List<Task<string>>();

            var locations = input.Split(new[] { Environment.NewLine, "<br/>", "<br>", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var location in locations)
            {
                taskList.Add(_googleService.MakeRequestTask(new List<string>() { location }));
            }
            Task.WaitAll(taskList.ToArray());

            foreach (var task in taskList)
            {
                if (!string.IsNullOrEmpty(task?.Result))
                {
                    var json = JsonConvert.DeserializeObject<GooglePlacesModel>(task.Result);

                    if (json?.candidates?.Any() ?? false)
                    {
                        var candidate = json.candidates.FirstOrDefault();
                        var state = AddressHelper.FindState(candidate.formatted_address);
                        if (!string.IsNullOrEmpty(state))
                        {
                            output.Add(new AddressDto() { Name = candidate.name, State = state });
                        }
                    }
                }
            }
            return output.OrderBy(x => x.State).ToList();
        }
    }
}
