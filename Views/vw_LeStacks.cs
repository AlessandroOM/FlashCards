using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcards.Models
{
    public class vw_LeStacks
    {
        public vw_LeStacks()
        {
          
        }
         
       public int StackId { get; set; }
       public string StackName { get; set; }

       public int FlashCardId { get; set; }

       public string FlashCardQuestion { get; set; }
       public string FlashCardAnswer { get; set; }

       public DateTime StudyDate { get; set; }
       public bool Sucess { get; set; }
       
    }
}