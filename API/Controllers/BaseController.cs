using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace API.Controllers
{
    public class BaseController : ApiController
    {
        public Int64 LoggedInUserId
        {
            get
            {
                Int64 id = 0;
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims1 = identity.Claims;
                for (int i = 0; i < claims1.ToList().Count; i++)
                {
                    if (!string.IsNullOrEmpty(claims1.ToList()[i].Value))
                    {
                        string val = claims1.ToList()[i].Value;
                        string ID = val.Split('_')[1];
                        id = Convert.ToInt64(ID);
                    }
                    else
                    {
                        id = -1;

                        id = Convert.ToInt64(claims1.ToList()[i].Value);
                    }

                }
                //var claims = (Request.GetRequestContext().Principal as ClaimsPrincipal).Claims;
                //if (claims != null && claims.ToList().Exists(u => u.Type == "Id"))
                //    return Convert.ToInt32(claims.Where(u => u.Type == "Id").First().Value);
                //else

                return id;

            }
        }
    }
}
