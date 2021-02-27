using Microsoft.Extensions.Logging;
using CloudProvider.SDK.Abstract;
using Microsoft.AspNetCore.Mvc;
using CloudProvider.SDK.Persistence.Domain;
using System.Threading.Tasks;
using CloudProvider.IGS;

namespace CloudProvider.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        #region Fields

        private readonly IIGSCloudInfrastructure _infrastructure;
        private readonly ILogger<TestController> _logger;

        #endregion

        #region Ctor

        public TestController(ILogger<TestController> logger, IIGSCloudInfrastructure infrastructure)
        {
            _logger = logger;
            _infrastructure = infrastructure;
        }

        #endregion

        #region Actions

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CloudInfrastructure model)
        {
            if (model == null)
                return BadRequest();

            await _infrastructure.Create(model);
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            _infrastructure.Delete(name);
            
            return Ok();
        }

        #endregion
    }
}
