## Add the GitHub package source
```powershell
$owner="Gorold-Streaming-Services"
$gh_pat="[PAT HERE]"

dotnet nuget add source --username USERNAME --password $gh_pat --store-password-in-clear-text --name github "https://nuget.pkg.github.com/$owner/index.json"
```
## Create and publish package
```powershell
$version="1.0.4"
$owner="Gorold-Streaming-Services"
$gh_pat="[PAT HERE]"

dotnet pack . --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/gorold.common -o ../nugets/

dotnet nuget push ../nugets/Gorold.Common.$version.nupkg --api-key $gh_pat --source "github"
```