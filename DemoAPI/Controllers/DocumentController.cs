using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Controllers
{
    // localhost:5000/api/document
    [ApiController]
    // [Route("api/document")]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DemoDbContext _context;

        public DocumentController(DemoDbContext context)
        {
              _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDocument(Document document)
        {
            _context.Documents.Add(document);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FirstOrDefaultAsync(i => i.id == id);

            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);

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
