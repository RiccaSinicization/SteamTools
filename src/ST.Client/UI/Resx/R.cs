using ReactiveUI;
using System.Application.Services;
using System.Application.UI.ViewModels;
using System.Application.UI.ViewModels.Abstractions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Properties;
using System.Reactive.Linq;
using Interface = System.Application.UI.Resx.R;
using BaseClass = System.Application.UI.Resx.Abstractions.R;
using static System.Application.UI.Resx.R;

namespace System.Application.UI.Resx
{
#pragma warning disable IDE1006 // 命名样式
    internal interface R : IReactiveObject
#pragma warning restore IDE1006 // 命名样式
    {
        static BaseClass Current => mCurrent is BaseClass i ? i : throw new NullReferenceException("R is null.");

        static BaseClass? mCurrent;
    }
}

namespace System.Application.UI.Resx.Abstractions
{
    public abstract class R : ReactiveObject, Interface
    {
        public static BaseClass Current => Interface.Current;

        public static readonly IReadOnlyCollection<KeyValuePair<string, string>> Languages;
        public static readonly Dictionary<string, string> SteamLanguages;
        static readonly Lazy<IReadOnlyCollection<KeyValuePair<string, string>>> mFonts = new(() => IFontManager.Instance.GetFonts());
        public static IReadOnlyCollection<KeyValuePair<string, string>> Fonts => mFonts.Value;

        public AppResources Res { get; protected set; } = new();

        public static CultureInfo DefaultCurrentUICulture { get; private set; }

        static R()
        {
            DefaultCurrentUICulture = CultureInfo.CurrentUICulture;
            var languagesDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "", "Auto" },
                { "zh-Hans", "Chinese (Simplified)" },
                { "zh-Hant", "Chinese (Traditional)" },
                { "en", "English" },
                { "ko", "Korean" },
                { "ja", "Japanese" },
                { "ru", "Russian" },
                { "es", "Spanish" },
                { "it", "Italian" },
            };
            Languages = languagesDict.ToList();
            SteamLanguages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "zh-CN", "schinese" },
                { "zh-Hans", "schinese" },
                { "zh-Hant", "tchinese" },
                { "en","english"},
                { "ko", "koreana" },
                { "ja", "japanese" },
                { "ru", "russian" },
                { "es", "spanish" },
                { "it", "italian" },
            };
            AppResources.Culture = DefaultCurrentUICulture;
        }

        static bool IsMatch(CultureInfo cultureInfo, string cultureName)
        {
            if (string.IsNullOrWhiteSpace(cultureInfo.Name))
            {
                return false;
            }
            if (cultureInfo.Name == cultureName)
            {
                return true;
            }
            else
            {
                return IsMatch(cultureInfo.Parent, cultureName);
            }
        }

        public static void ChangeAutoLanguage(CultureInfo? cultureInfo = null) => MainThread2.BeginInvokeOnMainThread(() => ChangeAutoLanguageCore(cultureInfo));

        static void ChangeAutoLanguageCore(CultureInfo? cultureInfo = null)
        {
            if (!string.IsNullOrWhiteSpace(SettingsPageViewModel.Instance.SelectLanguage.Key)) return;
            cultureInfo ??= CultureInfo.CurrentUICulture;
            DefaultCurrentUICulture = cultureInfo;
            ChangeLanguageCore(cultureInfo.Name);
        }

        /// <summary>
        /// 更改语言
        /// </summary>
        /// <param name="cultureName"></param>
        public static void ChangeLanguage(string? cultureName) => MainThread2.BeginInvokeOnMainThread(() => ChangeLanguageCore(cultureName));

        static void ChangeLanguageCore(string? cultureName)
        {
            if (cultureName == null || IsMatch(AppResources.Culture, cultureName)) return;
            AppResources.Culture = string.IsNullOrWhiteSpace(cultureName) ?
                DefaultCurrentUICulture :
                CultureInfo.GetCultureInfo(Languages.SingleOrDefault(x => x.Key == cultureName).Key);
            CultureInfo.DefaultThreadCurrentUICulture = AppResources.Culture;
            CultureInfo.DefaultThreadCurrentCulture = AppResources.Culture;
            CultureInfo.CurrentUICulture = AppResources.Culture;
            CultureInfo.CurrentCulture = AppResources.Culture;
            mAcceptLanguage = GetAcceptLanguageCore();
            mLanguage = GetLanguageCore();
            Current.Res = new();
            Current.RaisePropertyChanged(nameof(Res));
        }

        public static string GetCurrentCultureSteamLanguageName()
        {
            try
            {
                var culture = Culture;
                foreach (var item in SteamLanguages)
                {
                    if (IsMatch(culture, item.Key))
                    {
                        return item.Value;
                    }
                }
                //return AppResources.Culture == null ?
                //    Languages.First().Key :
                //    SteamLanguages[AppResources.Culture.Name];
            }
            catch (Exception ex)
            {
                Log.Error(nameof(R), ex, nameof(GetCurrentCultureSteamLanguageName));
            }
            return "english";
        }

        static string GetAcceptLanguageCore()
        {
            var culture = Culture;
            return culture.GetAcceptLanguage();
        }

        static string? mAcceptLanguage;

        public static string AcceptLanguage
        {
            get
            {
                if (mAcceptLanguage == null)
                {
                    mAcceptLanguage = GetAcceptLanguageCore();
                }
                return mAcceptLanguage;
            }
        }

        static string? mLanguage;

        static string GetLanguageCore()
        {
            var culture = Culture;
            foreach (var item in Languages)
            {
                if (!string.IsNullOrWhiteSpace(item.Key))
                {
                    if (IsMatch(culture, item.Key))
                    {
                        return item.Key;
                    }
                }
            }
            return "en";
        }

        public static string Language
        {
            get
            {
                if (mLanguage == null)
                {
                    mLanguage = GetLanguageCore();
                }
                return mLanguage;
            }
        }

        public static CultureInfo Culture => AppResources.Culture ?? DefaultCurrentUICulture;
    }

    public abstract class R<T> : BaseClass where T : R<T>, new()
    {
        public static T Current
        {
            get => ((T)Interface.Current)!;
            protected set => Interface.mCurrent = value;
        }

        static R()
        {
            Current = new();
        }
    }
}