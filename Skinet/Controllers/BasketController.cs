using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Dtos;
using System.Threading.Tasks;

namespace Skinet.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly ICustomerBasket _customerBasket;
        private readonly IMapper _mapper;

        public BasketController(
            ICustomerBasket customerBasket,
            IMapper mapper)
        {
            _customerBasket = customerBasket;
            _mapper = mapper;
        }
        [HttpGet(nameof(GetBasketElement))]
        public async Task<ActionResult<CustomerBasket>> GetBasketElement([FromQuery]string Id)
        {
            var basketelements = await _customerBasket.GetBasketAsync(Id);
            return Ok(basketelements??new CustomerBasket(Id));
        }

        [HttpPost(nameof(UpdateProduct))]
        public async Task<ActionResult<CustomerBasket>> UpdateProduct(CustomerBasket product)
        {
            //var customerbasket = _mapper.Map<CustomerbasketDto, CustomerBasket>(product);
            var data =  await _customerBasket.UpdateBasketAsync(product);
            return Ok(data);
        }
        [HttpDelete(nameof(DeleteProduct))]
        public async Task DeleteProduct(string Id)
        {
            await _customerBasket.DeleteBasketAsync(Id);
        }
    }
}
