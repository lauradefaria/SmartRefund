﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SmartRefund.WebAPI.Filters
{
    public class AuthorizationFilterEmployee : IAuthorizationFilter
    {
        private readonly ILogger<AuthorizationFilterEmployee> _logger;

        public AuthorizationFilterEmployee(ILogger<AuthorizationFilterEmployee> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "Unauthorized access. Authentication is required. Please authenticate at the /api/login route and copy the generated token to paste in the authorization." });
                return;
            }

            var userTypeClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userType")?.Value;

            _logger.LogInformation($"Employee Type: {userTypeClaim}");

            if (userTypeClaim == "employee")
            {
                return;
            }
            else
            {
                context.Result = new ForbidResult();
            }
            return;
        }
    }
}
