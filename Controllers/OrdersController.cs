using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Курсач.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Курсач.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly cofecontext _context;

        public OrdersController(cofecontext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/orders/most-popular-product")]
        public ActionResult GetMostPopularProduct()
        {
            try
            {
                // Получаем самый популярный menuitemid из таблицы orderitems
                var mostPopularMenuItemId = _context.orderitems
                    .GroupBy(oi => oi.MenuItemId)
                    .OrderByDescending(group => group.Count())
                    .Select(group => group.Key)
                    .FirstOrDefault();

                if (mostPopularMenuItemId == 0)
                {
                    return Ok(new { MostPopularProduct = "No popular product found" });
                }

                // Получаем информацию о продукте из таблицы menuitems
                var mostPopularProduct = _context.menuitems
                    .Where(mi => mi.MenuItemId == mostPopularMenuItemId)
                    .FirstOrDefault();

                if (mostPopularProduct == null)
                {
                    return Ok(new { MostPopularProduct = "Product not found" });
                }

                return Ok(new { MostPopularProduct = mostPopularProduct.ItemName });
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<Orders>> Get()
        {
            return _context.orders.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Orders> Get(int id)
        {
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
        [HttpPost]
        public ActionResult<Orders> Post(OrdersInsertModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Проверяем, существует ли пользователь с указанным UserId
                    var user = _context.users.FirstOrDefault(u => u.UserId == model.UserId);
                    if (user == null)
                    {
                        return BadRequest("Пользователь с указанным UserId не найден.");
                    }

                    // Создаем новый заказ
                    var newOrder = new Orders
                    {
                        UserId = model.UserId,
                        OrderDate = model.OrderDate,
                        StatusId = model.StatusId
                    };

                    // Добавляем заказ в контекст
                    _context.orders.Add(newOrder);
                    _context.SaveChanges();

                    // Обрабатываем orderitems
                    foreach (var orderitem in model.OrderItems)
                    {
                        // Создаем новый orderitem
                        var newOrderitem = new Orderitem
                        {
                            OrderId = newOrder.OrderId,
                            MenuItemId = orderitem.MenuItemId,
                            Quantity = orderitem.Quantity,
                            ItemPrice = orderitem.ItemPrice,
                            IsCompleted = orderitem.IsCompleted,
                            Note = orderitem.Note
                        };

                        // Добавляем orderitem в контекст
                        _context.orderitems.Add(newOrderitem);
                    }

                    // Сохраняем изменения в контексте
                    _context.SaveChanges();

                    // Создаем новый объект для возврата, убирая связанные объекты
                    var result = new Orders
                    {
                        OrderId = newOrder.OrderId,
                        UserId = newOrder.UserId,
                        OrderDate = newOrder.OrderDate,
                        StatusId = newOrder.StatusId
                    };

                    return CreatedAtAction("Get", new { id = newOrder.OrderId }, result);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error placing order: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPut("{id}")]
        public IActionResult Put(int id, int statusId)
        {
            try
            {
                // Проверяем, существует ли новое значение statusId в таблице "orderstatuses"
                var isStatusValid = _context.orderstatuses.Any(s => s.StatusId == statusId);

                if (!isStatusValid)
                {
                    // Выводим в консоль сообщение о недопустимом значении
                    Console.WriteLine($"Ошибка изменения статуса заказа: Недопустимое значение StatusId: {statusId}");
                    return BadRequest("Недопустимое значение StatusId.");
                }

                var existingOrder = _context.orders.FirstOrDefault(o => o.OrderId == id);

                if (existingOrder == null)
                {
                    // Выводим в консоль сообщение о том, что заказ не найден
                    Console.WriteLine($"Ошибка изменения статуса заказа: Заказ с id {id} не найден.");
                    return NotFound("Заказ не найден.");
                }

                existingOrder.StatusId = statusId;

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Выводим в консоль сообщение об исключении
                Console.WriteLine($"Ошибка изменения статуса заказа: {ex.Message}");
                return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.orders.Remove(order);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
