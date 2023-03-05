using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonControl
{
    public class CustomeEvents
    {
        private bool _refesh = false;
        public bool Refresh
        {
            get => _refesh;
            set
            {
                if (value != _refesh)
                {
                    _refesh = value;

                    OnRefreshChange(_refesh);
                }
            }
        }

        private event EventHandler<StatusChangedEventArgs> _refreshChangedEvent;
        public event EventHandler<StatusChangedEventArgs> RefreshChangedEvent
        {
            add
            {
                _refreshChangedEvent += value;
            }
            remove
            {
                _refreshChangedEvent -= value;
            }
        }

        void OnRefreshChange(bool value)
        {
            _refreshChangedEvent?.Invoke(this, new StatusChangedEventArgs(value));
        }
    }
}

public class StatusChangedEventArgs : EventArgs
{
    public bool NewStatus { get; set; }

    public StatusChangedEventArgs(bool newStatus)
    {
        NewStatus = newStatus;
    }
}

public class ValueChangedEventArgs : EventArgs
{
    public string NewValue { get; set; }

    public ValueChangedEventArgs(string newValue)
    {
        NewValue = newValue;
    }
}
