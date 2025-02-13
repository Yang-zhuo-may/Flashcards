using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Modes
{
    internal class StudySessionSession
    {
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public string StackName {  get; set; }
        public int StackId { get; set; }
    }
}
