using flashcards.DTOs;
namespace flashcards.Interface
{
    public interface IBaseRepository<T> where T : BaseDto
    {
        public bool AddModel(T model);

        public bool FindById(int id);

        public bool FindByName(string name);

        public void Delete(int id);

        public void Update(int id, T model);

        public T GetModelbyId(int id);

        public T GetModelbyName(string name);

        public IEnumerable<T> GetAll();

    }
}