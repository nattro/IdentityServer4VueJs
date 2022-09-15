﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace StsServerIdentity
{
    public class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("dataEventRecords", "Scope for the dataEventRecords ApiResource",
                    new List<string> { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user"}),
                new ApiScope("betsTalkService", "Scope for the betsTalkService ApiResource",
                    new List<string> { "role", "admin", "user", "betsTalkService", "betsTalkService.admin", "betsTalkService.user"}),
                new ApiScope("securedFiles",  "Scope for the securedFiles ApiResource",
                    new List<string> { "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user" })
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } ),
                new IdentityResource("betsTalkServiceScope",new []{ "role", "admin", "user", "betsTalkService", "betsTalkService.admin", "betsTalkService.user" } )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("DataEventRecordsApi")
                {
                    ApiSecrets =
                    {
                        new Secret("dataEventRecordsSecret".Sha256())
                    },
                    Scopes = new List<string> { "dataEventRecords" },
                    UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
                },
                new ApiResource("BetsTalkServiceApi")
                {
                    ApiSecrets =
                    {
                        new Secret("betsTalkServiceSecret".Sha256())
                    },
                    Scopes = new List<string> { "betsTalkService" },
                    UserClaims = { "role", "admin", "user", "betsTalkService", "betsTalkService.admin", "betsTalkService.user" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            // TODO use configs in app
            //var yourConfig = stsConfig["ClientUrl"];

            return new List<Client>
            {
                new Client
                {
                    ClientName = "vuejs_code_client",
                    ClientId = "vuejs_code_client",
                    AccessTokenType = AccessTokenType.Reference,
                    // RequireConsent = false,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44357",
                        "https://localhost:44357/callback.html",
                        "https://localhost:44357/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44357/",
                        "https://localhost:44357"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44357"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "role",
                        "profile",
                        "email"
                    }
                },
                new Client
                {
                    ClientName = "betstalk_spa_client",
                    ClientId = "betstalk_spa_client",
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44357",
                        "https://localhost:44357/callback.html",
                        "https://localhost:44357/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44357/",
                        "https://localhost:44357"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44357"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "betsTalkService",
                        "betsTalkServiceScope",
                        "role",
                        "profile",
                        "email"
                    }
                },
            };
        }
    }
}