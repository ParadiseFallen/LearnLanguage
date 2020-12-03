using System.Globalization;

namespace ApiServices.Endpoints
{
    public static class Api
    {
        public static string ApiBase => $"/api";

        public static class Account
        {
            public static string AccountBase => $"{ApiBase}/account";
            public static string Login => $"{AccountBase}/login";
            public static string Logout => $"{AccountBase}/logout";
            public static string Register => $"{AccountBase}/register";
        }

        public static class Phrase
        {
            public static string PhraseBase => $"{ApiBase}/phrases";
            public static string CreatePhrase => $"{PhraseBase}";
            public static string UpdatePhrase => $"{PhraseBase}";
            public static string CompileDeletePhrase(int id) => $"{PhraseBase}/{id}";
        }

        public static class Translation
        {
            public static string TranslationBase => $"{ApiBase}/translations";
            public static string CompileGetTranslations(CultureInfo from, CultureInfo to, int count = 1) =>
                $"{TranslationBase}/{from.Name}/{to.Name}/{count}";
            public static string CreateTranslation => $"{TranslationBase}";
            public static string UpdateTranslation => $"{TranslationBase}";
            public static string CompileDeleteTranslation(int id) => $"{TranslationBase}/{id}";
        }

        public static class Language
        {
            public static string LanguageBase => $"{ApiBase}/language";

            public static string CompileGetLanguage(CultureInfo cultureInfo) => $"{LanguageBase}/{cultureInfo.Name}";
            public static string AllLanguages => $"{LanguageBase}/all";
            public static string CreateLanguage => $"{LanguageBase}";
            public static string UpdateLanguage => $"{LanguageBase}";
            public static string CompileDeleteLanguage(int id) => $"{LanguageBase}/{id}";
        }
    }
}
