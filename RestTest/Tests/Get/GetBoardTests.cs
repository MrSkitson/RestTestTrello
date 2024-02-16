using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestTest.Arguments.Holders;
using RestTest.Arguments.Providers;
using RestTest.Consts;
using RestTest.Tests;
using System.Net;

public class GetBoardTests : BaseTest
{
    //private static IRestClient _client;


    [Test]
    public void GetBoards()
    {
        var request = RequestWithAuth(BoardsEndpoints.GetAllBoardUrl)
            .AddQueryParameter("field", "id, name")
            .AddUrlSegment("member", UrlParamValues.UserName);//path parametr

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
        var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
          .AddQueryParameter("field", "id, name")
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = _client.Get(request);
        Console.WriteLine(response.Content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var responseContent = JToken.Parse(response.Content);
        Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Homework"));

        var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getBoard.json"));
        Assert.True(responseContent.IsValid(jsonSchema));
    }

}
