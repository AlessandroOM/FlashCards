using flashcards.Models;

namespace flashcards.DTOs
{
    public class FlashCardDTO : BaseDto
    {
        public FlashCardDTO()
        {
            Expression = "";
            OppositeExpression = "";
            StackId = 0;
            Stack = new Stacks(0, "");
            Id = 0;
        }
        public FlashCardDTO(int id, string expression, string oppositeExpression, int stackid)
        {
            Expression = expression;
            OppositeExpression = oppositeExpression;
            StackId = stackid;
            //Stack = new Stacks(0, "");
            Id = id;
        }

        public string Expression { get; set ; }
        public string OppositeExpression { get; set ; }

        public int StackId { get; set ; }
            
        public  virtual Stacks Stack { get ; set ;}

        public virtual List<Study> Studies {get; set;}
        
        
    }
}
