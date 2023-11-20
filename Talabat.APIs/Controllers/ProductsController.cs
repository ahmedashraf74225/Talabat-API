using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<ProductBrand> _brandsRepo;
        //private readonly IGenericRepository<ProductType> _typesRepo;

        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork
           )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            //_productRepo = ProductRepo;
            //_brandsRepo = brandsRepo;
            //_typesRepo = typesRepo;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(specParams); 
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);


            var specCount = new ProductWithFilterationForCountSpecification(specParams);
            var count     = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(specCount);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageSize,specParams.PageIndex,data,count));
        }

        [Authorize]
        [Cached(600)]
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);

            if (product is null) return NotFound(new ApiResponse(404));
            return  Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        [Cached(600)]
        [Authorize]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands= await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        [Cached(600)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }

    }
}
