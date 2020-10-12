using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace WYBlog.Controllers
{
    [Authorize]
    [ApiController]    
    [Route("[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : AbpController
    {
    }
}