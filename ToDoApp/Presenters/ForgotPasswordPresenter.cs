using ToDoApp.Activities.Interfaces;

namespace ToDoApp.Presenters
{
    public class ForgotPasswordPresenter : BasePresenter
    {
        private readonly IForgotPasswordView _view;

        public ForgotPasswordPresenter(IForgotPasswordView view)
        {
            _view = view;
        }

        public void SendLinkRequest()
        {
            var email = _view.GetEmail();

            if (FindEmailInDatabase(email))
            {
                SendMessage(email);
                _view.ShowSnackBar("Message will be sended on your E-Mail");
            }
            else _view.ShowSnackBar("User with that E-Mail was not found");
        }

        private void SendMessage(string email)
        {

        }

        private bool FindEmailInDatabase(string email)
        {
            var user = UserService.GetUser(email);
            return user != null;
        }
    }
}