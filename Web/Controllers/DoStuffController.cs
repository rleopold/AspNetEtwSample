using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web.Controllers
{
    public class DoStuffController : ApiController
    {
        public DoStuffController()
        {
            Instrumentation.Telemetry.Info("Created controller..");
        }
        public IHttpActionResult Get()
        {
            using(new Instrumentation.Telemetry())
            {
                //do some stuff that might take awhile
                int j =0;
                for (int i = 0; i < 100000000; i++ )
                {
                    j += i;
                }

                return Ok("done");
            }
        }

        [Route("api/dostuff/{count}")]
        public IHttpActionResult Get(int count)
        {
            // a different way, which is better?
            Instrumentation.Telemetry.Capture(() =>
            {
                int j = 0;
                for (int i = 0; i < 100000000; i++)
                {
                    j += i;
                }
            });

            return Ok("done");
        }

    }
}
