using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Курсач.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace Курсач.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuitemController : ControllerBase
    {
        private readonly cofecontext _context;

        public MenuitemController(cofecontext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Menuitem>> Get()
        {
            return _context.menuitems.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Menuitem> Get(int id)
        {
            var menuitem = _context.menuitems.Find(id);
            if (menuitem == null)
            {
                return NotFound();
            }
            var menuItems = _context.menuitems.Include(mi => mi.menuitemcategory).ToList();
            return menuitem;
        }

        [HttpPost]
        public ActionResult<Menuitem> Post(MenuitemInsertModel model)
        {
            if (ModelState.IsValid)
            {
                var newMenuItem = new Menuitem
                {
                    ItemName = model.ItemName,
                    Description = model.Description,
                    Price = model.Price,
                    Availability = model.Availability
                };

                _context.menuitems.Add(newMenuItem);
                _context.SaveChanges();

                return CreatedAtAction("Get", new { id = newMenuItem.MenuItemId }, newMenuItem);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Menuitem menuitem)
        {
            if (id != menuitem.MenuItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuitem).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var menuitem = _context.menuitems.Find(id);
            if (menuitem == null)
            {
                return NotFound();
            }

            _context.menuitems.Remove(menuitem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
