using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace LearnCode.Client.Util
{
    public class ViewModelBase : INotifyPropertyChanged, IChangeTracking, INotifyDataErrorInfo, IDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private object setPropertyLock = new object();

        #region INotifyPropertyChanged

        protected virtual void WhenProperty(string propertyName) { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, bool isChange, [CallerMemberName]string propertyName = null)
        {
            bool changing = false;

            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                if (Monitor.TryEnter(setPropertyLock))
                {
                    try
                    {
                        changing = true;
                        WhenProperty(propertyName);
                    }
                    finally
                    {
                        try
                        {
                            OnPropertyChanged(propertyName);
                        }
                        finally
                        {
                            Monitor.Exit(setPropertyLock);
                        }
                    }
                    if (isChange)
                        IsChanged = true;
                }

                IsChanged = changing;
                return true;
            }

            return false;
        }

        #endregion

        #region IChangeTracking

        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        #endregion

        #region Command

        protected T GetOrSetProperty<T>(ref T field, Func<T> valueIfNull) where T : class
        {
            LazyInitializer.EnsureInitialized(ref field, valueIfNull);
            return field;
        }

        #endregion

        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        public bool HasErrors => _errors.Count > 0;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.Values;
            }

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        protected void AddError(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }

        protected void AddErrors(string propertyName, IList<string> errors)
        {
            var changed = false;
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
            }

            foreach (var error in errors)
            {
                if (_errors[propertyName].Contains(error))
                    continue;

                _errors[propertyName].Add(error);
                changed = true;
            }

            if (changed)
                OnErrorChanged(propertyName);
        }

        protected void ClearErrors(string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                _errors.Clear();
            else
                _errors.Remove(propertyName);

            OnErrorChanged(propertyName);
        }

        #endregion

        #region IDataErrorInfo

        public string this[string properyName] => string.Join(Environment.NewLine, _errors.Where(p => string.Equals(p.Key, properyName)).OrderBy(p => p.Key).Select(p => p.Value));

        public string Error => string.Join(Environment.NewLine, _errors.Where(p => string.IsNullOrEmpty(p.Key)).OrderBy(p => p.Key).Select(p => p.Value));

        #endregion
    }
}
