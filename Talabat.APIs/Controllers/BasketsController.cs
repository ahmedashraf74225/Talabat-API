using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
  
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
           _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet] // api/baskets/id ->  api/baskets?id=1 (query param) -> 1 endpoint
        public async Task<ActionResult<CustomerBasket>> GetCustomerBakset(string id)
        {

            // [Get] or [Recreate After Expiration if if NULL]
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? new CustomerBasket(id) : basket;
        }

        [HttpPost] // api/baskets
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);

            // [ Update ] or [create For First Time]
            var Updatedbasket = await _basketRepository.UpdateBaksetAsync(mappedBasket);
            return Updatedbasket is null ? BadRequest(new ApiResponse(400)) : Ok(Updatedbasket);
        }

        [HttpDelete] // api/baskets/id
        public async Task<ActionResult<bool>>DeleteBakset(string id)
        {
            // [ Delete ]
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }

   
}
