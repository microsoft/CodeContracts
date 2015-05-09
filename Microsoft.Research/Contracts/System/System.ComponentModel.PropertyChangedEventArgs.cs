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

       public virtual string PropertyName
       {
           get
           {
               Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

               return default(string);
           }
        }
    }
}
