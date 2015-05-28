//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace UI.Mvc.Security
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Security;
    using Newtonsoft.Json;
    using UI.Mvc.Helpers;
    using UI.Mvc.Models;
    using WebMatrix.WebData;

    public class WebApiMembershipProvider : ExtendedMembershipProvider 
    {
        private string baseApiUrl;

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override bool EnablePasswordReset
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override bool EnablePasswordRetrieval
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override int MinRequiredPasswordLength
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override int PasswordAttemptWindow
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override string PasswordStrengthRegularExpression
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override bool RequiresUniqueEmail
        {
            get { throw new System.NotImplementedException(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override string ApplicationName
        {
            get { throw new System.NotImplementedException(); }

            set { throw new System.NotImplementedException(); }
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            var registrationInfo = values["RegistrationInfo"] as RegistrationInfo;
            using (var client = new HttpClient())
            {
                HttpClientHelper.AddJsonRequestAcceptHeader(client);
                var response = client.PostAsJsonAsync(this.baseApiUrl + "/account/register", registrationInfo).Result;

                var httpErrorMessage = new { message = string.Empty };
                var error = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, httpErrorMessage);

                try
                {
                    response.EnsureSuccessStatusCode();
                    return userName;
                }
                catch (Exception)
                {
                    throw new MembershipCreateUserException(error.message);
                }
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                HttpClientHelper.AddJsonRequestAcceptHeader(client);

                var response = client.PostAsJsonAsync(
                    this.baseApiUrl + "/account/login",
                    new { Username = username, Password = password }).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var status = response.StatusCode;
                    var error = JsonConvert.DeserializeAnonymousType(
                        response.Content.ReadAsStringAsync().Result,
                        new { Message = string.Empty });
                    throw new HttpException((int)status, error.Message);
                }

                var id = response.Content.ReadAsAsync<string>().Result;
                if (id != null)
                {
                    AuthenticatedSessionHelper.CurrentUserSessionKey = id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            throw new System.NotImplementedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            throw new System.NotImplementedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "We don't need to implement this method")]
        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteAccount(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new System.NotImplementedException();
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override DateTime GetCreateDate(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsConfirmed(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            this.baseApiUrl = ConfigurationManager.AppSettings["baseApiUrl"];   
            base.Initialize(name, config);
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}