using AutoMapper;
using Events;
using Ordering.Application.Commands;

namespace Ordering.API.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
