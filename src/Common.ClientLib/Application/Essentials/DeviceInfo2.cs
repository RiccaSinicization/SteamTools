using Xamarin.Essentials;
using System.Application.Services;
using XEDeviceIdiom = Xamarin.Essentials.DeviceIdiom;

// ReSharper disable once CheckNamespace
namespace System.Application
{
    /// <inheritdoc cref="DeviceInfo"/>
    public static class DeviceInfo2
    {
        static readonly Lazy<IDeviceInfoPlatformService?> @interface = new(DI.Get_Nullable<IDeviceInfoPlatformService>);
        static IDeviceInfoPlatformService? Interface => @interface.Value;

        /// <inheritdoc cref="DeviceInfo.Model"/>
        public static string Model
        {
            get
            {
                if (Essentials.IsSupported)
                {
                    return DeviceInfo.Model;
                }
                else
                {
                    return Interface?.Model ?? string.Empty;
                }
            }
        }

        /// <inheritdoc cref="DeviceInfo.Manufacturer"/>
        public static string Manufacturer
        {
            get
            {
                if (Essentials.IsSupported)
                {
                    return DeviceInfo.Manufacturer;
                }
                else if (Interface != null)
                {
                    return Interface.Manufacturer;
                }
                else
                {
                    if (OperatingSystem2.IsMacOS || OperatingSystem2.IsIOS || OperatingSystem2.IsTvOS || OperatingSystem2.IsWatchOS)
                    {
                        return "Apple";
                    }
                    return string.Empty;
                }
            }
        }

        /// <inheritdoc cref="DeviceInfo.Name"/>
        public static string Name
        {
            get
            {
                if (Essentials.IsSupported)
                {
                    return DeviceInfo.Name;
                }
                else
                {
                    return Interface?.Name ?? string.Empty;
                }
            }
        }

        /// <inheritdoc cref="DeviceInfo.VersionString"/>
        public static string VersionString
        {
            get
            {
                if (Essentials.IsSupported)
                {
                    return DeviceInfo.VersionString;
                }
                else
                {
                    return Interface?.VersionString ?? string.Empty;
                }
            }
        }

        /// <inheritdoc cref="DeviceInfo.Platform"/>
        public static Platform Platform
        {
            get
            {
                if (OperatingSystem2.IsWindows)
                {
                    if (DeviceInfo.Platform == DevicePlatform.UWP)
                    {
                        return Platform.UWP;
                    }
                    else
                    {
                        return Platform.Windows;
                    }
                }
                else if (OperatingSystem2.IsAndroid)
                {
                    return Platform.Android;
                }
                else if (
                    OperatingSystem2.IsIOS ||
                    OperatingSystem2.IsMacOS ||
                    OperatingSystem2.IsTvOS ||
                    OperatingSystem2.IsWatchOS)
                {
                    return Platform.Apple;
                }
                else if (OperatingSystem2.IsLinux)
                {
                    return Platform.Linux;
                }
                return Platform.Unknown;
            }
        }

        /// <inheritdoc cref="DeviceInfo.Idiom"/>
        public static DeviceIdiom Idiom
        {
            get
            {
                if (OperatingSystem2.IsDesktop)
                {
                    return DeviceIdiom.Desktop;
                }
                else
                {
                    return DeviceInfo.Idiom.Convert();
                }
            }
        }

        public static string OSName => OSNameValue.ToDisplayName();

        static readonly Lazy<OSNames.Value> _OSNameValue = new(() =>
        {
            if (OperatingSystem2.IsWindows)
            {
                if (DeviceInfo.Platform == DevicePlatform.UWP)
                {
                    return OSNames.Value.UWP;
                }
                else if (DesktopBridge.IsRunningAsUwp)
                {
                    return OSNames.Value.WindowsDesktopBridge;
                }
                else
                {
                    return OSNames.Value.Windows;
                }
            }
            else if (OperatingSystem2.IsAndroid)
            {
                if (OperatingSystem2.IsRunningOnWSA)
                {
                    return OSNames.Value.WSA;
                }
                else if (Interface != null && Interface.IsChromeOS)
                {
                    return OSNames.Value.ChromeOS;
                }
                else
                {
                    if (DeviceType == DeviceType.Virtual)
                    {
                        return OSNames.Value.AndroidVirtual;
                    }
                    return DeviceInfo.Idiom.Convert() switch
                    {
                        DeviceIdiom.Phone => OSNames.Value.AndroidPhone,
                        DeviceIdiom.Tablet => OSNames.Value.AndroidTablet,
                        DeviceIdiom.Desktop => OSNames.Value.AndroidDesktop,
                        DeviceIdiom.TV => OSNames.Value.AndroidTV,
                        DeviceIdiom.Watch => OSNames.Value.AndroidWatch,
                        _ => OSNames.Value.AndroidUnknown,
                    };
                }
            }
            else if (OperatingSystem2.IsIOS)
            {
                if (DeviceInfo.Idiom == XEDeviceIdiom.Tablet)
                    return OSNames.Value.iPadOS;
                return OSNames.Value.iOS;
            }
            else if (OperatingSystem2.IsMacOS)
            {
                return OSNames.Value.macOS;
            }
            else if (OperatingSystem2.IsTvOS)
            {
                return OSNames.Value.tvOS;
            }
            else if (OperatingSystem2.IsWatchOS)
            {
                return OSNames.Value.watchOS;
            }
            else if (OperatingSystem2.IsLinux)
            {
                return OSNames.Value.Linux;
            }
            return default;
        });
        public static OSNames.Value OSNameValue => _OSNameValue.Value;

        /// <inheritdoc cref="DeviceInfo.DeviceType"/>
        public static DeviceType DeviceType
        {
            get
            {
                if (Interface != null)
                {
                    return Interface.DeviceType;
                }
                return DeviceInfo.DeviceType;
            }
        }
    }
}