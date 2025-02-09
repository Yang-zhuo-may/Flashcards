using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Modes
{
    internal class CardSession
    {
        public int Id { get; set; }
        public string Front {  get; set; }
        public string Back { get; set; }
        public string StackName { get; set; }
    }
}
