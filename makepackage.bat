REM "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" ReactiveUIFody.sln

cd Nuget

..\packages\NugetUtilities.1.0.17\UpdateVersion.exe ReactiveUIFody.nuspec -Increment

mkdir build
cd build

copy ..\Nuget.exe .
copy ..\ReactiveUIFody.nuspec .

mkdir lib
mkdir lib\net462
mkdir lib\netstandard2.0

copy ..\..\ReactiveUI.Fody\bin\Release\netstandard2.0\ReactiveUI.Fody.* .
copy ..\..\ReactiveUI.Fody.Helpers\bin\Release\net46\ReactiveUI.Fody.Helpers.* lib\net462
copy ..\..\ReactiveUI.Fody.Helpers\bin\Release\netstandard2.0\ReactiveUI.Fody.Helpers.* lib\netstandard2.0


nuget pack ReactiveUIFody.nuspec

copy *.nupkg ..

cd ..
rmdir build /S /Q
cd ..