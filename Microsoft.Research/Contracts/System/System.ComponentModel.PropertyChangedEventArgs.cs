namespace System.ComponentModel
{
   using System;
   using System.Diagnostics.Contracts;

   public class PropertyChangedEventArgs : EventArgs
   {
       public PropertyChangedEventArgs(string propertyName)
       {
           Contract.Requires(!string.IsNullOrEmpty(Contract.Result<string>()));
           Contract.Ensures(this.PropertyName == propertyName);
       }

#if SILVERLIGHT
       public string PropertyName
#else
       public virtual string PropertyName
#endif
       {
           get
           {
               Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

               return default(string);
           }
        }
    }
}
