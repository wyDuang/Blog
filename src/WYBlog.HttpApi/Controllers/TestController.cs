using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace WYBlog
{
    [Route("api/test")]
    public class TestController : AbpController
    {
        [HttpGet]
        public async Task<List<TestModel>> GetAsync()
        {
            var testList = new List<TestModel>
            {
                new TestModel {Name = "John", BirthTime = new DateTime(1942, 11, 18)},
                new TestModel {Name = "Adams", BirthTime = new DateTime(1997, 05, 24)}
            };

            return await Task.Run(() => testList);
        }
    }
}