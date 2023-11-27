using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Курсач.Models;

namespace Курсач.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderstatusController : ControllerBase
    {
        private readonly cofecontext _context;

        public OrderstatusController(cofecontext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Orderstatus>> Get()
        {
            return _context.orderstatuses.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Orderstatus> Get(int id)
        {
            var orderstatus = _context.orderstatuses.Find(id);
            if (orderstatus == null)
            {
                return NotFound();
            }
            return orderstatus;
        }

        [HttpPost]
        public ActionResult<Orderstatus> Post(OrderstatusInsertModel orderstatusInsertModel)
        {
            if (ModelState.IsValid)
            {
                var newOrderstatus = new Orderstatus
                {
                    StatusName = orderstatusInsertModel.StatusName
                };

                _context.orderstatuses.Add(newOrderstatus);
                _context.SaveChanges();

                return CreatedAtAction("Get", new { id = newOrderstatus.StatusId }, newOrderstatus);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, OrderstatusInsertModel orderstatusInsertModel)
        {
            var existingOrderstatus = _context.orderstatuses.Find(id);

            if (existingOrderstatus == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingOrderstatus.StatusName = orderstatusInsertModel.StatusName;

                _context.orderstatuses.Update(existingOrderstatus);
                _context.SaveChanges();

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderstatus = _context.orderstatuses.Find(id);
            if (orderstatus == null)
            {
                return NotFound();
            }

            _context.orderstatuses.Remove(orderstatus);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
