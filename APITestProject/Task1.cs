using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace APITestProject
{
    [TestClass]
    public class Task1
    {
        RestClientHelper restClientHelper;

        [TestInitialize]
        public void Setup()
        {
            var baseURL = ConfigurationManager.AppSettings["DogAPIBaseURL"];
            this.restClientHelper = new RestClientHelper(baseURL);
        }

        [TestMethod]
        public void GetAllBreeds()
        {
            var response = restClientHelper.Get($"/breeds/list/all");
            var responseContent = response.Content;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responseContent);
        }
        [TestMethod]
        public void TestIfBreedExist()
        {
            string breed = "retriever";
            var response = restClientHelper.Get($"/breeds/list/all");
            var responseContent = response.Content;
            Assert.IsTrue(responseContent.Contains(breed));
        }
        [TestMethod]
        public void GetBreedSubBreeds()
        {
            string breed = "retriever";
            var response = restClientHelper.Get($"/breed/{breed}/list");
            var responseContent = response.Content;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(responseContent);
        }

        [TestMethod]
        public void GetRanndomImage()
        {
            var response = restClientHelper.Get($"/breed/retriever-golden/images/random");
            var responseContent = response.Content;
            var image = JsonConvert.DeserializeObject<Image>(responseContent);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(image.Message);
        }

    }
}
