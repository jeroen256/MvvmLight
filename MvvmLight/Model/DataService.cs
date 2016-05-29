using System;

namespace MvvmLight.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);

        }

        public void GetHtml(Action<String, Exception> callback)
        {
            // Use this to connect to the actual data service

            string html = "Welcome to <b>MVVM</b> Light";
            callback(html, null);
        }
    }
}