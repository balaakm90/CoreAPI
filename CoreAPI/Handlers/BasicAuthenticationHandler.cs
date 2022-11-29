using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreAPI.Handlers
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{

		public BasicAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock)

		{
		}
		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{

			if (!Request.Headers.ContainsKey("Authorization"))
			{
				return AuthenticateResult.Fail("Authorization header not found");
			}
			else
			{
				try
				{
					var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
					var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
					var credentials = Encoding.UTF8.GetString(bytes);
					var username = credentials.Split(":")[0];
					var password = credentials.Split(":")[1];
					if (username.ToLower() == "balaakm90" && password == "Bala@16041990")
					{
						Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username.ToString()), null);
						var claims = new[] { new Claim(ClaimTypes.Name, username.ToString()) };
						var identity = new ClaimsIdentity(claims, Scheme.Name);
						var principal = new ClaimsPrincipal(identity);
						var ticket = new AuthenticationTicket(principal, Scheme.Name);
						return AuthenticateResult.Success(ticket);
					}
					else
					{
						return AuthenticateResult.Fail("UnAuthorized user");
					}
				}
				catch (Exception)
				{
					return AuthenticateResult.Fail("Error occured");
				}
			}
		}
	}
}
