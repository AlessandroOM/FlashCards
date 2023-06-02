using Database;
using flashcards.DTOs;
using flashcards.Models;
using flashcards.utils;
using flashcards.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace flashcards.Menus
{
    public class LessonsMenu
    {
        private StudyDTO _StudyDTO;
        private StackDTO _StackDTO;
        private FlashCardDTO _FlashCardDTO;

        private FlashCardRepository<FlashCardDTO> _flashRep;
        private StackRepository<StackDTO> _StackRep;
        private StudyRepository<StudyDTO> _StudyRep;

        private Mapper _mapper;
        private MyContext _db;
        
        private readonly Utilitys _util;

        private ConsoleKey _lastKey;

        public LessonsMenu(Mapper mapper, MyContext context)
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
                Menu menu = new Menu(new string[] { "0", "1", "2"},
                                    new string[] { "exit", 
                                                   "Lessons X Stack Report",
                                                   "Lessons x FlashCard Report",
                                                   });

                option = menu.MenuExecute("Lessons", "Input 0 to exit or your option"); 

                switch (option)
                {
                    case "0":
                        return;
                     
                    case "1":
                        StackReport();
                        break;

                     case "2":
                        FlashCardReport();
                        break;    
                                        
                    default:
                        break;
                }
            }
        }

        private void StackReport()
        {
            var consulta = _db.ConsultaSql.FromSqlRaw(
                                                   "SELECT " +
                                                   "  ST.STACKID," +
                                                   "  ST.FLASHCARDID," +
                                                   " S.NAME as StackName," +
                                                   " F.EXPRESSION as FlashCardQuestion," +
                                                   " F.OPPOSITEEXPRESSION as FlashCardAnswer," +
                                                   " ST.DATE as StudyDate," +
                                                   " ST.HAVESUCESS as Sucess" +
                                                   " FROM STUDIES ST" +
                                                   " INNER JOIN STACKS S" +
                                                   " ON S.ID=ST.STACKID" +
                                                   " INNER JOIN FLASHCARDS F" +
                                                   " ON F.ID=ST.FLASHCARDID" +
                                                   " ORDER BY ST.STACKID"
                                                    ).ToList();

            Console.WriteLine("#== Report Stacks x FlashCards =====#");
            foreach (var item in consulta)
            {
                Console.WriteLine("Stack : " + item.StackName + "  QUESTION :" + item.FlashCardQuestion + " ANSWER :" +
                                  item.FlashCardAnswer + " HAVE SUCCESS? " + item.Sucess + " DATE :" + item.StudyDate);
            }
            Console.WriteLine("#== End Report Stacks x FlashCards =====#");
            Console.WriteLine("#== Press any key to continue =====#");
            Console.ReadKey();


            
        }

        private void FlashCardReport()
        {
            var consulta = _db.ConsultaSql.FromSqlRaw(
                                                   "SELECT " +
                                                   "  ST.STACKID," +
                                                   "  ST.FLASHCARDID," +
                                                   " S.NAME as StackName," +
                                                   " F.EXPRESSION as FlashCardQuestion," +
                                                   " F.OPPOSITEEXPRESSION as FlashCardAnswer," +
                                                   " ST.DATE as StudyDate," +
                                                   " ST.HAVESUCESS as Sucess" +
                                                   " FROM STUDIES ST" +
                                                   " INNER JOIN STACKS S" +
                                                   " ON S.ID=ST.STACKID" +
                                                   " INNER JOIN FLASHCARDS F" +
                                                   " ON F.ID=ST.FLASHCARDID" +
                                                   " ORDER BY ST.FLASHCARDID"
                                                    ).ToList();

            Console.WriteLine("#== Report FlashCards x Stacks =====#");
            foreach (var item in consulta)
            {
                Console.WriteLine("  QUESTION :" + item.FlashCardQuestion + " ANSWER :" +
                                  item.FlashCardAnswer + " HAVE SUCCESS? " + item.Sucess +
                                   " DATE :" + item.StudyDate + " Stack : " + item.StackName);
            }
            Console.WriteLine("#== End Report FlashCards x Stacks =====#");
            Console.WriteLine("#== Press any key to continue =====#");
            Console.ReadKey();


            
        }

    }
}


