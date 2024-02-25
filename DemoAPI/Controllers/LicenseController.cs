using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Controllers
{
     
    // localhost:5000/api/license
    [ApiController]
   // [Route("api/license")]
   // [Route("api/license/{id}")] => id 1 olanı getir
   [Route("api/[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly DemoDbContext _context;

        public LicenseController(DemoDbContext context)
        {
            _context = context;

            //_licenses = new List<License>
            //{
            //    new () {id = 1, LicenseKey = "abc", ExpirationDate = "24-02"},
            //    new () {id = 2, LicenseKey = "def", ExpirationDate = "25-02"},
            //    new () {id = 3, LicenseKey = "ghj", ExpirationDate = "26-02"},
            //};
            
        }

        [HttpGet]
       public async Task<IActionResult> GetLicenses()
        {
            var licences = await _context.Licenses.ToListAsync();

            return Ok(licences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLicenses(int? id)
        {
            if (id== null)
            {
                return NotFound();
            }

            var p = await _context.Licenses.FirstOrDefaultAsync(i => i.id == id);

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLisence(License license)
        {
            _context.Licenses.Add(license);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLicenses), new { id = license.id }, license);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLicense(int id, License license)
        {
            if(id != license.id)
            {
                return BadRequest();
            }

            var entity = await _context.Licenses.
                FirstOrDefaultAsync(i=>i.id == id);

            if(entity == null)
            {
                return NotFound();
            }

            entity.LicenseKey = license.LicenseKey;
            entity.ExpirationDate = license.ExpirationDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound(e);
                
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicense(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var license = await _context.Licenses.FirstOrDefaultAsync(i => i.id == id);

            if (license == null)
            {
                return NotFound();
            }

            _context.Licenses.Remove(license);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }

            return Ok();
        }

    }
}
