using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Factories
{
    public interface IOrderViewModelFactory
    {
        OrderViewModel CreateViewModel(Order order);
    }
}
