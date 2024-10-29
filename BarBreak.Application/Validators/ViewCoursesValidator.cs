using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BarBreak.Application.Validators
{
    public class ViewCoursesValidator : AbstractValidator<int>
    {
        public ViewCoursesValidator()
        {
            RuleFor(userId => userId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than zero.");
        }
    }
}

