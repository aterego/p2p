using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using p2p.Communication;
using p2p.Helpers;
using p2p.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Services
{
    public class BackendProxy : IBackendProxy
    {

        /// <summary>
        /// Authenticate on backend service.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public Task<SessionToken> LoginAsync(string username, string password)
        {
            return Task<SessionToken>.Run(async () =>
            {
                if (!Validators.ConnectionAvailableAsync().Result) return new SessionToken();

                var url = string.Format("{0}/api/login", Validators.ServerBaseUrl);
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JObject jsonObject = new JObject();
                //jsonObject.Add("email", username);
                //jsonObject.Add("password", password);
                //jsonObject.Add("email", "mytest8@test.com");
                //jsonObject.Add("password", "123456");
                jsonObject.Add("email", "profi@test.com");
                jsonObject.Add("password", "1234567");

                string jsonData = JsonConvert.SerializeObject(jsonObject); //@"{""email"" : ""mytest8@test.com"", ""password"" : ""123456""}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
               

                string returnVal = "";
                try
                {
                    HttpResponseMessage response = await client.PostAsync("", content);

                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        returnVal = await response.Content.ReadAsStringAsync();
                        JObject s = JObject.Parse(returnVal);
                        
                        return new SessionToken
                        {
                            AccessToken = (string)s["accessToken"],
                            RefreshToken = (string)s["refreshToken"],
                            Expiration = (long)s["expiration"],
                            //Username = "mytest8@test.com", // username
                            Username = "profi@test.com",
                            Role = (string)s["role"]
                        };
                        
                    }
                    else
                    {
                        return new SessionToken();
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    returnVal = null;
                }

                return new SessionToken();
                
            });
        }

        public Task<SessionToken> RefreshTokenAsync(string refreshToken, string username)
        {
            return Task<SessionToken>.Run(async () =>
            {
                var url = string.Format("{0}/api/token/refresh", Validators.ServerBaseUrl);
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JObject jsonObject = new JObject();
                jsonObject.Add("token", refreshToken);
                jsonObject.Add("userEmail", username);

                string jsonData = JsonConvert.SerializeObject(jsonObject);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                string returnVal = "";
                try
                {
                    HttpResponseMessage response = await client.PostAsync("", content);
                    //response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        returnVal = await response.Content.ReadAsStringAsync();
                        JObject s = JObject.Parse(returnVal);
                        
                        return new SessionToken
                        {
                            AccessToken = (string)s["accessToken"],
                            RefreshToken = (string)s["refreshToken"],
                            Expiration = (long)s["expiration"],
                            Username = username,
                            Role = (string)s["role"]
                        };

                    }
                    else
                    {
                        returnVal = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine("response : " + returnVal);
                    }
                }
                catch (Exception ex)
                {
                    
                    var msg = ex.Message;
                    returnVal = null;
                }

                return new SessionToken();

            });
        }

        public Task<string> GetContentCustomerAsync(string accessToken, Test test)
        {
            return Task<string>.Run(async () =>
            {
                var url = string.Format("{0}/api/protectedforcustomers", Validators.ServerBaseUrl);
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                string authorizationValue = "Bearer " + accessToken;
                client.DefaultRequestHeaders.Add("Authorization", authorizationValue);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var returnVal = "";
                try
                {
                    HttpResponseMessage response = await client.GetAsync("");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        returnVal = await response.Content.ReadAsStringAsync();


                        return returnVal + test.Clock.ToString();

                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    returnVal = null;
                }

                return returnVal;
            });

        }

        public Task<string> GetContentProfiAsync(string accessToken, Test test)
        {
            return Task<string>.Run(async () =>
            {
                var url = string.Format("{0}/api/protectedforprofi", Validators.ServerBaseUrl);
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                string authorizationValue = "Bearer " + accessToken;
                client.DefaultRequestHeaders.Add("Authorization", authorizationValue);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var returnVal = "";
                try
                {
                    HttpResponseMessage response = await client.GetAsync("");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        returnVal = await response.Content.ReadAsStringAsync();


                        return returnVal + test.Clock.ToString();

                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    returnVal = null;
                }

                return returnVal;
            });

        }

        public async Task<RegistrationResponse> RegisterCustomerAsync(string[] arr)
        {
            // return Task<RegistrationResponse>.Run(async () =>
            // {
                string returnVal = "Something went wrong.";
                bool connection = await Validators.ConnectionAvailableAsync();
                if (!connection)
                    return new RegistrationResponse(false, "No Connection", null) ;

                var url = string.Format("{0}/api/usersCustomer", Validators.ServerBaseUrl);
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JObject jsonObject = new JObject();
                jsonObject.Add("FirstName", arr[0]);
                jsonObject.Add("LastName", arr[1]);
                jsonObject.Add("Gender", arr[2]);
                jsonObject.Add("BirthDate", arr[3]);
                jsonObject.Add("PassportId", arr[4]);
                jsonObject.Add("Phone1", arr[5]);
                jsonObject.Add("Phone2", arr[6]);
                jsonObject.Add("Company", arr[7]);
                jsonObject.Add("Address1", arr[8]);
                jsonObject.Add("Address2", arr[9]);
                jsonObject.Add("Memo", arr[10]);

                JObject credObject = new JObject();
                credObject.Add("password", arr[11]);
                credObject.Add("email", arr[12]);
                jsonObject.Add("UserCredentials", credObject);

                //***AVA** temp
                jsonObject.Add("LastIP", "127.0.0.1");
                //jsonObject.Add("LastIP", arr[13]);

                string jsonData = JsonConvert.SerializeObject(jsonObject); //@"{""email"" : ""mytest8@test.com"", ""password"" : ""123456""}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("", content);

                    //response.EnsureSuccessStatusCode();
                    returnVal = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        
                        JObject s = JObject.Parse(returnVal);

                        return new RegistrationResponse(true, "Registration complete! Thank you!", null); 

                    }
                    else
                    {
                     if(returnVal.Contains("<html")) returnVal = "Something went wrong.";

                     return new RegistrationResponse(false, returnVal, null);
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    returnVal = msg;
                }

                return new RegistrationResponse(false, returnVal, null); 

            //});
        }

        public async Task<RegistrationResponse> RegisterProfiAsync(string[] arr)
        {
            // return Task<RegistrationResponse>.Run(async () =>
            // {
            string returnVal = "Something went wrong.";
            bool connection = await Validators.ConnectionAvailableAsync();
            if (!connection)
                return new RegistrationResponse(false, "No Connection", null);

            var url = string.Format("{0}/api/usersProfi", Validators.ServerBaseUrl);
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JObject jsonObject = new JObject();
            jsonObject.Add("FirstName", arr[0]);
            jsonObject.Add("LastName", arr[1]);
            jsonObject.Add("Gender", arr[2]);
            jsonObject.Add("BirthDate", arr[3]);
            jsonObject.Add("PassportId", arr[4]);
            jsonObject.Add("Phone1", arr[5]);
            jsonObject.Add("Phone2", arr[6]);
            jsonObject.Add("Company", arr[7]);
            jsonObject.Add("Address1", arr[8]);
            jsonObject.Add("Address2", arr[9]);
            jsonObject.Add("Memo", arr[10]);
            jsonObject.Add("LicenceId", arr[11]);

            JObject credObject = new JObject();
            credObject.Add("password", arr[12]);
            credObject.Add("email", arr[13]);
            jsonObject.Add("UserCredentials", credObject);

            //***AVA** temp
            jsonObject.Add("LastIP", "127.0.0.1");
            //jsonObject.Add("LastIP", arr[13]);

            string jsonData = JsonConvert.SerializeObject(jsonObject); //@"{""email"" : ""mytest8@test.com"", ""password"" : ""123456""}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("", content);

                //response.EnsureSuccessStatusCode();
                returnVal = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    JObject s = JObject.Parse(returnVal);

                    return new RegistrationResponse(true, "Registration complete! Thank you!", null);

                }
                else
                {
                    if (returnVal.Contains("<html")) returnVal = "Something went wrong.";

                    return new RegistrationResponse(false, returnVal, null);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                returnVal = msg;
            }

            return new RegistrationResponse(false, returnVal, null);

            //});
        }


    }
}
