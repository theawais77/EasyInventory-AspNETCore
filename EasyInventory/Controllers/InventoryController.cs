using EasyInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly Inventorycontext _dbcontext;
        public InventoryController(Inventorycontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            if (_dbcontext.Inventories == null)
            {
                return NotFound();
            }
            return await _dbcontext.Inventories.ToListAsync();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            if (_dbcontext.Inventories == null)
            {
                return NotFound();
            }
            var inventory = await _dbcontext.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;

        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Inventory>>> PostInventory(Inventory inventory)
        {
            _dbcontext.Inventories.Add(inventory);//save changes in database
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventory), new { id = inventory.ID }, inventory);//This is for front-end to return the same variable with id
        }


        [HttpPut]
        public async Task<IActionResult> PutInventory(int id,Inventory inventory)
        {
            if (id != inventory.ID)
            {
                return BadRequest();
            }
            _dbcontext.Entry(inventory).State = EntityState.Modified;
            //going to update database for our particular rrecord
            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryAvail(id)) 
                { 
                    return NotFound();
                }
                else
                {
                    throw;
                }
              
            }
            return Ok();

        }
        private bool InventoryAvail(int id)
        {
            return _dbcontext.Inventories.Any(x => x.ID == id);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventpry(int id)
        {
            if (_dbcontext.Inventories == null)
            {
                return NoContent();
            }
            var inventory = await _dbcontext.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            _dbcontext.Inventories.Remove(inventory);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }


    }
}
