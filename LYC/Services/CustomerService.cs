using LYC.Helpers;

namespace LYC.Services
{
    public interface ICustomerService
    {

    }

    class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}

