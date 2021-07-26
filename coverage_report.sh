#! /bin/sh

coverageFileName="coverage.cobertura.xml"
outputDirectory="./CoverageReport"
# Since we don't want to include multiple test runs data in our analysis, we
# should remove old test coverage data. For now, we do this by cleaning. That's
# technically inefficient, but since downloading nuget packages and building is
# cheap, we are fine with it.
git clean -dfx

# This runs code coverage and drops the results into XML files in each test
# project in the solution.
dotnet test --collect 'XPlat Code Coverage'

# We need to locate somehow the coverage files. The ReportGenerator command uses
# a semicolon delimited list of paths, so we use transliterate to change the
# newline delimited output of the find command into a semicolon delimited one.
coverageFilePaths=$(find . -type f -name "$coverageFileName" | tr "\n" ";")

reportgenerator "-reports:$coverageFilePaths" "-targetdir:$outputDirectory"
