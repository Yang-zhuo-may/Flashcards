using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Modes;
using static System.Net.Mime.MediaTypeNames;

namespace Flashcards.DTOs
{
    internal class CardMapper
    {
        static Dictionary<int, int> card_Id = new Dictionary<int, int>();
        public static List<CardDTO> TransitionToDTO(List<CardSession> cards)
        {
            List<CardDTO> cardDTOs = new List<CardDTO>(cards.Count);
            int interfaceID = 0;
            card_Id.Clear();

            for (int i = 0; i < cards.Count; i++)
            {
                interfaceID++;
                card_Id.Add(interfaceID, cards[i].Id);

                cardDTOs.Add(new CardDTO { Front = cards[i].Front, Back = cards[i].Back });
            }

            return cardDTOs;
        }

        public static int GetCardDataID(string interfaceID)
        {
            int cardDataID = int.Parse(interfaceID);
            foreach (var item in card_Id)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            if (card_Id[cardDataID] != null)
            { 
                return card_Id[cardDataID]; 
            }

            return 0;
        }
    }
}
