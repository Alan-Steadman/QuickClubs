namespace QuickClubs.AdminUI.Common.Listeners;

public class NotFoundListener
{
    public Action OnNotFound { get; set; } = null!;

    public void NotifyNotFound()
    {
        if (NotifyNotFound != null)
        {
            OnNotFound.Invoke();
        }
    }
}
