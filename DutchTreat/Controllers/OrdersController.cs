using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public OrdersController(IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var newOrder = _mapper.Map<OrderModel, Order>(model);

                    if (newOrder.OrderDate == DateTime.MinValue)
                        newOrder.OrderDate = DateTime.Now;

                    if (User.Identity.Name == null)
                        return BadRequest("no user!");

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (currentUser == null)
                        return BadRequest("error of user");
                    newOrder.Buyer = currentUser;

                    _repository.AddEntity(newOrder);

                    if (!_repository.SaveAll())
                        throw new Exception("Can't add model");

                    return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderModel>(newOrder));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving Order: {ex}");

            }

            return BadRequest("Error while adding model ");
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                //var results = _repository.GetAllOrders(includeItems);
                var results = _repository.GetAllOrdersByUser(username, includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting Orders: {ex}");
                return BadRequest("server-error: can't get orders");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, id);
                if (order != null)
                    return Ok(_mapper.Map<Order, OrderModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting Orders: {ex}");
                return BadRequest("server-error: can't get orders");
            }
        }

    }
}