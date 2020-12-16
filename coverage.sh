#!/bin/sh
# dotnet tool install -g dotnet-reportgenerator-globaltool
rm Advent2020.Tests/TestResults/*/coverage.cobertura.xml
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:./Advent2020.Tests/TestResults/*/coverage.cobertura.xml -targetdir:reports