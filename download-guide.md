# 下载指南
- Desktop(桌面端)
	- Windows
		- 如果你使用 Intel、AMD 的 x64(x86-64) 芯片的 PC，则下载文件名中带有 **win_x64** 的文件
		- 如果你 **已安装** 了 [ASP.NET Core 运行时 6.0.4](https://dotnet.microsoft.com/zh-cn/download/dotnet/6.0) 则下载文件名中带有 **fde(框架依赖)** 的文件
		- 注意：是 《ASP.NET Core 运行时》 而不是 《.NET 桌面运行时》
			- [在 Windows 上安装 .NET](https://docs.microsoft.com/zh-cn/dotnet/core/install/windows)
				- 本程序在 Windows 上不需要 Hosting Bundle 和 IIS support
				- [下载安装程序并手动安装(aspnetcore-runtime-6.0.4-win-x64.exe)](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/runtime-aspnetcore-6.0.4-windows-x64-installer)
				- [使用 PowerShell 自动化安装](https://docs.microsoft.com/zh-cn/dotnet/core/install/windows?tabs=net60#install-with-powershell-automation)
	- macOS
		- 如果你使用 Intel 的 x64(x86-64) 芯片的 Mac，则下载文件名中带有 **macos_x64** 的文件
		- 如果你使用 ARM64(Apple Silicon) 芯片的 Mac，例如 **Apple M1**，则下载文件名中带有 **macos_arm64** 的文件
	- Linux
		- 如果你使用 Intel、AMD 的 x64(x86-64) 芯片的 PC 则下载文件名中带有 **linux_x64** 的文件
		- 如果你使用 ARM64 芯片的 PC 例如 **Raspberry Pi Model 3+**，则下载文件名中带有 **linux_arm64** 的文件

<!--		
- Mobile(移动端)
	- Android
		- 如果你使用 ARM64 芯片的设备（较为**普遍**）则下载文件名中带有 **android_arm64_v8a** 的文件
		- 如果你使用 ARM32 芯片的设备（较为**稀有**）通常为 **14** 年下半年之前生产的设备，则下载文件名中带有 **android_armeabi_v7a** 的文件
		- 如果你使用 Intel、AMD 的 x64 芯片的设备（较为**稀有**）则下载文件名中带有 **android_x64** 的文件
-->

<!--
- 如果你使用 ARM64 芯片的 PC（极为**稀有**），例如 **Surface Pro X**，则下载文件名中带有 **win_x64** 的文件可通过 Win11 x86 模拟运行
- **[暂未支持]** ~~如果你使用 ARM64 芯片的 PC（极为**稀有**），例如 **Surface Pro X**，则下载文件名中带有 **win_arm64** 的文件~~
- **[暂未支持]** ~~如果你使用 ARM64 芯片的 Mac（较为**稀有**），例如 **M1**，则下载文件名中带有 **macos_arm64** 的文件~~
			- [在 Linux 上安装 .NET](https://docs.microsoft.com/en-us/dotnet/core/install/linux)
				- 推荐 [通过 Snap 安装 .NET Runtime](https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-snap)
				- ```sudo snap install dotnet-runtime-60 --classic```
-->
