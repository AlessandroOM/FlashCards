using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcards.Models
{
    public class Study
    {
         [Key]
        public int Id { get ; set ;}

        public  virtual Stacks Stack { get ; set ;}

        [ForeignKey("StackId")]
        public int StackId { get ; set ;}
        public  virtual FlashCardDb FlashCard { get ; set ;}

        [ForeignKey("FlashCardId")]
        public int FlashCardId { get ; set ;}

        [Required]
        public DateTime date {get; set ;}

        [Required]
        public bool HaveSucess {get; set;}

        [Required]
        [MaxLength(50)]
        public string Response {get; set;}
    
    }
}