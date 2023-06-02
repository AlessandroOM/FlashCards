using flashcards.Interface;
using flashcards.DTOs;
using Database;
using flashcards.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace flashcards.Repository
{

public class StackRepository<T> : IBaseRepository<T> where T : StackDTO
    {
        protected readonly MyContext _context;
        private DbSet<Stacks> _dataset;

        private Mapper _mapper;
        public StackRepository(MyContext context, Mapper mapper)
        {
            _context = context;
            _dataset = _context.Set<Stacks>();
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
        var entity = _mapper.Map<StackDTO,Stacks>(model); 
        _context.Set<Stacks>().Add(entity);
        var emsg = applyChanges();
        return (string.IsNullOrEmpty(emsg));
    }

    public void Delete(int id)
    {
        var item = _context.Set<Stacks>().FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            _context.Set<Stacks>().Remove(item);
            var emsg = applyChanges();
         }
    }

    public bool FindById(int id)
    {
        throw new NotImplementedException();
    }

    public bool FindByName(string name)
    {
        var item = _context.Set<Stacks>().FirstOrDefault(i => i.Name == name);
        return (item != null);
    }

   
    public T GetModelbyName(string name)
     {
         var item = _context.Set<Stacks>()
                            .Include(x=>x.FlashCards)
                            .FirstOrDefault(i => i.Name == name);
         if (item == null)
         {
            return null;
         }

         return (T)_mapper.Map<Stacks, StackDTO>(item);
         
     }
     public T GetModelbyId(int id)
     {
        var item = _context.Set<Stacks>()
                           .Include(x=>x.FlashCards)
                           .FirstOrDefault(i => i.Id == id);
                             
         if (item == null)
         {
            return null;
         }

         return (T)_mapper.Map<Stacks, StackDTO>(item);
     }

    public IEnumerable<T> GetAll()
    {
         var list = _context.Set<Stacks>()    
                           .Include(x=>x.FlashCards)
                           .ToList();

        
         var olist = _mapper.Map<IEnumerable<T>>(list);
         return olist;
    }

    public void Update(int id, T model)
    {
         var item = _context.Set<Stacks>().FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            item.Name = model.Name;
            var emsg = applyChanges();
         }
    }
}
}
