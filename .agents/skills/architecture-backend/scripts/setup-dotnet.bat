@echo off
rem Encoding: UTF-8
chcp 65001 > nul

PROJ_NAME=backend

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

echo Setup Application Project...
rem [.Net Class Library] → Domain
dotnet new classlib -n "%PROJ_NAME%.Application" -o "src/%PROJ_NAME%.Application"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.Application/%PROJ_NAME%.Application.csproj"
dotnet add "src/%PROJ_NAME%.Application/%PROJ_NAME%.Application.csproj" reference "src/%PROJ_NAME%.Domain/%PROJ_NAME%.Domain.csproj"
    
echo Setup Infrastructure Project...
rem [.Net Class Library] → Domain
dotnet new classlib -n "%PROJ_NAME%.Infrastructure" -o "src/%PROJ_NAME%.Infrastructure"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.Infrastructure/%PROJ_NAME%.Infrastructure.csproj"
dotnet add "src/%PROJ_NAME%.Infrastructure/%PROJ_NAME%.Infrastructure.csproj" reference "src/%PROJ_NAME%.Domain/%PROJ_NAME%.Domain.csproj"

echo Setup Presentation Project...
rem [.Net Web API] → Application
dotnet new webapi -n "%PROJ_NAME%.Presentation" -o "src/%PROJ_NAME%.Presentation"
dotnet sln "%PROJ_NAME%.sln" add "src/%PROJ_NAME%.Presentation/%PROJ_NAME%.Presentation.csproj"
dotnet add "src/%PROJ_NAME%.Presentation/%PROJ_NAME%.Presentation.csproj" reference "src/%PROJ_NAME%.Application/%PROJ_NAME%.Application.csproj"
dotnet add "src/%PROJ_NAME%.Presentation/%PROJ_NAME%.Presentation.csproj" reference "src/%PROJ_NAME%.Infrastructure/%PROJ_NAME%.Infrastructure.csproj"

echo ==========================================================
echo  [Success] Architecture Contract - %PROJ_NAME% Project Setup Script
echo ==========================================================
pause