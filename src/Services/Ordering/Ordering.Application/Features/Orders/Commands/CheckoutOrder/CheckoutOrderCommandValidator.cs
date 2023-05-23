using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator() 
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Username is required").NotNull()
                .MaximumLength(50).WithMessage("userName must to exceed 50 character");

            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("Email Address is required");

            RuleFor(p => p.TotalPrice).NotEmpty().WithMessage("Price is required").
                GreaterThan(0).WithMessage("total price should be greter tha zero");
        }
    }
}
