using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace ThermostatDotNet.Runner.ViewModels
{
    /// <summary>
    /// ViewModel base class
    /// </summary>
    public abstract class BaseViewModel : BaseStaticViewModel
    {
        //protected readonly MsgHelper msgHelper;
        protected readonly NavigationManager navigationManager;
        protected bool disposedValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msgHelper"></param>
        /// <param name="navigationManager"></param>
        /// <param name="authenticationStateProvider"></param>
        public BaseViewModel(
                            //MsgHelper msgHelper,
                            NavigationManager navigationManager
                            )
        {
            //this.msgHelper = msgHelper;
            this.navigationManager = navigationManager;
        }

        #region ## Methods ##

        /// <summary>
        /// Construct an API object with a base object and a dictionary of field names to values. Supports nested objects (1 level)
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        protected static T GetObjectFromDatagridValues<T>(T dataItem, IDictionary<string, object> newValue)
        {
            foreach ((var key, var value) in newValue) {
                if (key.Contains(".")) {
                    if (typeof(T).Name.ToLowerInvariant() == key.Split('.')[0].ToLowerInvariant())
                        typeof(T).GetProperty(key.Split('.')[1]).SetValue(dataItem, value);
                }
                else
                    typeof(T).GetProperty(key)?.SetValue(dataItem, value);
            }
            return dataItem;
        }
        #endregion
    }
}