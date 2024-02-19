using RestSharp;
using RestTest.Arguments.Holders;
using Parameter = System.Reflection.Metadata.Parameter;

namespace RestTest.Consts
{

    public  class UrlParamValues
    {
        public const string ExistingBoardId = "5db7e6f535458d1f8a77c0e9";
        public const string ExistingListId = "5db7e6f535458d1f8a77c0eb";
        public const string ExistingCardId = "5df0e93e13a8de6d77bd5498";
        public const string UserName = "mr_skit";

        public const string BoardIdToUpdate = "65cf7da9c1c5c45c8dd935fd";
        public const string CardIdToUpdate = "65cf7fc77d819a334ff97ea5";

        public const string ValidKey = "78cc9dcd0fe568a68b5e7d8cdfab098c";
        public const string InvalidKey = "78cc9dcd0fe568a68b5e7d8cdfab098e";
        public const string ValidToken = "ATTAa93f8c08f0535703af8d7bb92b0a2aa7b113474a74bad30fc61110df1b53b87dC13D048B";
        public const string InvalidToken = "AATAa93f8c08f0535703af8d7bb92b0a2aa7b113474a74bad30fc61110df1b53b87dC13D048B";

        public static IEnumerable<MyParameter> AuthQueryParams { get; } = new[]
        {
            new MyParameter("key", ValidKey, ParameterType.QueryString, true),
            new MyParameter("token", ValidToken, ParameterType.QueryString, true)
        };
        public static IEnumerable<MyParameter> InvalidAuthQueryParams { get; } = new[]
{
    new MyParameter("key", InvalidKey, ParameterType.QueryString, true),
    new MyParameter("token", InvalidToken, ParameterType.QueryString, true)
};
    }
}
