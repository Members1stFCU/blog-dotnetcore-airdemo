using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirDemo.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AirDemo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AirplanesController : Controller
    {
        private readonly AirplaneContext _context;

        public AirplanesController(AirplaneContext context)
        {
            _context = context;
        }

        // GET api/airplanes
        [HttpGet]
        public async Task<IEnumerable<Airplane>> Get()
        {
            return await _context.Airplanes.ToAsyncEnumerable().ToList();
        }

        // GET api/airplanes/12345
        [HttpGet("{sn}")]
        public async Task<Airplane> Get(string sn)
        {
            return await _context.Airplanes.Where(x => x.SerialNumber == sn).ToAsyncEnumerable().FirstOrDefault();
        }

        // POST api/airplanes
        [HttpPost]
        public async Task Post([FromBody]Airplane request)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
        }

        // PUT api/airplanes/12345/fly
        [HttpPut("{sn}/fly")]
        public async Task Fly(string sn, [FromBody]TimeSpan estimatedTripTime)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == sn).FirstOrDefault();
            if (airplane != null)
            {
                airplane.Fly(estimatedTripTime);
            }

            await _context.SaveChangesAsync();
        }

        // PUT api/airplanes/12345/land
        [HttpPut("{sn}/land")]
        public async Task Land(string sn, [FromBody]string airportCode)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == sn).FirstOrDefault();
            if (airplane != null)
            {
                airplane.Land(airportCode);
            }

            await _context.SaveChangesAsync();
        }

        // DELETE api/airplanes/12345
        [HttpDelete("{sn}")]
        public async Task Delete(string sn)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == sn).FirstOrDefault();
            if (airplane != null)
            {
                _context.Airplanes.Remove(airplane);
            }

            await _context.SaveChangesAsync();
        }
    }
}