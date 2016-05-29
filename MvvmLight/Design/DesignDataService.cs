using System;
using MvvmLight.Model;

namespace MvvmLight.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }

        public void GetHtml(Action<string, Exception> callback)
        {
            callback("html <b>design</b>", null);
        }
    }
}