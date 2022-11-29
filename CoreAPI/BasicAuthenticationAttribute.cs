using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CoreAPI
{
	public class BasicAuthentication : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			var authorization = actionContext.Request.Headers.Authorization;
			if (authorization == null)
			{
				actionContext.Response = actionContext.Request
					.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
			}
			else
			{
				var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
				var data = Convert.FromBase64String(authenticationToken.ToString());
				var decodedToken = Encoding.UTF8.GetString(data);
				var username = decodedToken.ToString().Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
				var password = decodedToken.ToString().Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1];

				if (LoginSecurity.Login(username.ToString(), password.ToString()))
				{
					Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username.ToString()), null);
				}
				else
				{
					actionContext.Response = actionContext.Request
					.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
				}
			}
		}
	}
}
