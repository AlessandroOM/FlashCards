using Database;
using flashcards.DTOs;
using flashcards.Models;
using flashcards.utils;
using flashcards.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace flashcards.Menus
{
    public class StudyMenu
    {
        private StudyDTO _StudyDTO;
        private StackDTO _StackDTO;
        private FlashCardDTO _FlashCardDTO;

        private FlashCardRepository<FlashCardDTO> _flashRep;
        private StackRepository<StackDTO> _StackRep;
        private StudyRepository<StudyDTO> _StudyRep;

        private Mapper _mapper;
        private DbContext _db;
        
        private readonly Utilitys _util;

        private ConsoleKey _lastKey;

        public StudyMenu(Mapper mapper, DbContext context)
        {
            
            _mapper = mapper;
            _db = context;
            _flashRep = new((MyContext) _db, _mapper);
            _StackRep = new((MyContext) _db, _mapper);
            _StudyRep = new((MyContext) _db, _mapper);
            _util = new Utilitys();
            _StackDTO = new StackDTO();
           
        }

        public void Execute()
        {
            string option = "";
            while (option.Trim() !="0")
            {
                Menu menu = new Menu(new string[] { "0", "X", "S"},
                                    new string[] { "exit", 
                                                   "Change current stack",
                                                   "Study",
                                                   });

                Console.WriteLine("Current working stack: " + _StackDTO.Name);                                   
                option = menu.MenuExecute("Studies", "Input 0 to exit or your option"); 

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
                    case "S":
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            StartStudy();
                        }
                        
                        break;
                    case "V":
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            
                        }
                        
                        break;    
                    case "E":
                        if (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            
                        }
                        
                        break;    
                    case "D":
                        if  (_StackDTO != null && _StackDTO.Id > 0)
                        {
                            
                        }
                        
                        break;      
                    default:
                        break;
                }
            }
        }

        private void internalSaveStudy(bool success, string answer)
        {
            //Console.WriteLine("internalSaveStudy 3");
            _StudyDTO = new StudyDTO();
            _StudyDTO.date = DateTime.Now;
            _StudyDTO.HaveSucess = success;
            _StudyDTO.Response = answer;
            //_StudyDTO.FlashCard = _mapper.Map<FlashCardDTO, FlashCardDb>(_FlashCardDTO);

            _StudyDTO.FlashCardId = _FlashCardDTO.Id;
            _StudyDTO.StackId = _StackDTO.Id;
            // Console.WriteLine($"stkId {_StudyDTO.StackId} flsID {_FlashCardDTO.Id}");
            // Console.ReadLine();
            _StudyRep.AddModel(_StudyDTO);

        }

        private void StartStudy()
        {
            int totalStacks = _StackDTO.FlashCards.Count();
            Console.WriteLine($" flashs {totalStacks}");
            bool lbok = true;
            bool lbRun = true;
            int success = 0;
            int fails = 0;
            string answer = "";
            Random random = new Random();
            while (lbRun)
            {
                int CurrentIndex = random.Next(0, totalStacks);
                Console.WriteLine($" CurrentIndex {CurrentIndex}");
                _FlashCardDTO = _mapper.Map<FlashCardDb, FlashCardDTO>(_StackDTO.FlashCards[CurrentIndex]);
                if (_FlashCardDTO != null)
                {
                    Console.WriteLine("Type answer for question OR empty to EXIT.");
                    Console.WriteLine($"\n{_FlashCardDTO.Expression}");
                    answer = internalGetText();
                    if (string.IsNullOrEmpty(answer) || string.IsNullOrWhiteSpace(answer))
                    {
                        lbRun = false;
                        break;
                    }
                    else
                    {
                        lbok = (_FlashCardDTO.OppositeExpression.ToLower() == answer.ToLower() ||
                                _FlashCardDTO.OppositeExpression.ToLower().Contains(answer.ToLower()));
                    }

                    if (lbok)
                    {
                        success++;
                        Console.WriteLine($"\nCongratullations your score is {success} success and {fails} fails.");
                    } else
                    {
                        fails++;
                        Console.WriteLine($"\nSorry the right answer is {_FlashCardDTO.OppositeExpression}.");
                        Console.WriteLine($"\nYour score is {success} success and {fails} fails.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadLine();
                    }
                    
                    internalSaveStudy(lbok, answer);

                }
               
            }

            Console.WriteLine("#========= FINAL SCORE   ============#");
            Console.WriteLine($"#= Sucess : {success}   ============#");
            Console.WriteLine($"#= Fail s : {fails}     ============#");
            Console.WriteLine("#====================================#");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        private string internalGetText()
        {
            bool lbEmpty =false;
            string FieldValue = "";
            while (!lbEmpty)
            {
                Console.WriteLine("#= Please enter the answer OR empty TO EXIT: =======#");
                FieldValue =_util.GetStr(false, FieldValue);
                
                if (string.IsNullOrEmpty(FieldValue) || FieldValue.Trim() == "NONE")
                {
                    lbEmpty = true;
                    FieldValue = "";
                    break;
                } else
                {
                    break;
                }
            }

            return FieldValue;
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

    }
}


