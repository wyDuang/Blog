using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;

namespace WYBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : AbpController
    {
    }
}
