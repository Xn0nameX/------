using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Курсач.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace Курсач.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly cofecontext _context; // Внедрение контекста базы данных

        public TablesController(cofecontext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tables>> Get()
        {
            return _context.tables.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tables> Get(int id)
        {
            var table = _context.tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        [HttpPost]
        public ActionResult<Tables> Post(TablesInsertModel model)
        {
            if (ModelState.IsValid)
            {
                var newTable = new Tables
                {
                    TableNumber = model.TableNumber,
                    Capacity = model.Capacity,
                    IsReserved = model.IsReserved,
                    ReservedByUserId = null // Устанавливаем как null, если не связываем с пользователем
                };

                // Установите ReservedByUserId на null, если он не передан
                newTable.ReservedByUserId = model.ReservedByUserId;

                // Добавьте запись в контекст и сохраните изменения
                _context.tables.Add(newTable);
                _context.SaveChanges();

                return CreatedAtAction("Get", new { id = newTable.TableId }, newTable);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, TablesInsertModel tableInsertModel)
        {
            var existingTable = _context.tables.Find(id);

            if (existingTable == null)
            {
                return NotFound(); // Возврат кода 404 Not Found, если таблица с указанным id не найдена
            }

            if (ModelState.IsValid)
            {
                // Обновление существующей таблицы
                existingTable.TableNumber = tableInsertModel.TableNumber;
                existingTable.Capacity = tableInsertModel.Capacity;
                existingTable.IsReserved = tableInsertModel.IsReserved;

                _context.tables.Update(existingTable);
                _context.SaveChanges();

                return NoContent(); // 204 No Content
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var table = _context.tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }
            _context.tables.Remove(table);
            _context.SaveChanges();
            return NoContent();
        }
    }

}

