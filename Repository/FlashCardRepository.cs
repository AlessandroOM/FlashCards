using flashcards.Interface;
using flashcards.DTOs;
using Database;
using flashcards.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace flashcards.Repository
{

public class FlashCardRepository<T> : IBaseRepository<T> where T : FlashCardDTO
    {
        protected readonly MyContext _context;
        private DbSet<FlashCardDb> _dataset;

        private Mapper _mapper;
        public FlashCardRepository(MyContext context, Mapper mapper)
        {
            _context = context;
            _dataset = _context.Set<FlashCardDb>();
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
            return e.Message;
        }
    }
    public bool AddModel(T model)
    {
        var entity = _mapper.Map<FlashCardDTO,FlashCardDb>(model); 
        _context.Set<FlashCardDb>().Add(entity);
        var emsg = applyChanges();
        return (string.IsNullOrEmpty(emsg));
    }

    public void Delete(int id)
    {
        var item = _context.Set<FlashCardDb>().FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            _context.Set<FlashCardDb>().Remove(item);
            var emsg = applyChanges();
         }
    }

    public bool FindById(int id)
    {
        throw new NotImplementedException();
    }

    public bool FindByName(string expression)
    {
        var item = _context.Set<FlashCardDb>().FirstOrDefault(i => i.Expression == expression);
        return (item != null);
    }

   
    public T GetModelbyName(string expression)
     {
         var item = _context.Set<FlashCardDb>().Include(x=>x.Stack).FirstOrDefault<FlashCardDb>(m => m.Expression == expression);
         if (item == null)
         {
            return null;
         }

         return (T)_mapper.Map<FlashCardDb, FlashCardDTO>(item);
         
     }
     public T GetModelbyId(int id)
     {
        var item = _context.Set<FlashCardDb>()
                           .Include(x=>x.Stack)
                           .First<FlashCardDb>(m => m.Id == id);
         if (item == null)
         {
            return null;
         }

         return (T)_mapper.Map<FlashCardDb, FlashCardDTO>(item);
     }

    public IEnumerable<T> GetAll()
    {
        var list = _context.Set<FlashCardDb>()    //FlashCardDbx
                           .Include(x=>x.Stack)
                           .ToList();
         
        
         var olist = _mapper.Map<IEnumerable<T>>(list);
         return olist;
    }

    public void Update(int id, T model)
    {
         var item = _context.Set<FlashCardDb>().FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            item.Expression = model.Expression;
            item.OppositeExpression = model.OppositeExpression;
            var emsg = applyChanges();
         }
    }
}
}
