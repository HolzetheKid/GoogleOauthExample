using GoogleOAuthExample.Services;
using Xamarin.Forms;

namespace GoogleOAuthExample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        private string currentCSRFToken;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var ga = new GoogleAuthentication();

            ga.LogIn();
        }
    }
}