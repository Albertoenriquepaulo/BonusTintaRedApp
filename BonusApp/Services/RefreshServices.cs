using System;

namespace BonusApp.Services
{
    public class RefreshServices
    {
        public event Action RefreshRequested;

        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}