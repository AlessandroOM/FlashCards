using flashcards.Models;

namespace flashcards.DTOs
{
    public class StudyDTO : BaseDto
    {
        public StudyDTO()
        {
            StackId = 0;
            FlashCardId = 0;
            Response = "";
            HaveSucess = true;
            Stack = new Stacks(0, "");
            FlashCard = new FlashCardDb();
        }

        public int StackId { get; set ; }
        public  virtual Stacks Stack { get ; set ;}
        public int FlashCardId { get; set ; }
        public  virtual FlashCardDb FlashCard { get ; set ;}
        public DateTime date {get; set ;}
        public bool HaveSucess {get; set;}
        public string Response {get; set;}
        
    }
}
