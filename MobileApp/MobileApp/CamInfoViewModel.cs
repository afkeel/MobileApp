using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp
{
    public class CamInfoViewModel
    {
        public ObservableCollection<CamInfoModel> CamInfo { get; set; }
        public ICommand LoadDataCommand { protected set; get; }
        public CamInfoViewModel()
        {
            CamInfo = new ObservableCollection<CamInfoModel>();
            LoadDataCommand = new Command(LoadData);
        }
        private async void LoadData()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Constants.DemoMacroscop)
            };
            string userName = "root";
            string passwd = "";
            byte[] authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(authToken));
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress.AbsoluteUri);
                if (response.IsSuccessStatusCode)
                {
                    // работает только через WiFi!???
                    string content = await response.Content.ReadAsStringAsync();
                    JObject o = JObject.Parse(content);
                    JToken str = o["Channels"];
                    List<CamInfoModel> rep = JsonConvert.DeserializeObject<List<CamInfoModel>>(str.ToString());
                    if (CamInfo.Count != 0)
                    {
                        CamInfo.Clear();
                    }
                    foreach (CamInfoModel item in rep)
                    {
                        CamInfo.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("\tERROR {0}", ex.Message, "OK");
            }
        }
    }
}
