using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class ItemController : ControllerBase
    {
        //[Produces("application/json")]

        private readonly Models.ShopBridgeDb _context;

        public ItemController(Models.ShopBridgeDb context)
        {
            _context = context;
        }

        // This Method Adds Item
        [HttpPost("AddItem")]
        public async Task<ActionResult<Models.ItemMaster>> AddItem([FromBody]Models.ItemMaster item)
        {
            if (ModelState.IsValid)
            {
                if(ItemNameExists(item.ItemName))
                {
                    return Content("{\"status\" : \"fail\", \"message\" : \"Item Name Already Exist\"}");
                }
                _context.ItemMaster.Add(item);
                await _context.SaveChangesAsync();

                return Content("{\"status\" : \"success\", \"message\" : \"Item Details Added Sucessfully\"}");
            }

            return BadRequest(ModelState);
        }
    


        // This Method Gets an Item
        [HttpGet("GetItems")]
        public async Task<ActionResult<IList<Models.ItemMaster>>> GetItems()
        {
            return await _context.ItemMaster.ToListAsync();
        }

        // This Method Gets All Items
        [HttpGet("GetItem/{id}")]
        public async Task<ActionResult<Models.ItemMaster>> GetItem(int id)
        {
            var item = await _context.ItemMaster.FindAsync(id);

            if (item == null)
            {
                var model = new
                {
                    error = new
                    {
                        code = 1001,
                        message = "Item Not Found"
                    }
                };
                return NotFound(model);
            }

            return item;
        }

        // This Method Updates an Item
        [HttpPost("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] Models.ItemMaster item)
        { 

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.ItemId))
                {
                    var model = new
                    {
                        error = new
                        {
                            code = 1001,
                            message = "Item Not Found"
                        }
                    };
                    return NotFound(model);
                }
                else
                {
                    throw;
                }
            }
             
            return Content("{\"status\" : \"success\", \"message\" : \"Item Details Upated Sucessfully\"}");
        }


        // This Method Deletes an Item
        [HttpGet("DeleteItem/{id}")]
        public async Task<ActionResult<Models.ItemMaster>> DeleteItem(int id)
        {
            var item = await _context.ItemMaster.FindAsync(id);
            if (item == null)
            {
                var model = new
                {
                    error = new
                    {
                        code = 1001,
                        message = "Item Not Found"
                    }
                };
                return NotFound(model);
            }

            _context.ItemMaster.Remove(item);
            await _context.SaveChangesAsync();

            return Content("{\"status\" : \"success\", \"message\" : \"Item deleted Sucessfully\"}");
        }


        // Checks and returns Item
        private bool ItemExists(int id)
        {
            return _context.ItemMaster.Any(e => e.ItemId == id);
        }

        // Checks and returns Item by Name
        private bool ItemNameExists(string ItemName)
        {
            return _context.ItemMaster.Any(e => e.ItemName.Equals(ItemName));
        }
    }
}
