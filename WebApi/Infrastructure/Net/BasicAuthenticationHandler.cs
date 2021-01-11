using System;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Infrastructure
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITrainerService _trainerService;
        private string failReason;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITrainerService p_trainerService)
            : base(options, logger, encoder, clock)
        {
            _trainerService = p_trainerService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return Task.FromResult(AuthenticateResult.NoResult());

            if (!Request.Headers.ContainsKey("Authorization")) {
                failReason = "Missing Authorization Header";
                return Task.FromResult(AuthenticateResult.Fail(failReason));
            }

            Trainer trainer = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var email = credentials[0];
                var password = credentials[1];
                trainer = _trainerService.GetTrainerByCredentials(email, password);
            }
            catch
            {
                failReason = "Invalid Authorization Header";
                return Task.FromResult(AuthenticateResult.Fail(failReason));
            }

            if (trainer == null)
            {
                failReason = "Invalid Username or Password";
                return Task.FromResult(AuthenticateResult.Fail(failReason));
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, trainer.Id.ToString()),
                new Claim(ClaimTypes.Name, trainer.Name),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            if (failReason != null)
            {
                Context.Response.ContentType = "application/json";
                using (var writer = new Utf8JsonWriter(Context.Response.BodyWriter))
                {
                    writer.WriteStartObject();
                    writer.WriteString("Error", failReason);
                    writer.WriteEndObject();
                    writer.Flush();
                }
            }
            return Task.CompletedTask;
        }
    }
}