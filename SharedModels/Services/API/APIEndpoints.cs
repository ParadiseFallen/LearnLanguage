using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Services.API
{
    public static class APIEndpoints
    {
        public static string APIEndpoint { get => "/api"; }

        #region Account
        public static string AccountEndpoint { get => $"{APIEndpoint}/account"; }
        public static string LoginAccountEndpoint { get => $"{AccountEndpoint}/login"; }
        public static string LogoutAccountEndpoint { get => $"{AccountEndpoint}/logout"; }
        public static string RegisterAccountEndpoint { get => $"{AccountEndpoint}/register"; }
        #endregion

        #region Phrase

        #endregion

        #region Translate

        #endregion
    }
}
