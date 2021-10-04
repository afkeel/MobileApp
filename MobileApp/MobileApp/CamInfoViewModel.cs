using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
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
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress.AbsoluteUri);
                if (response.IsSuccessStatusCode)
                {
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



        //protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        //{
        //    if (!Equals(field, newValue))
        //    {
        //        field = newValue;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //        return true;
        //    }

        //    return false;
        //}

        //private string name;
        //public string Name { get => name; set => SetProperty(ref name, value); }
        //private string sound;
        //public string Sound { get => sound; set => SetProperty(ref sound, value); }
        //private string serverName;
        //public string ServerName { get => serverName; set => SetProperty(ref serverName, value); }
        //private string name;
        //public string Name
        //{
        //    get { return name; }
        //    private set
        //    {
        //        name = value;
        //        OnPropertyChanged("Name");
        //    }
        //}
        //private bool sound;
        //public bool Sound
        //{
        //    get { return sound; }
        //    private set
        //    {
        //        sound = value;
        //        OnPropertyChanged("Sound");
        //    }
        //}
        //private string serverName;
        //public string ServerName
        //{
        //    get { return serverName; }
        //    private set
        //    {
        //        serverName = value;
        //        OnPropertyChanged("ServerName");
        //    }
        //}
        //public void OnPropertyChanged(string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //public ObservableCollection<CamInfoModel> CamInfo 
        //{
        //    get => camInfo;
        //    set
        //    {
        //        camInfo = value;
        //        OnPropertyChanged();
        //    }
        //}
        //CamInfoModel model;
        //public CamInfoModel Model
        //{
        //    get { return model; }
        //    set
        //    {
        //        if (model != value)
        //        {
        //            model = new CamInfoModel()
        //            {
        //                Name = value.Name,
        //                IsSoundOn = value.IsSoundOn,
        //                AttachedToServer = value.AttachedToServer
        //            };
        //            OnPropertyChanged("SelectedFriend");

        //        }
        //    }
        //}
        //private void OnPropertyChanged()
        //{
        //    //if (PropertyChanged != null)
        //    //    PropertyChanged(this, new PropertyChangedEventArgs(v));
        //}
    }
}
