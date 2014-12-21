﻿using System;
using System.IO;
using System.Dynamic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

using DynamicRestProxy.PortableHttpClient;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTestHelpers;

namespace Client.Google.UnitTests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        [TestCategory("portable-client")]
        [TestCategory("integration")]
        [TestCategory("google")]
        public async Task AuthenticateAndGetUserName()
        {
            dynamic google = new DynamicRestClient("https://www.googleapis.com/", null, async (request, cancelToken) =>
            {
                // this demonstrates how t use the configuration callback to handle authentication 
                var auth = new GoogleOAuth2("email profile");
                var token = await auth.Authenticate("", cancelToken);
                Assert.IsNotNull(token, "auth failed");

                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", token);
            });

            var profile = await google.oauth2.v1.userinfo.get();

            Assert.IsNotNull(profile);
            Assert.AreEqual("Kackman", (string)profile.family_name);
        }
    }
}
