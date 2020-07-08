using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using AutoMapper;

using GenericRestService.ControllerFactory;
using Data;
using Data.Repositories;

namespace GenericRestService.Controllers
{
    [ApiController]
    [GenericRestControllerNameConvention]
    [Route("[controller]")]
    public class GenericRestController<T, TRequest, TResponse> : ControllerBase where T : class where TRequest : class where TResponse : class
    {
        private ILogger Logger { get; set; }

        private IGenericRepository<T> Repository { get; set; }

        private IMapper Mapper { get; set; }

        public GenericRestController(ILogger _logger, IGenericRepository<T> _repository, IMapper _mapper)
        {
            this.Logger = _logger;
            this.Repository = _repository;
            this.Mapper = _mapper;
        }

        [HttpGet]
        public async Task<TResponse> Get(int key)
        {
            //T db_model = Mapper.Map<TRequest, T>(request);
            var result = await Repository.Get(key);
            return Mapper.Map<T, TResponse>(result);
        }

        [HttpPost]
        public async Task<TResponse> Create(TRequest request)
        {
            T db_model = Mapper.Map<TRequest, T>(request);
            var result = await Repository.Add(db_model);
            return Mapper.Map<T, TResponse>(result);
        }
    }
}