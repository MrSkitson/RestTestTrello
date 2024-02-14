﻿using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using Newtonsoft.Json.Schema;
using System.Buffers.Text;

namespace RestTest
{
    public class GetBoardTrello : BaseClass
    {
       
        [Test]
        public void GetBoards()
        {
            var request = RequestWithAuth("/1/members/{member}/boards")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("member", "mr_skit");//path parametr

            var response = _client.Get(request);

            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getBoards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth("/1/board/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "5db7e6f535458d1f8a77c0e9");

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);

            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Homework"));
           
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getBoard.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        public void GetCards()
        {
            var request = RequestWithAuth("/1/lists/{idList}/cards")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("idList", "5db7e6f535458d1f8a77c0eb");

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));

        }
        [Test]
        public void CheckGetCard()
        {
            var request = RequestWithAuth("/1/cards/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "5df0e93e13a8de6d77bd5498");

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Itech"));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCard.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        
    }
}
