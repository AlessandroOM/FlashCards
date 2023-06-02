using Database;
using flashcards.DTOs;
using flashcards.Models;
using flashcards.utils;
using flashcards.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace flashcards.Menus
{
    public class StackMenu
    {
        private Stacks _stackModel;
        private StackDTO _stackDto;

        private StackRepository<StackDTO> _stackRep;

        private Mapper _mapper;
        private DbContext _db;
        
        private readonly Utilitys _util;

        public StackMenu(Mapper mapper, DbContext context)
        {
            
            _mapper = mapper;
            _db = context;
            _stackRep = new((MyContext) _db, _mapper);
            _util = new Utilitys();
           
        }

        public void Execute()
        {
            string option = "";
            while (option.Trim() !="0")
            {
                Menu menu = new Menu(new string[] { "0", "I", "E", "V", "D"},
                                    new string[] { "exit", "Insert", "Edit", "View", "Delete"});
                option = menu.MenuExecute("Stacks", "Input 0 to exit or your option"); 

                switch (option)
                {
                    case "0":
                        return;
                    case "I":
                        CreateStack();
                        break;
                    case "V":
                        ViewStacks();
                        break;    
                    case "E":
                        EditStack();
                        break;    
                    case "D":
                        DeleteStack();
                        break;      
                    default:
                        break;
                }
            }
        }

        private string internalGetName(int idToCompare)
        {
            bool lbOk = false;
            bool lbEmpty =false;
            string StackName = "";
            //Console.WriteLine($"idToCompare {idToCompare}");
            while (!lbOk && !lbEmpty)
            {
                Console.WriteLine("#= Please enter the Stack name OR empty TO EXIT:   =======#");
                StackName =_util.GetStr(false, StackName);
                
                if (string.IsNullOrEmpty(StackName) || StackName.Trim() == "NONE")
                {
                    lbEmpty = true;
                    StackName = "";
                    break;
                }

                if (idToCompare > 0)
                {
                    lbOk = true;
                    break;
                }
                else
                {
                    lbOk =  (idToCompare <= 0) && (!_stackRep.FindByName(StackName));
                }

                //Console.WriteLine("internalgetname lbok "+ lbOk);                
                if (lbOk && idToCompare <= 0)
                {
                    lbOk = _util.Answer("Data OK?(Y/N)", "Y");
                } else
                {
                    Console.WriteLine("#= Error. This Name already exists. =======#");    
                }
                
               
            }

            return StackName;

        }

        private void  DeleteStack()
        {
            Console.Clear();
            Console.WriteLine("# Delete Stack ===========================#");
            var id = 0;
            bool lbOk = false;
            bool lbExit = false;
            while (!lbOk && !lbExit)
            {
                Console.WriteLine("# Please enter the Stack ID OR ZERO TO EXIT ==#");
                id = _util.GetInt(false, 0);
                lbExit = (id == 0);
                
                if (!lbExit)
                {
                    _stackDto = _stackRep.GetModelbyId(id);
                    lbOk = (_stackDto != null);
                } 

                if (!lbOk && !lbExit)
                {
                    Console.WriteLine("# Id does not exist! ==#");
                }

            }

            if (lbOk)
            {
                lbOk = _util.Answer($"Confirm delete : Stack:{_stackDto.Name} on id:{id} OK?(Y/N)", "Y");   
                if (lbOk)
                {
                    _stackRep.Delete(id);
                }
                
            }

        }

        private void  EditStack()
        {
            Console.Clear();
            Console.WriteLine("# Edit Stacks ===========================#");
            var id = 0;
            bool lbOk = false;
            bool lbExit = false;
            while (!lbOk && !lbExit)
            {
                Console.WriteLine("# Please enter the Stack ID OR ZERO TO EXIT ==#");
                id = _util.GetInt(false, 0);
                lbExit = (id == 0);
                
                if (!lbExit)
                {
                    _stackDto = _stackRep.GetModelbyId(id);
                    Console.WriteLine("valida se existe it no bd " + _stackDto.Id);
                    lbOk = (_stackDto != null);
                } 

                if (!lbOk && !lbExit)
                {
                    Console.WriteLine("# Id does not exist! ==#");
                }

            }

            if (lbOk)
            {
                string StackName;
                StackName = internalGetName(id);
                
                if ((!string.IsNullOrEmpty(StackName)) || ((!string.IsNullOrEmpty(StackName)) && (StackName.Trim() != "NONE")))
                {
                    var newModel = _stackRep.GetModelbyName(StackName);
                
                    if (newModel != null)
                    {
                        Console.WriteLine($"#== Error : the name {StackName} is used by Stack id {newModel.Id} =#"); 
                        Console.WriteLine("#== Press any key to continue. =#"); 
                        Console.ReadLine();
                        EditStack();
                    }
                    else
                    {
                        lbOk = _util.Answer($"Confirm changes : name:{_stackDto.Name} to {StackName} on id:{id} OK?(Y/N)", "Y");   

                        if (lbOk)
                        {
                            _stackDto.Name = StackName;
                            _stackRep.Update(id, _stackDto);
                        }
                        else
                        {
                            EditStack();
                        }
                    }
                }
            }

        }

        private void ViewStacks()
        {
            Console.Clear();
            Console.WriteLine("# View Stacks ===========================#");
            var olist = _stackRep.GetAll();
           
            foreach (var item in olist)
            {
                Console.WriteLine(item.Id + " - " + item.Name + " FlahsCards " + item.FlashCards.Count());
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        private void CreateStack()
        {
            Console.Clear();
            Console.WriteLine("# Create Stack ===========================#");
            _stackDto = new StackDTO();
            bool lbOk = false;
            string StackName = "";
            StackName = internalGetName(0);
            lbOk =  (!string.IsNullOrEmpty(StackName) && StackName.Trim() != "NONE");
            
            if (lbOk)
            {
                Console.WriteLine("# New Stack to create =============#");
                Console.WriteLine("# Stack Name :" + StackName);
                lbOk = _util.Answer("Confirm?(Y/N)", "Y");
                if (lbOk)
                {
                    _stackDto.Name = StackName;
                    var ok = _stackRep.AddModel(_stackDto);
                }

            }
        }


    }
}


