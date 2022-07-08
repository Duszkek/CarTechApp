using CarTechApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using RestSharp;


namespace CarTechApp.Services
{
    public class MicroserviceAPI : IMicroserviceAPI
    {
        //string base_url = "https://localhost:5216/api/Cars/";
        public async Task<List<CarProp>> getData(string model, int bywhat)
        {
            //'http://localhost:5216/api/Cars?searching=Opel&bywhat=1

            //string url = "http://127.0.0.1:5216/";

            //string url = "https://api.sampleapis.com/";
            //string url = base_url + model;
            //var client = new RestClient(url);
            //var client1 = new RestClient(url1);
            //var request = new RestRequest("api/Cars", Method.Get);
            //var request1 = new RestRequest("beers/ale", Method.Get);
            //request.AddParameter("id", "1");
            //client.BaseAddress = new Uri("https://api.sampleapis.com/beers/ale");
            try
            {
                string url = "";
                if (model == null)
                {
                    url = "https://carsservice.conveyor.cloud/api/Cars?byWhat="+bywhat;
                }
                else
                {
                    url = "https://carsservice.conveyor.cloud/api/Cars?searching="+model+"&bywhat="+bywhat;
                }
                HttpClient client = new HttpClient();
                //var response = await client.ExecuteGetAsync(request);
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = response.Content;
                //var responsed = JsonConvert.DeserializeObject<List<Place>>(content);
                var responseAsString = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<List<CarProp>>(responseAsString);
                return json;

            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task saveData(CarProp car, string dateasstring, int isworking)
        {
            try
            {
                string url = "";
                if (dateasstring == null)
                {
                    return;
                }
                else
                {
                    dateasstring = dateasstring+" 12:00:00";
                    car.actualDate = car.nextDate;
                    car.nextDate = Convert.ToDateTime(dateasstring);
                    if(isworking == 1)
                    {
                        car.isWorking = true; 
                    }
                    else
                    {
                        car.isWorking = false;
                    }
                    
                    var stringContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
                    url = "https://carsservice.conveyor.cloud/api/Cars/api/Cars/Update";
                    HttpClient client = new HttpClient();
                    //var response = await client.ExecuteGetAsync(request);
                    var response = await client.PostAsync(url, stringContent);
                    return;
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
    }

}

