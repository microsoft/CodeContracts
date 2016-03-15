namespace System.ComponentModel
{
   using System;
   using System.Diagnostics.Contracts;

   public class PropertyChangedEventArgs : EventArgs
   {
       public PropertyChangedEventArgs(string propertyName)
       {
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
               return default(string);
           }
        }
    }
}
