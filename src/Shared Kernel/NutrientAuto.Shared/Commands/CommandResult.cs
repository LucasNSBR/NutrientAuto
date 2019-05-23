using NutrientAuto.Shared.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace NutrientAuto.Shared.Commands
{
    public class CommandResult
    {
        public bool Success { get; }
        public object Data { get; }

        private readonly List<DomainNotification> _notifications;
        public IReadOnlyList<DomainNotification> Notifications
        {
            get
            {
                return _notifications;
            }
        }

        private CommandResult(List<DomainNotification> notifications)
        {
            Success = false;
            _notifications = notifications ?? new List<DomainNotification>();
        }

        private CommandResult(object data)
        {
            Success = true;
            Data = data;
            _notifications = new List<DomainNotification>();
        }

        public static CommandResult Ok(object data = null)
        {
            return new CommandResult(data);
        }

        public static CommandResult Failure(List<DomainNotification> notifications)
        {
            return new CommandResult(notifications);
        }

        public static CommandResult Failure(params DomainNotification[] notifications)
        {
            return new CommandResult(notifications.ToList());
        }

        public static CommandResult Failure(string title, string description)
        {
            return new CommandResult(new List<DomainNotification>
            {
                new DomainNotification(title, description)
            });
        }
    }
}
