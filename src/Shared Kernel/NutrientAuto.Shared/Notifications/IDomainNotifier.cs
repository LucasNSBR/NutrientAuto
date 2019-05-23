using System.Collections.Generic;

namespace NutrientAuto.Shared.Notifications
{
    public interface IDomainNotifier
    {
        IReadOnlyList<DomainNotification> GetNotifications();
        IDictionary<string, string> GetNotificationsAsDictionary();
        void AddNotification(string title, string description);
        void AddNotification(DomainNotification notification);
        bool HasNotifications();
        bool IsValid { get; }
    }
}
