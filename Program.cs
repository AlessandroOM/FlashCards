using Database;
using flashcards.utils;
using flashcards.Menus;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, welcome to FlashCards App!");

         
        var mapper = FlashCard.Mapping.InitializeAutomapper();
        var myContext = new MyContext();
        
        StackMenu stMenu = null;
        FlashCardMenu fcMenu = null;
        StudyMenu ssMenu = null;
        LessonsMenu lsMenu = null;

        string option = "";
        while (option.Trim() !="0")
        {
            Menu menu = new Menu(new string[] { "0", "S", "F", "R", "L"},
                             new string[] { "exit", "Stacks", "FlashCards", "Study", "Lessons"});
            option = menu.MenuExecute("Main", "Input 0 to exit or your option"); 

            switch (option)
            {
                case "0":
                    Console.WriteLine("bye bye, from FlashCards App!");
                    break;
                case "S":
                    if (stMenu == null)
                    {
                        stMenu = new StackMenu(mapper, myContext);
                    }
                    stMenu.Execute();
                    break;
                case "F":
                    if (fcMenu == null)
                    {
                        fcMenu = new FlashCardMenu(mapper, myContext);
                    }
                    fcMenu.Execute();
                    break;    
                case "R":
                    if (ssMenu == null)
                    {
                        ssMenu = new StudyMenu(mapper, myContext);
                    }
                    ssMenu.Execute();
                    break;  
                case "L":
                    if (lsMenu == null)
                    {
                        lsMenu = new LessonsMenu(mapper, myContext);
                    }
                    lsMenu.Execute();
                    break;            
                default:
                    break;
            }

        }
        
        

    }


}