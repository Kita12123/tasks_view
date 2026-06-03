@echo off
rem Encoding: UTF-8
chcp 65001 > nul

PROJ_NAME=Backend

echo ==========================================================
echo  Architecture Contract - %PROJ_NAME% Project Setup Script
echo ==========================================================

mkdir -p "src/%PROJ_NAME%"

echo Setup Solution...
dotnet new sln -n "%PROJ_NAME%"
dotnet new gitignore

echo Setup Domain Project...
rem [.Net Class Library]
dotnet new classlib -n "%PROJ_NAME%.Domain" -o "src/%PROJ_NAME%.Domain"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.Domain/%PROJ_NAME%.Domain.csproj"

echo Setup App Project...
rem [.Net Class Library] → Domain
dotnet new classlib -n "%PROJ_NAME%.App" -o "src/%PROJ_NAME%.App"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.App/%PROJ_NAME%.App.csproj"
dotnet add "src/%PROJ_NAME%.App/%PROJ_NAME%.App.csproj" reference "src/%PROJ_NAME%.Domain/%PROJ_NAME%.Domain.csproj"
    
echo Setup Infra Project...
rem [.Net Class Library] → Domain
dotnet new classlib -n "%PROJ_NAME%.Infra" -o "src/%PROJ_NAME%.Infra"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.Infra/%PROJ_NAME%.Infra.csproj"
dotnet add "src/%PROJ_NAME%.Infra/%PROJ_NAME%.Infra.csproj" reference "src/%PROJ_NAME%.Domain/%PROJ_NAME%.Domain.csproj"

echo Setup Web Project...
rem [.Net Web API] → App
dotnet new webapi -n "%PROJ_NAME%.Web" -o "src/%PROJ_NAME%.Web"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.Web/%PROJ_NAME%.Web.csproj"
dotnet add "src/%PROJ_NAME%.Web/%PROJ_NAME%.Web.csproj" reference "src/%PROJ_NAME%.App/%PROJ_NAME%.App.csproj"
dotnet add "src/%PROJ_NAME%.Web/%PROJ_NAME%.Web.csproj" reference "src/%PROJ_NAME%.Infra/%PROJ_NAME%.Infra.csproj"

echo ==========================================================
echo  [Success] Architecture Contract - %PROJ_NAME% Project Setup Script
echo ==========================================================
pause