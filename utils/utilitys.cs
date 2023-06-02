namespace flashcards.utils;


public class Utilitys
{
    public string GetStr(bool isEdit, string value)
    {
        string result = "NONE";
        if (isEdit)
        {
            Console.WriteLine($"#== Valor atual: {value} =#");
        }
        result = Console.ReadLine(); 
        if (result.Trim() == "" || result.Trim() == "0")
        {
                result = "NONE";
        }
        
        return result;
    }

    public int GetInt(bool isEdit, int value)
    {
        string result = "0";
        if (isEdit)
        {
            Console.WriteLine($"#== Valor atual: {value} =#");
        }
        result = Console.ReadLine(); 
        if (result.Trim() == "" || result.Trim() == "0")
        {
                result = "0";
        }
        int res ;
        int.TryParse(result, out res);
        return res;
    }

    public bool Answer(string Msg, string responseIfOK)
    {
        Console.WriteLine(Msg);
        string response = Console.ReadLine();
        if (string.IsNullOrEmpty(response))
        {
            return false;
        }
        else
        {
            return response.ToUpper().Trim() == responseIfOK.ToUpper().Trim();
        }


    }

}


 