using FluentValidation;
using WYBlog.Dtos;

namespace WYBlog.Validators
{
    public class CreateOrEditTagDtoValidator : AbstractValidator<CreateOrEditTagDto>
    {
        public CreateOrEditTagDtoValidator()
        {
            RuleFor(x => x.TagName)
                .NotNull()
                .WithName("标签名")
                .WithMessage("required|{PropertyName}是必填的")
                .MaximumLength(64)
                .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");
            RuleFor(x => x.TagKey)
                .NotNull()
                .WithName("标签Key")
                .MaximumLength(32)
                .WithMessage("maxlength|{PropertyName}的最大长度是{MaxLength}");
        }
    }
}