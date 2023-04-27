using Auditore.Services.Interfaces;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services
{
    public class NotificationService : INotificationService
    {
        public async Task EndChrono()
        {
            var toast = Toast.Make("Ha finalizado el cronometro", ToastDuration.Long);
            await toast.Show();
        }
    }
}
