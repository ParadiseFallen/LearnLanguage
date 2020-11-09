using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Models.Services.API
{
    public static class APIEndpoints
    {
        public static string APIEndpoint { get => "/api"; }

        #region Account
        public static string AccountEndpoint { get; } = $"{APIEndpoint}/account";

        public static string LoginAccountEndpoint { get;  } = $"{AccountEndpoint}/login";
        public static string LogoutAccountEndpoint { get; } = $"{AccountEndpoint}/logout";
        public static string RegisterAccountEndpoint { get; } = $"{AccountEndpoint}/register";
        #endregion

        #region Phrase
        public static string PhrasesEndpoint { get; } = $"{APIEndpoint}/phrases";

        public static string CreatePhraseEndpoint { get; } = $"{PhrasesEndpoint}";
        public static string CompileDeletePhraseEndpoint(int id)  => $"{PhrasesEndpoint}/{id}";
        public static string UpdatePhraseEndpoint { get; } = $"{PhrasesEndpoint}";

        #endregion

        #region Translate
        public static string TranslationsEndpoint { get; } = $"{APIEndpoint}/translations";

        public static string CompileGetTranslationsEndpoint(CultureInfo from, CultureInfo to,int count = 1) => $"{TranslationsEndpoint}/{from.Name}/{to.Name}/{count}";
        public static string CreateTranslationEndpoint { get; } = $"{TranslationsEndpoint}";
        public static string UpdateTranslationEndpoint { get; } = $"{TranslationsEndpoint}";
        public static string CompileDeleteTranslationEndpoint(int id) => $"{TranslationsEndpoint}/{id}";

        #endregion
    }
}
