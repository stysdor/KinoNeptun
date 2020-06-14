using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Repositories;

namespace Cinema.Infrastructure.Services
{
    /// <summary>
    /// Provides methods for using customers. 
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _repository;
        private IMapper _mapper;

        public CustomerService(IMapper mapper) {
            _mapper = mapper;
            _repository = new CustomerRepository();
        }

        public CustomerDto Get(int id)
        {
            var customer = _repository.Get(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto GetByData(CustomerDto customerInput)
        {
            var customer = _mapper.Map<Customer>(customerInput);
            var customerOutput = _repository.GetOrAddByData(customer);
            return _mapper.Map<CustomerDto>(customerOutput);
        }

        public CustomerDto GetOrAddByData(CustomerDto customerInput)
        {
            var customer = _mapper.Map<Customer>(customerInput);
            var customerOutput = _repository.GetOrAddByData(customer);
            return _mapper.Map<CustomerDto>(customerOutput);
        }
    }
}
