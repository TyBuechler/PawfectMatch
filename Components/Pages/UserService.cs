public class UserService
{
    public event Action OnChange;

    private bool _isLoggedIn;
    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            _isLoggedIn = value;
            NotifyStateChanged();
        }
    }

    private string _username;
    public string Username
    {
        get => _username;
        private set
        {
            _username = value;
            NotifyStateChanged();
        }
    }

    public void Login(string username)
    {
        Username = username;
        IsLoggedIn = true;
    }

    public void Logout()
    {
        Username = string.Empty;
        IsLoggedIn = false;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
