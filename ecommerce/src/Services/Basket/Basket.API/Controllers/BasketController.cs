using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _repository;
        //private readonly DiscountGrpcService _discountGrpcService;
        //private readonly IPublishEndpoint _publishEndpoint;
        //private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository/*, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint, IMapper mapper*/)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            //_discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            //_publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // TODO : Communicate with Discount.Grpc
            // and Calculate latest prices of product into shopping cart
            // consume Discount Grpc
            //foreach (var item in basket.Items)
            //{
            //    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            //    item.Price -= coupon.Amount;
            //}

            return Ok(await _repository.UpdateBasket(basket));
        }
    }
}
