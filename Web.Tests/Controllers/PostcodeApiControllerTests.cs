using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers.Postcodes;

namespace Web.Tests.Controllers
{
    public class PostcodeApiControllerTests
    {
        [Fact]
        public void GetPostcodes_Success()
        {
            Mock<IMemoryCache>
            PostcodeApiController controller = new PostcodeApiController()
        }
    }
}
