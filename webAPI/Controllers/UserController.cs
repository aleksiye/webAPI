using Bogus;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            

            var context = new TerminContext();
            //var groupIds = context.Groups.Select(g => g.Id).ToList();
            //List<DataAccess.Entities.User> users = GetFakeUsers(groupIds);
            //context.Users.AddRange(users);

            var productsFaker = new Faker<Product>();
            productsFaker.RuleFor(p => p.Name, f => f.Commerce.ProductName());
            productsFaker.RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(1, 10000, 2)));
            productsFaker.RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

            var products = productsFaker.Generate(150);

            var userIds = context.Users.Select(u => u.Id).ToList();
            var orderFaker = new Faker<Order>();

            var orderProductFaker = new Faker<OrderProduct>();
            orderProductFaker.RuleFor(op => op.Product, f => f.PickRandom(products));
            orderProductFaker.RuleFor(op => op.Quantity, f => f.Random.Int(1, 10));

            orderFaker.RuleFor(o => o.UserId, f => f.PickRandom(userIds));
            orderFaker.RuleFor(o => o.OrderDate, f => f.Date.Past(2, DateTime.Now));
            orderFaker.RuleFor(o => o.OrderProducts, (f, o) => orderProductFaker.Generate(f.Random.Int(1,2)));

            //var orderProducts = orderProductFaker.Generate(500);
            var orders = orderFaker.Generate(15);
            context.Orders.AddRange(orders);
            context.SaveChanges();
            return Ok();
        }

        private static List<DataAccess.Entities.User> GetFakeUsers(List<int> groupIds)
        {
            var usersFaker = new Faker<DataAccess.Entities.User>();
            usersFaker.RuleFor(u => u.FirstName, f => f.Name.FirstName());
            usersFaker.RuleFor(u => u.LastName, f => f.Name.LastName());
            usersFaker.RuleFor(u => u.Username, f => f.Internet.UserName());
            usersFaker.RuleFor(u => u.GroupId, f => f.PickRandom(groupIds));
            var users = usersFaker.Generate(100);
            return users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
