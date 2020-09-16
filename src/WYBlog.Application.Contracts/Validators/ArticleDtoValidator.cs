using FluentValidation;
using WYBlog.Dtos;

namespace WYBlog.Validators
{
    public class ArticleDtoValidator : AbstractValidator<ArticleDto>
    {
        public ArticleDtoValidator()
        {
            RuleFor(x => x.Title).MaximumLength(256).WithMessage("文章标题不能超过256字节");
        }
    }
}