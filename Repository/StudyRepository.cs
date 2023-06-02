using flashcards.Interface;
using flashcards.DTOs;
using Database;
using flashcards.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace flashcards.Repository
{

public class StudyRepository<T> : IBaseRepository<T> where T : StudyDTO
    {
        protected readonly MyContext _context;
        private DbSet<Study> _dataset;

        private Mapper _mapper;
        public StudyRepository(MyContext context, Mapper mapper)
        {
            _context = context;
            _dataset = _context.Set<Study>();
            _mapper = mapper;
        }

    private string applyChanges()
    {
        try
        {
           
            _context.SaveChanges();
            return "";
        }
        catch (System.Exception e)
        {
            if (e.InnerException != null)
            {
                Console.WriteLine("Inner exception .....");
                Console.WriteLine(e.InnerException.Message);
                var list  = e.InnerException.Data;
                foreach (DictionaryEntry entry in list)
                {
                    Console.WriteLine(entry.Key + ": " + entry.Value);
                }
                //Console.WriteLine(" lc " + list.ToString());
            }
            
            return e.Message;
        }
    }
    public bool AddModel(T model)
    {
        var entity = _mapper.Map<StudyDTO,Study>(model); 
        //entity.StackId = model.StackId;
        //entity.FlashCardId = model.FlashCardId;
        // Console.WriteLine("apos set e antes de add");
         //Console.WriteLine($"entity StackID {entity.StackId}");
         //Console.WriteLine($"entity FlashCardID {entity.FlashCardId}");
        // Console.WriteLine($"model StackID {model.StackId}");
        // Console.WriteLine($"model FlashCardID {model.FlashCardId}");
        _context.Entry(entity).State = EntityState.Added;
        _context.Set<Study>().Add(entity);
        
        var emsg = applyChanges();
        // Console.WriteLine($"entity stcID {entity.StackId}");
        // Console.WriteLine($"entity flsID {entity.FlashCardId}");
        return (string.IsNullOrEmpty(emsg));
    }

    public void Delete(int id)
    {
        var item = _context.Set<Study>().FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            _context.Set<Study>().Remove(item);
            var emsg = applyChanges();
         }
    }

    public bool FindById(int id)
    {
        var item = _context.Set<Study>().FirstOrDefault(i => i.Id == id);
        return (item != null);
    }

    public bool FindByName(string expression)
    {
        throw new NotImplementedException();
    }

   
    public T GetModelbyName(string expression)
     {
         throw new NotImplementedException();
     }
     public T GetModelbyId(int id)
     {
        var item = _context.Set<Study>()
                           .Include(x=>x.Stack)
                           .Include(x=>x.FlashCard)
                           .First<Study>(m => m.Id == id);
         if (item == null)
         {
            return null;
         }

         return (T)_mapper.Map<Study, StudyDTO>(item);
     }

    public IEnumerable<T> GetAll()
    {
        var list = _context.Set<Study>()    //FlashCardDbx
                           .Include(x=>x.Stack)
                           .Include(x=>x.FlashCard)
                           .ToList();
         
        
         var olist = _mapper.Map<IEnumerable<T>>(list);
         return olist;
    }

    public void Update(int id, T model)
    {
         var item = _context.Set<Study>().FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            item.date = model.date;
            item.HaveSucess = model.HaveSucess;
            item.Response = model.Response;
            var emsg = applyChanges();
         }
    }
}
}
