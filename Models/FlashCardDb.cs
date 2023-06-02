using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcards.Models
{
    public class FlashCardDb
    {
        public FlashCardDb()
        {
            Id = 0;
            Expression = "";
            OppositeExpression = "";
            StackId = 0;
        }
         public FlashCardDb(int id, string expression, string oppositeExpression, int stackid)
         {
            Id = id;
            Expression = expression;
            OppositeExpression = oppositeExpression;
            StackId = stackid;
         }
        [Key]
        public int Id { get ; set ;}

        [Required]
        [MaxLength(50)]
        public string Expression { get ; set ;}

        [MaxLength(50)]
        public string OppositeExpression{ get ; set ;}

      
        public  virtual Stacks Stack { get ; set ;}

         [ForeignKey("StackId")]
        public int StackId { get ; set ;}

        public  IEnumerable<Study> Studies { get; set; }

       
    }
}