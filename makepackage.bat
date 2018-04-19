REM "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" ReactiveUIFody.sln

cd Nuget

..\packages\NugetUtilities.1.0.17\UpdateVersion.exe ReactiveUIFody.nuspec -Increment

mkdir build
cd build

copy ..\Nuget.exe .
copy ..\ReactiveUIFody.nuspec .

mkdir lib
mkdir lib\net45
mkdir lib\Xamarin.iOS10
mkdir lib\MonoAndroid
REM mkdir "lib\portable-net45+win+wpa81+wp80+MonoAndroid10+xamarinios10+MonoTouch10"

copy ..\..\ReactiveUI.Fody\bin\Release\ReactiveUI.Fody.* lib
copy ..\..\ReactiveUI.Fody.Helpers.Ios\bin\iPhone\Release\ReactiveUI.Fody.Helpers.* lib\Xamarin.iOS10
REM copy ..\..\ReactiveUI.Fody.1.0.26\lib\Xamarin.iOS10\ReactiveUI.Fody.Helpers.* lib\Xamarin.iOS10
copy ..\..\ReactiveUI.Fody.Helpers.Net45\bin\Release\ReactiveUI.Fody.Helpers.* lib\net45
REM copy ..\..\ReactiveUI.Fody.Helpers.Pcl\bin\Debug\ReactiveUI.Fody.Helpers.* "lib\portable-net45+win+wpa81+wp80+MonoAndroid10+xamarinios10+MonoTouch10"
copy ..\..\ReactiveUI.Fody.Helpers.Android\bin\Release\ReactiveUI.Fody.Helpers.* lib\MonoAndroid
REM copy ..\..\ReactiveUI.Fody.1.0.26\lib\MonoAndroid\ReactiveUI.Fody.Helpers.* lib\MonoAndroid


nuget pack ReactiveUIFody.nuspec

copy *.nupkg ..

cd ..
rmdir build /S /Q
cd ..