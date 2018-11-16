using ToDoApp.Common.Models;
using ToDoApp.Common.Utils;
using ToDoApp.DAL.Repositories;

namespace ToDoApp.Common.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }

        public UserModel Login(string email, string password)
        {
            var user = Mapper.MapUser(_repository.GetUser(email));
            return string.Equals(user.Password, password) ? user : null;
        }

        public bool Register(UserModel model)
        {
            return _repository.CreateUser(Mapper.MapUser(model));
        }

        public UserModel GetUser(string email)
        {
            return Mapper.MapUser(_repository.GetUser(email));
        }

        public bool DeleteUser(UserModel user)
        {
            return _repository.DeleteUser(Mapper.MapUser(user));
        }

        public bool UpdateUser(UserModel model)
        {
            return _repository.UpdateUser(Mapper.MapUser(model));
        }
    }
}
