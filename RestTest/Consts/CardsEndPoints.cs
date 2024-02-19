using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Consts
{
    public class CardsEndPoints
    {
        public const string GetAllCardsUrl = "/1/lists/{list_id}/cards";
        public const string GetCardUrl = "/1/cards/{id}";

        public const string CreateCardUrl = "/1/cards";

        public const string DeleteCardUrl = "/1/cards/{id}";
        public const string UpdateCardUrl = "/1/cards/{id}";

    }
}
