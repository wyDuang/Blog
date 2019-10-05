using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.ResultModel
{
    public class ResourceValidationResult : Dictionary<string, IEnumerable<ResourceValidationError>>
    {
        public ResourceValidationResult() : base(StringComparer.OrdinalIgnoreCase)//忽略大小写
        {
        }

        public ResourceValidationResult(ModelStateDictionary modelState) : this()
        {
            if(modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if(errors?.Count > 0)
                {
                    var errorsToAdd = new List<ResourceValidationError>();
                    foreach (var modelError in errors)
                    {
                        var keyAndMessage = modelError.ErrorMessage.Split('|');
                        if (keyAndMessage.Length > 1)
                            errorsToAdd.Add(new ResourceValidationError(keyAndMessage[1], keyAndMessage[0]));
                        else
                            errorsToAdd.Add(new ResourceValidationError(keyAndMessage[0]));
                    }
                    Add(key, errorsToAdd);
                }



            }
        }
    }
}
