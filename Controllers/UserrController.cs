using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Курсач.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
namespace Курсач.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserrController : ControllerBase
    {
        private readonly cofecontext _context; // Внедрение контекста базы данных
        private readonly IConfiguration _configuration;

        public UserrController(cofecontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser(UserAuthenticationModel authenticationModel)
        {
            var user = AuthenticateUser(authenticationModel.PhoneNumber, authenticationModel.Password);

            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private Userr AuthenticateUser(string phoneNumber, string password)
        {
            // Проведите аутентификацию пользователя здесь, сравнивая номер телефона и пароль
            var user = _context.users.FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password);

            return user;
        }

        private string GenerateJwtToken(Userr user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]); // Здесь используется секретный ключ из конфигурации

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    // Добавьте другие необходимые claims
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Время жизни токена
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Userr>> Get()
        {
            return _context.users.ToList();
        }
        [HttpGet("{userId}/orders")]
        public ActionResult<IEnumerable<UserOrderDto>> GetUserOrders(int userId)
        {
            var userOrders = _context.orders
                .Where(o => o.UserId == userId)
                .ToList();

            if (userOrders.Count == 0)
            {
                return NotFound("Пользователь не имеет заказов.");
            }

            var userOrderDtos = new List<UserOrderDto>();

            foreach (var order in userOrders)
            {
                var orderItems = _context.orderitems
                    .Where(oi => oi.OrderId == order.OrderId)
                    .ToList();

                var orderItemDtos = new List<OrderItemDto>();

                foreach (var orderItem in orderItems)
                {
                    var menuItem = _context.menuitems
                        .FirstOrDefault(mi => mi.MenuItemId == orderItem.MenuItemId);

                    if (menuItem != null)
                    {
                        var orderItemDto = new OrderItemDto
                        {
                            MenuItemId = menuItem.MenuItemId,
                            MenuItemName = menuItem.ItemName,
                            MenuItemPrice = menuItem.Price,
                        };

                        orderItemDtos.Add(orderItemDto);
                    }
                }
                var orderStatus = _context.orderstatuses.FirstOrDefault(os => os.StatusId == order.StatusId);
                var userOrderDto = new UserOrderDto
                {
                    OrderId = order.OrderId,
                    OrderItems = orderItemDtos,
                    OrderStatus = orderStatus != null ? orderStatus.StatusName : "В обработке" // Используйте ваше предпочтительное значение по умолчанию
                };

                userOrderDtos.Add(userOrderDto);
            }

            return Ok(userOrderDtos);
        }

        [HttpGet("{phoneNumber}")]
        public ActionResult<Userr> Get(string phoneNumber)
        {
            var user = _context.users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<Userr> Post(UserInsertModel userModel)
        {
            if (ModelState.IsValid)
            {
                // Найдите роль "Пользователь" по имени
                var role = _context.userroles.SingleOrDefault(r => r.RoleName == "Пользователь");
                var CreatedDate=userModel.CreatedAt;
                CreatedDate=DateTime.Now;


                if (role == null)
                {
                    return BadRequest("Роль 'Пользователь' не найдена.");
                }

                var newUser = new Userr
                {
                    PhoneNumber = userModel.PhoneNumber,
                    Username = userModel.Username,
                    Password = userModel.Password,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    RoleId = role.RoleId, 
                    DOB = userModel.DateOfBirth,
                    CreatedAt = CreatedDate,
                };

                _context.users.Add(newUser);
                _context.SaveChanges();

                return CreatedAtAction("Get", new { id = newUser.UserId }, newUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, UserInsertModel userInsertModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.users.Find(id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                // Найдите соответствующую роль "Пользователь" по её идентификатору или другим критериям
                var userRole = _context.userroles.SingleOrDefault(r => r.RoleName == "Пользователь");

                if (userRole == null)
                {
                    return NotFound("Роль 'Пользователь' не найдена.");
                }

                existingUser.RoleId = userRole.RoleId;
                existingUser.PhoneNumber = userInsertModel.PhoneNumber;
                existingUser.Username = userInsertModel.Username;
                existingUser.Password = userInsertModel.Password;
                existingUser.FirstName = userInsertModel.FirstName;
                existingUser.LastName = userInsertModel.LastName;
                existingUser.DOB = userInsertModel.DateOfBirth;
                existingUser.CreatedAt = userInsertModel.CreatedAt;

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
            var user = _context.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}


