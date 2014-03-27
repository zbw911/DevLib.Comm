:: cd  .\.nuget

:: comm
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Config\Dev.Comm.Config.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Core\Dev.Comm.Core.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Data\Dev.Comm.Data.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Net\Dev.Comm.Net.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Validate\Dev.Comm.Validate.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Web\Dev.Comm.Web.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Comm\Dev.Comm.Web.Mvc\Dev.Comm.Web.Mvc.csproj  -Build -Properties Configuration=Release

::cache
.\NuGet.exe  pack ..\Framework\Cache\Kt.Framework.Cache\Dev.Framework.Cache.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Cache\Kt.Framework.Cache.AppFabric\Dev.Framework.Cache.AppFabric.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Cache\Kt.Framework.Cache.Impl\Dev.Framework.Cache.Impl.csproj  -Build -Properties Configuration=Release

:: file server
.\NuGet.exe  pack ..\Framework\FileServer\Kt.Framework.ImageServer\Dev.Framework.FileServer.csproj  -Build -Properties Configuration=Release

:: Log
.\NuGet.exe  pack ..\Framework\Log\dev.Log\Dev.Log.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Log\Dev.Log.Sms\Dev.Log.Sms.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework\Log\Dev.Comm.Log\Dev.Log.Impl.csproj  -Build -Properties Configuration=Release

:: adpaper
.\NuGet.exe  pack ..\Framework2\Dev.Crosscutting.Adapter\Dev.Crosscutting.Adapter.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework2\Dev.Crosscutting.Adapter.NetFramework\Dev.Crosscutting.Adapter.NetFramework.csproj  -Build -Properties Configuration=Release

::这个有问题
.\NuGet.exe  pack  ..\Framework2\Dev.DataContextStorage\Dev.DataContextStorage.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework2\Dev.Data\Dev.Data.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework2\Dev.Data.Infras\Dev.Data.Infras.csproj  -Build -Properties Configuration=Release
.\NuGet.exe  pack ..\Framework2\Dev.Web.CompositionRootBase\Dev.Web.CompositionRootBase.csproj  -Build -Properties Configuration=Release
 ::CAS
.\NuGet.exe  pack ..\CAS\Dev.CasClient\Dev.CasClient.csproj  -Build -Properties Configuration=Release

.\NuGet.exe   push .\*.nupkg -s http://192.168.1.10:9999/ 123456


pause