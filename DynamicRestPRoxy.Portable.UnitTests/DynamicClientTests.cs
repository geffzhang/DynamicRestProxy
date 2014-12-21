﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTestHelpers;

namespace DynamicRestProxy.PortableHttpClient.UnitTests
{
    [TestClass]
    public class DynamicClientTests
    {
        [TestMethod]
        [TestCategory("portable-client")]
        [TestCategory("integration")]
        public async Task CoordinateFromPostalCode()
        {
            dynamic client = new DynamicRestClient("http://dev.virtualearth.net/REST/v1/");

            string key = CredentialStore.RetrieveObject("bing.key.json").Key;

            dynamic result = await client.Locations.get(postalCode: "55116", countryRegion: "US", key: key);

            Assert.AreEqual(200, (int)result.statusCode);
            Assert.IsTrue(result.resourceSets.Count > 0);
            Assert.IsTrue(result.resourceSets[0].resources.Count > 0);

            var r = result.resourceSets[0].resources[0].point.coordinates;
            Assert.IsTrue((44.9108238220215).AboutEqual((double)r[0]));
            Assert.IsTrue((-93.1702041625977).AboutEqual((double)r[1]));
        }


        [TestMethod]
        [TestCategory("portable-client")]
        [TestCategory("integration")]
        public async Task GetMethod2PathAsProperty2Params()
        {
            dynamic client = new DynamicRestClient("http://openstates.org/api/v1/");

            string key = CredentialStore.RetrieveObject("sunlight.key.json").Key;

            var parameters = new Dictionary<string, object>()
            {
                { "lat", 44.926868 },
                { "long", -93.214049 } // since long is a keyword we need to pass arguments in a Dictionary
            };
            var result = await client.legislators.geo.get(paramList: parameters, apikey: key);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}

