using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Common.Models;
using ToDoApp.Interfaces.Views;

namespace ToDoApp.Presenters
{
    public class LoginPresenter : BasePresenter
    {
        private ILoginView _view;

        public LoginPresenter(ILoginView view)
        {
            _view = view;
        }

        public void DetachView()
        {
            _view = null;
        }

        public async void SendLogin()
        {
            _view.ShowProgressBar();

            var model = _view.GetData();
            UserModel user = null;

            await Task.Run(async () =>
            {
                Thread.Sleep(2500);
                user = Login(model.Email, model.Password);
            });

            if (user == null)
            {
                _view.ShowSnackBar(Resource.String.invalidPasswordOrEmail);
                _view.HideProgressBar();
                return;
            }

            User = user;
            var email = user.Email;
            SaveLoggedUser(email);

            _view.SendSuccess();
        }

        private UserModel Login(string email, string password)
        {
            return UserService.Login(email, password);
            
        }
    }
}