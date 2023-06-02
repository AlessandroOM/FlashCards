using System.ComponentModel.DataAnnotations;

namespace flashcards.Models
{
    public class Stacks
    {
       
        public Stacks(int id, string name)
        {
            Id = id;
            Name = name;
        }
        [Key]
        public int Id {get ; set ;}

        [Required]
        [MaxLength(50)]
        public string Name {get ; set ;}

        public  IEnumerable<FlashCardDb> FlashCards { get; set; }
        public  IEnumerable<Study> Studies { get; set; }
        

    }
}