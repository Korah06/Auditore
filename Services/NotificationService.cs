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
        #region Desktop
        public async Task EndChrono()
        {
            var toast = Toast.Make("Ha finalizado el cronometro", ToastDuration.Long);
            await toast.Show();
        }

        public async Task EndRest()
        {
            var toast = Toast.Make("Ha finalizado el descanso", ToastDuration.Long);
            await toast.Show();
        }
        public async Task EndWorkTime()
        {
            var toast = Toast.Make("Ha finalizado el tiempo de trabajo", ToastDuration.Long);
            await toast.Show();
        }
        public async Task EndPomodoro()
        {
            var toast = Toast.Make("Ha finalizado el Pomodoro", ToastDuration.Long);
            await toast.Show();
        } 
        #endregion
    }
}
