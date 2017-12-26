using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirDemo.Service;
using AirDemo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirDemo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AirplanesController : Controller
    {
        private readonly IAirplaneService _service;

        public AirplanesController(IAirplaneService service)
        {
            _service = service;
        }

        // GET api/airplanes
        [HttpGet]
        public async Task<IEnumerable<AirplaneResponse>> Get()
        {
            return await _service.GetAirplanes();
        }

        // GET api/airplanes/12345
        [HttpGet("{sn}")]
        public async Task<AirplaneResponse> Get(string sn)
        {
            return await _service.GetAirplane(sn);
        }

        // POST api/airplanes
        [HttpPost]
        public async Task Post([FromBody]AirplaneAddRequest request)
        {
            await _service.RegisterNewAirplane(request);
        }

        // PUT api/airplanes/12345/fly
        [HttpPut("{sn}/fly")]
        public async Task Fly(string sn, [FromBody]AirplaneFlyRequest request)
        {
            await _service.FlyAirplane(sn, request);
        }

        // PUT api/airplanes/12345/land
        [HttpPut("{sn}/land")]
        public async Task Land(string sn, [FromBody]AirplaneLandRequest request)
        {
            await _service.LandAirplane(sn, request);
        }

        // DELETE api/airplanes/12345
        [HttpDelete("{sn}")]
        public async Task Delete(string sn)
        {
            await _service.DeleteAirplane(sn);
        }
    }
}