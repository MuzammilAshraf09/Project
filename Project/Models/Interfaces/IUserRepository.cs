using Project.Models.Entities;

namespace Project.Models.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        List<User> GetAll(); 
        void Update(User user);
        void Delete(int id);
        void Add(User user);
    }
}
