﻿using System.Collections.Generic;

namespace IdentityModel.AspNetCore.ScopeValidation
{
    public class ScopeValidationOptions
    {
        public string ScopeClaimType { get; set; } = "scope";
        public IEnumerable<string> AllowedScopes { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}