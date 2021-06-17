taskkill /IM "InsuranceQuoter.Saga.Service.exe" /F
taskkill /IM "InsuranceQuoter.Presentation.Hub.exe" /F
taskkill /IM "InsuranceQuoter.Presentation.Api.exe" /F

taskkill /IM "dotnet.exe" /F

echo "Complete"
