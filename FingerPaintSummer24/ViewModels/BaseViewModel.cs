using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FingerPaintSummer24.ViewModels
{
    /// <summary>
    /// Base class for all ViewModels in the application.
    /// Implements INotifyPropertyChanged for property change notifications.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event that is raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Dictionary to store computed properties' values.
        /// </summary>
        private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        protected T GetProperty<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            if (_propertyValues.TryGetValue(propertyName, out object value))
                return (T)value;

            return default;
        }

        /// <summary>
        /// Sets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="value">The new value of the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>True if the value was changed, false otherwise.</returns>
        protected bool SetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            if (Equals(value, GetProperty<T>(propertyName)))
                return false;

            _propertyValues[propertyName] = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the PropertyChanged event for a specific property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Creates a new Command that can always execute.
        /// </summary>
        /// <param name="execute">The action to execute.</param>
        /// <returns>A new Command instance.</returns>
        protected Command CreateCommand(Action execute)
        {
            return new Command(execute);
        }

        /// <summary>
        /// Creates a new Command that can always execute, with a parameter.
        /// </summary>
        /// <typeparam name="T">The type of the command parameter.</typeparam>
        /// <param name="execute">The action to execute.</param>
        /// <returns>A new Command instance.</returns>
        protected Command<T> CreateCommand<T>(Action<T> execute)
        {
            return new Command<T>(execute);
        }

        /// <summary>
        /// Creates a new Command with a predicate to determine when it can execute.
        /// </summary>
        /// <param name="execute">The action to execute.</param>
        /// <param name="canExecute">The predicate to determine if the command can execute.</param>
        /// <returns>A new Command instance.</returns>
        protected Command CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new Command(execute, canExecute);
        }

        /// <summary>
        /// Creates a new Command with a parameter and a predicate to determine when it can execute.
        /// </summary>
        /// <typeparam name="T">The type of the command parameter.</typeparam>
        /// <param name="execute">The action to execute.</param>
        /// <param name="canExecute">The predicate to determine if the command can execute.</param>
        /// <returns>A new Command instance.</returns>
        protected Command<T> CreateCommand<T>(Action<T> execute, Func<T, bool> canExecute)
        {
            return new Command<T>(execute, canExecute);
        }
    }
}
