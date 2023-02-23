## Create and publish package
```powershell
$version="1.0.4"
$owner="Gorold-Streaming-Services"
$gh_pat="[PAT HERE]"

dotnet pack . --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/gorold.common -o ../nugets/

dotnet nuget push ..\packages\Play.Common.$version.nupkg --api-key $gh_pat --source "github"
```