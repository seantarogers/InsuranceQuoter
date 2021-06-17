// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityProvider.Quickstart
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityModel;
    using IdentityServer4.Test;

    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "06c71238-0137-4df6-bb6a-e50e62a4a7c5", 
                Username = "Sean", Password = "Password1",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Sean Rogers"),
                    new Claim(JwtClaimTypes.GivenName, "Sean"),
                    new Claim(JwtClaimTypes.FamilyName, "Rogers"),
                    new Claim(JwtClaimTypes.Email, "seanrogers@email.com"),
                    new Claim("country", "UK")
                }
            }
        };
    }
}