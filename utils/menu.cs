namespace flashcards.utils
{
    public class Menu
    {
        private  Dictionary<string, string> _ListOptions = new Dictionary<string, string>();
        public Menu(string[] menuNumberOptions, string[] menuTextOptions)
        {
           for (var i = 0; i < menuNumberOptions.Length; i++)
           {
            _ListOptions.Add(menuNumberOptions[i],menuTextOptions[i]);
           }
        }

        public string MenuExecute(string title, string questionMessage)
        {
            string result = "X";
            bool validOption = false;
            while (!validOption)
            {
                //Console.Clear();
                Console.WriteLine($"+----------------------------[{title}]");
                foreach (var item in _ListOptions)
                {
                
                Console.WriteLine(item.Key.ToString() + " - " + item.Value);
                }
                Console.WriteLine($"+----------------------------[{questionMessage}]");
                result = Console.ReadLine().ToUpper();
                validOption = _ListOptions.ContainsKey(result);

                if (!validOption)
                {
                    Console.WriteLine("+-------- invalid option ---------+");
                }
               /*  if (result == "0" || result == "S" ||
                    result == "F" || result == "R" ||
                    result == "L" )
                {
                    validOption = true;
                } else
                {  
                   Console.WriteLine("+-------- invalid option ---------+");
                } */
            }

            return result;
                             
        }
    }
}