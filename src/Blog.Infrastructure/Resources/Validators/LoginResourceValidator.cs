using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources.Validators
{
    public class LoginResourceValidator<T> : AbstractValidator<T> where T : LoginResource
    {
        public LoginResourceValidator()
        {
            RuleFor(x => x.Username)
               .NotEmpty()
               .WithName("用户名")
               .WithMessage("required|{PropertyName}必须大于0")
               .MaximumLength(50)
               .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithName("密码")
               .WithMessage("required|{PropertyName}必须大于0")
               .MaximumLength(50)
               .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");
        }
    }
}
