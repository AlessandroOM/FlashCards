using flashcards.Models;

namespace flashcards.DTOs
{
    public class StackDTO : BaseDto
    {
        public string Name {get; set;}

        public virtual List<FlashCardDb> FlashCards {get; set;}
        public virtual List<Study> Studies {get; set;}
    }
}