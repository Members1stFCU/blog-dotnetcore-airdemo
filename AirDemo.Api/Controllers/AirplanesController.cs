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
        public async Task<IActionResult> Get(string sn)
        {
            var plane = await _service.GetAirplane(sn);
            if (plane == null)
            {
                // Requested a resource that doesn't exist, so return a 404
                return NotFound();
            }
            else
            {
                return Ok(plane);
            }
        }

        // POST api/airplanes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AirplaneAddRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            await _service.RegisterNewAirplane(request);
            var plane = await _service.GetAirplane(request.SerialNumber);
            if (plane == null)
            {
                // There was some reason the resource does not exist in the database, so something was wrong with the request, return 400
                return BadRequest();
            }
            else
            {
                // Return 201 with the Location header indicating how to retrieve the resource, and the state of the resource after it was created
                return Created($"api/airplanes/{request.SerialNumber}", plane);
            }
        }

        // PUT api/airplanes/12345/fly
        [HttpPut("{sn}/fly")]
        public async Task<IActionResult> Fly(string sn, [FromBody]AirplaneFlyRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (await _service.FlyAirplane(sn, request))
            {
                // Return a 200, but provide the resource in the state it exists in after the request (courtesy to client to avoid another GET request)
                return Ok(await _service.GetAirplane(sn));
            }

            // The service indicated a failure which at this point means it is not found as no other known paths would return false, so return a 404
            return NotFound();
        }

        // PUT api/airplanes/12345/land
        [HttpPut("{sn}/land")]
        public async Task<IActionResult> Land(string sn, [FromBody]AirplaneLandRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (await _service.LandAirplane(sn, request))
            {
                // Return a 200, but provide the resource in the state it exists in after the request (courtesy to client to avoid another GET request)
                return Ok(await _service.GetAirplane(sn));
            }

            // The service indicated a failure which at this point means it is not found as no other known paths would return false, so return a 404
            return NotFound();
        }

        // DELETE api/airplanes/12345
        [HttpDelete("{sn}")]
        public async Task<IActionResult> Delete(string sn)
        {
            if (await _service.DeleteAirplane(sn))
            {
                // The resource no longer exists and there's nothing else to return, so return a 204
                return NoContent();
            }

            // The service indicated a failure which at this point means it is not found as no other known paths would return false, so return a 404
            return NotFound();
        }
    }
}