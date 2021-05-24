using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using OnlineStore.Entities;

namespace OnlineStore.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        string baseUrl = ConfigurationManager.AppSettings.Get("virtualPath").ToString();
        HttpRequest request { get; set; }
        private int cacheTimeoutInMinutes = 30;
        public Result RegisterCustomer(UserSession user)
        {
            Result result = new Result();
            Customer customer = new Customer()
            {
                CustomerName = user.Name,
                CustomerEmail = user.Email,
                CustomerMobil = user.Mobile
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PostAsJsonAsync("api/Customer/RegisterCustomer/", customer).Result;
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<Result>(response);
                    if (result.Status == ResultStatus.Ok)
                    {
                       
                        Customer customerR = JsonConvert.DeserializeObject<Customer>(result.ObjectResult.ToString());
                        if (customerR != null)
                        {
                            if (ValidateUser(customerR.CustomerEmail, ""))
                            {
                                FormsAuthentication.SetAuthCookie(customerR.CustomerEmail, false);
                                result.Status = ResultStatus.Ok;
                            }
                        }
                        
                    }
                }
            }
            return result;
        }
        public override bool ValidateUser(string username, string password)
        {
           
            var result = false;
            UserSession userF = new UserSession()
            {
                Email = username,
                Password = password
            };
            Result resultApi = new Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PostAsJsonAsync("api/Security/GetUserByUserName/", userF).Result;
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    resultApi = JsonConvert.DeserializeObject<Result>(response);
                    if (resultApi.Status == ResultStatus.Ok)
                    {
                        userF = JsonConvert.DeserializeObject<UserSession>(resultApi.ObjectResult.ToString());
                    }
                }
            }

            if (resultApi.Status == ResultStatus.Error)
            {
                throw new Exception(resultApi.Message);
            }
            if (string.IsNullOrEmpty(password) && userF != null)
            {

                if (resultApi.Status == ResultStatus.Error)
                {
                    throw new Exception(resultApi.Message);
                }
                UserSession user = GetUsuarioLogin(userF);
                var userSession = GetUserSession(user);

                if (userSession != null)
                {

                    var membershipUser = new CustomMembershipUser(userSession);
                    var cacheKey = string.Format("UserData_{0}", username);
                    HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

                    result = true;
                }

            }
            else
            {
                UserSession userLog = new UserSession()
                {
                    Email = username,
                    Password = password
                };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = client.PostAsJsonAsync("api/Security/GetUserByUserName/", userLog).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var response = res.Content.ReadAsStringAsync().Result;
                        resultApi = JsonConvert.DeserializeObject<Result>(response);
                        if (resultApi.Status == ResultStatus.Ok)
                        {
                            userLog = JsonConvert.DeserializeObject<UserSession>(resultApi.ObjectResult.ToString());
                        }

                    }
                }
                result = userLog != null;
                if (result)
                {

                    UserSession user = GetUsuarioLogin(userLog);
                    var userSession = GetUserSession(user);
                    if (userSession != null)
                    {
                        System.Web.HttpContext.Current.Session["CustomerId"] = userSession.CustomerId;
                    }
                }


            }
            return result;
        }

        public UserSession GetUsuarioLogin(UserSession user)
        {
            UserSession usuarioLogin = new UserSession()
            {
                CustomerId = user.CustomerId,
                Name = user.Name,
                Email = user.Email,
                Mobile = user.Mobile
            };

            return usuarioLogin;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            UserSession user = default(UserSession);

            try
            {
                var cacheKey = string.Format("UserData_{0}", username);

                if (HttpRuntime.Cache[cacheKey] != null)
                {
                    return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];
                }
                else
                {
                    UserSession userF = new UserSession()
                    {
                        Email = username
                    };
                    Result resultApi = new Result();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage res = client.PostAsJsonAsync("api/Secutity/GetUserByUserName/", userF).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            var response = res.Content.ReadAsStringAsync().Result;
                            resultApi = JsonConvert.DeserializeObject<Result>(response);
                            if (resultApi.Status == ResultStatus.Ok)
                            {
                                userF = JsonConvert.DeserializeObject<UserSession>(resultApi.ObjectResult.ToString());
                            }
                        }
                    }
                    if (userF != null)
                    {
                        user = GetUsuarioLogin(userF);

                        UserSession userSession = GetUserSession(user);
                        if (userSession != null)
                        {
                            var membershipUser = new CustomMembershipUser(userSession);

                            HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

                            return membershipUser;
                        }
                    }

                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserSession GetUserSession(UserSession user)
        {

            UserSession userSession = default(UserSession);

            if (user != null)
            {

                userSession = new UserSession()
                {
                    CustomerId = user.CustomerId,
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile
                };

            }

            return userSession;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }




    }
}
