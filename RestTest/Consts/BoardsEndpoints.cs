using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Consts
{
    public class BoardsEndpoints
    {
        public const string GetBoardUrl = "/1/board/{id}";
        public const string GetAllBoardUrl = "/1/members/{member}/boards";
        public const string CreateBoardUrl = "/1/boards";
        public const string DeleteBoardUrl = "/1/boards/{id}";
    }
}
