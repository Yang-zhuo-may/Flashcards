using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Modes
{
    internal class StudySessionSession
    {
        public string SessionName { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public string StackName {  get; set; }
    }
}
