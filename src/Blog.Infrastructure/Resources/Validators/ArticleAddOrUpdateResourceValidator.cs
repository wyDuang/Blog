using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources.Validators
{
    public class ArticleAddOrUpdateResourceValidator<T> : AbstractValidator<T> where T : ArticleAddOrUpdateResource
    {
        public ArticleAddOrUpdateResourceValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithName("标题")
                .WithMessage("required|{PropertyName}是必填的")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");

            RuleFor(x => x.Html)
                .NotNull()
                .WithName("正文")
                .WithMessage("required|{{PropertyName}是必填的")
                .MinimumLength(100)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");
        }
    }
}
