using Database;
using flashcards.DTOs;
using flashcards.Models;
using flashcards.utils;
using flashcards.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace flashcards.Menus
{
    public class FlashCardMenu
    {
        private StackDTO _StackDTO;
        private FlashCardDTO _FlashCardDTO;

        private FlashCardRepository<FlashCardDTO> _flashRep;
        private StackRepository<StackDTO> _StackRep;

        private Mapper _mapper;
        private DbContext _db;
        
        private readonly Utilitys _util;

        private ConsoleKey _lastKey;

        public FlashCardMenu(Mapper mapper, DbContext context)
        {
            
            _mapper = mapper;
            _db = context;
            _flashRep = new((MyContext) _db, _mapper);
            _StackRep = new((MyContext) _db, _mapper);
            _util = new Utilitys();
            _StackDTO = new StackDTO();
           
        }

        public void Execute()
        {
            string option = "";
            while (option.Trim() !="0")
            {
                Menu menu = new Menu(new string[] { "0", "X", "V", "C", "E", "D"},
                                    new string[] { "exit", 
                                                   "Change current stack",
                                                   "View all FlashCards in stack",
                                                   "Create a Flashcard",
                                                   "Edit a Flashcard",
                                                   "Delete a flashcard"});

                Console.WriteLine("Current working stack: " + _StackDTO.Name);                                   
                option = menu.MenuExecute("FlashCards", "Input 0 to exit or your option"); 

                switch (option)
                {
                    case "0":
                        return;
                     case "X":
                        _StackDTO = internalGetStack();
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            continue;
                        }

                        _StackDTO = new StackDTO();
                        break;    
                    case "C":
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            CreateFlashCard();
                        }
                        
                        break;
                    case "V":
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            ViewFlashCards();
                        }
                        
                        break;    
                    case "E":
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            EditFlashCard();
                        }
                        
                        break;    
                    case "D":
                        if  (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            DeleteFlashCard();
                        }
                        
                        break;      
                    default:
                        break;
                }
            }
        }

        private string internalGetText(int idToCompare, string fieldName, bool validateDuplicate)
        {
            bool lbOk = false;
            bool lbEmpty =false;
            string FieldValue = "";
            while (!lbOk && !lbEmpty)
            {
                Console.WriteLine($"#= Please enter the value for {fieldName} OR empty TO EXIT:   =======#");
                FieldValue =_util.GetStr(false, FieldValue);
                
                if (string.IsNullOrEmpty(FieldValue) || FieldValue.Trim() == "NONE")
                {
                    lbEmpty = true;
                    FieldValue = "";
                    break;
                }

                if (idToCompare > 0)
                {
                    lbOk = true;
                    break;
                }
                else
                {
                    if (validateDuplicate)
                    {
                        lbOk =  (idToCompare <= 0) && (validateDuplicate) && (!_flashRep.FindByName(FieldValue));
                    }
                    else
                    {
                        lbOk =  (idToCompare <= 0) && (!_flashRep.FindByName(FieldValue));
                    }
                    
                }
                
                if (lbOk && idToCompare <= 0)
                {
                    lbOk = _util.Answer($" Field {fieldName} OK?(Y/N)", "Y");
                } else
                {
                    Console.WriteLine($"#= Error. This  {fieldName} already exists. =======#");    
                }
               
            }

            return FieldValue;
        }

        private void  DeleteFlashCard()
        {
            Console.Clear();
            Console.WriteLine("# Delete FlashCard ===========================#");
            var id = 0;
            bool lbOk = false;
            bool lbExit = false;
            while (!lbOk && !lbExit)
            {
                Console.WriteLine("# Please enter the FlashCard ID OR ZERO TO EXIT ==#");
                id = _util.GetInt(false, 0);
                lbExit = (id == 0);
                
                if (!lbExit)
                {
                    _FlashCardDTO = _flashRep.GetModelbyId(id);
                    lbOk = (_FlashCardDTO != null);
                } 

                if (!lbOk && !lbExit)
                {
                    Console.WriteLine("# Id does not exist! ==#");
                }

            }

            if (lbOk)
            {
                lbOk = _util.Answer($"Confirm delete : FlashCard:{_FlashCardDTO.Expression} on id:{id} OK?(Y/N)", "Y");   
                if (lbOk)
                {
                    _flashRep.Delete(id);
                    _StackDTO = _StackRep.GetModelbyId(_StackDTO.Id);
                }
                
            }

        }

        private void  EditFlashCard()
        {
            Console.Clear();
            Console.WriteLine($"# Edit FlashCard for stack {_StackDTO.Name} =================#");
            var id = 0;
            bool lbOk = false;
            bool lbExit = false;
            while (!lbOk && !lbExit)
            {
                Console.WriteLine("# Please enter the FlashCard ID OR ZERO TO EXIT ==#");
                id = _util.GetInt(false, 0);
                lbExit = (id == 0);
                
                if (!lbExit)
                {
                    _FlashCardDTO = _flashRep.GetModelbyId(id);
                    lbOk = (_FlashCardDTO != null);
                } 

                if (!lbOk && !lbExit)
                {
                    Console.WriteLine("# Id does not exist! ==#");
                }

            }

            if (lbOk)
            {
                Console.WriteLine("# FlashCard to edit      =============#");
                Console.WriteLine("# Stack Name :" + _FlashCardDTO.Stack.Name);
                Console.WriteLine("# Stack Id   :" + _FlashCardDTO.Stack.Id);
                Console.WriteLine("# Question :" + _FlashCardDTO.Expression);
                Console.WriteLine("# Answer :" + _FlashCardDTO.OppositeExpression);
                Console.WriteLine("#======================================#");
                string expression;
                expression = internalGetText(id, "Question", true);
                string oppositeexpression;
                oppositeexpression = internalGetText(id, "Answer", false); 
               
                if ((!string.IsNullOrEmpty(expression)) || ((!string.IsNullOrEmpty(expression)) && (expression.Trim() != "NONE")))
                {
                    var newModel = _flashRep.GetModelbyName(expression);
                    if (newModel != null)
                    {
                        Console.WriteLine($"#== Error : the answer {expression} is used by FlashCard id {newModel.Id} =#"); 
                        Console.WriteLine("#== Press any key to continue. =#"); 
                        Console.ReadLine();
                        EditFlashCard();
                    }
                    else
                    {
                        Console.WriteLine("# FlashCard changes ====+==============#");
                        Console.WriteLine("# Stack Name :" + _FlashCardDTO.Stack.Name);
                        Console.WriteLine("# Stack Id   :" + _FlashCardDTO.Stack.Id);
                        Console.WriteLine("# Question :" + _FlashCardDTO.Expression + " to " + expression);
                        Console.WriteLine("# Answer :" + _FlashCardDTO.OppositeExpression + " to " + oppositeexpression);
                        Console.WriteLine("#======================================#");
                        lbOk = _util.Answer("Confirm changes OK?(Y/N)", "Y");   

                        if (lbOk)
                        {
                            _FlashCardDTO.Expression = expression;
                            _FlashCardDTO.OppositeExpression =  string.IsNullOrEmpty(oppositeexpression) ? _FlashCardDTO.OppositeExpression : oppositeexpression;
                            _flashRep.Update(id, _FlashCardDTO);
                            _StackDTO = _StackRep.GetModelbyId(_StackDTO.Id);
                        }
                        else
                        {
                            EditFlashCard();
                        }
                    }
                }
            }

        }

        private void ViewFlashCards()
        {
            Console.Clear();
            Console.WriteLine($"# View FlashCards for stack {_StackDTO.Name}    ========================#");
            var olist = _StackDTO.FlashCards;
            foreach (var item in olist)
            {
                Console.WriteLine("Id: "+ item.Id + " Question : " + item.Expression + " answer : " + item.OppositeExpression);
            }

            Console.WriteLine("Press any key to continue.");         
            Console.ReadLine();
        }

        private StackDTO internalGetStack()
        {
            var id = 0;
            bool lbOk = false;
            bool lbExit = false;
            var olist = _StackRep.GetAll();
            Console.WriteLine("Available stacks."); 
            foreach (var item in olist)
            {
                Console.WriteLine("Id " + item.Id + " - " + item.Name);
            }

            StackDTO dto = null;
            while (!lbOk && !lbExit)
            {
                Console.WriteLine("# Please enter the Stack ID OR ZERO TO EXIT ==#");
                id = _util.GetInt(false, 0);
                lbExit = (id == 0);
               
                if (!lbExit)
                {
                    dto = _StackRep.GetModelbyId(id);
                    lbOk = (dto != null);
                    if (lbOk)
                    {
                        lbOk = _util.Answer($"Confirm Stack {dto.Name} on Id {dto.Id}?(Y/N)", "Y");
                    }
                    
                } 

                if (!lbOk && !lbExit)
                {
                    Console.WriteLine("# Id does not exist! ==#");
                }

            }

            return dto;
        }

        private void CreateFlashCard()
        {
            Console.Clear();
            Console.WriteLine($"# Create FlashCard for stack {_StackDTO.Name} =================#");

            if (_StackDTO != null && _lastKey != ConsoleKey.Escape)
            {

                bool lbOk = false;
                string expression = internalGetText(0, "Question", true);
                string oppositeExpression = internalGetText(0, "Answer", false);
                lbOk =  (!string.IsNullOrEmpty(expression) && expression.Trim() != "NONE");
                
                if (lbOk)
                {
                    if (_lastKey != ConsoleKey.Escape)
                    {
                        _FlashCardDTO = new FlashCardDTO(0, expression, oppositeExpression, _StackDTO.Id);
                        Console.WriteLine("# New FlashCard to create =============#");
                        Console.WriteLine("# Stack Name :" + _StackDTO.Name);
                        Console.WriteLine("# Stack Id   :" + _StackDTO.Id);
                        Console.WriteLine("# Question :" + expression);
                        Console.WriteLine("# Answer :" + oppositeExpression);
                        Console.WriteLine("#======================================#");
                        lbOk = _util.Answer("Confirm?(Y/N)", "Y");
                        if (lbOk)
                        {
                            _FlashCardDTO.StackId = _StackDTO.Id;
                            var ok = _flashRep.AddModel(_FlashCardDTO);
                            if (ok)
                            {
                                _StackDTO = _StackRep.GetModelbyId(_StackDTO.Id);
                            }
                        }

                        return;
                    }

                }
            }
        }


    }
}


