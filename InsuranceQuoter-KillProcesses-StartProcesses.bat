SET insurancequoterhome=%CD%

taskkill /IM "InsuranceQuoter.Saga.Service.exe" /F
taskkill /IM "InsuranceQuoter.Presentation.Hub.exe" /F
taskkill /IM "InsuranceQuoter.Presentation.Api.exe" /F

taskkill /IM "dotnet.exe" /F

echo "Complete"

cd %insurancequoterhome%"\InsuranceQuoter.Saga.Service\bin\Debug\net5.0"
start InsuranceQuoter.Saga.Service.exe

cd %insurancequoterhome%"\InsuranceQuoter.Presentation.Hub\bin\Debug\net5.0"
start InsuranceQuoter.Presentation.Hub.exe

cd %insurancequoterhome%"\InsuranceQuoter.Presentation.Api\bin\Debug\net5.0"
start InsuranceQuoter.Presentation.Api.exe

echo "Complete"
