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
    public class OrderitemController : ControllerBase
    {
        private readonly cofecontext _context;

        public OrderitemController(cofecontext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Orderitem>> Get()
        {
            return _context.orderitems.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Orderitem> Get(int id)
        {
            var orderitem = _context.orderitems.Find(id);
            if (orderitem == null)
            {
                return NotFound();
            }
            return orderitem;
        }

        [HttpPost]
        public ActionResult<Orderitem> Post(OrderitemInsertModel model)
        {
            if (ModelState.IsValid)
            {
                var newOrderitem = new Orderitem
                {
                    OrderId = model.OrderId,
                    MenuItemId = model.MenuItemId,
                    Quantity = model.Quantity,
                    ItemPrice = model.ItemPrice,
                    IsCompleted = model.IsCompleted,
                    Note = model.Note
                };

                _context.orderitems.Add(newOrderitem);
                _context.SaveChanges();

                return CreatedAtAction("Get", new { id = newOrderitem.OrderItemId }, newOrderitem);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Orderitem orderitem)
        {
            if (id != orderitem.OrderItemId)
            {
                return BadRequest();
            }

            _context.Entry(orderitem).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderitem = _context.orderitems.Find(id);
            if (orderitem == null)
            {
                return NotFound();
            }

            _context.orderitems.Remove(orderitem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
