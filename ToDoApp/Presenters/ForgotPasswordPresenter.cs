using ToDoApp.Interfaces.Views;

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
                _view.ShowSnackBar(Resource.String.messageWillBeSended);
            }
            else _view.ShowSnackBar(Resource.String.userWithThatEmailNotFound);
        }

        private void SendMessage(string email)
        {
            // Not implemented, because need backend
        }

        private bool FindEmailInDatabase(string email)
        {
            var user = UserService.GetUser(email);
            return user != null;
        }
    }
}