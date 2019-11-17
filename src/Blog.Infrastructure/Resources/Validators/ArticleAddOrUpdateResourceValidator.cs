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
            RuleFor(x => x.CategoryId)
                .NotEqual(0)
                .WithName("文章分类")
                .WithMessage("required|{PropertyName}必须大于0");              

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("标题")
                .WithMessage("required|{PropertyName}是必填的")
                .MaximumLength(200)
                .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");

            RuleFor(x => x.Html)
                .NotEmpty()
                .WithName("正文Html")
                .WithMessage("required|{{PropertyName}是必填的")
                .MinimumLength(1)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");

            RuleFor(x => x.Html)
                .NotEmpty()
                .WithName("正文Markdown")
                .WithMessage("required|{{PropertyName}是必填的")
                .MinimumLength(1)
                .WithMessage("minlength|{PropertyName}的最小长度是{MinLength}");

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithName("作者")
                .WithMessage("required|{{PropertyName}是必填的")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName}的最小长度是{MaxLength}");

            RuleFor(x => x.Remark)
                .NotNull()
                .WithName("备注")
                .WithMessage("required|{{PropertyName}是必填的")
                .MaximumLength(1000)
                .WithMessage("maxlength|{PropertyName}的最小长度是{MaxLength}");
        }
    }
}
