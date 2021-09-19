@echo off
cd /d "%~dp0"

echo generate typescript class diagram for FindAdresses
call tplant --input ..\ClientApp\src\api\FindAdressesBase.ts ..\ClientApp\src\impl\FindAdresses.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\FindAdresses.puml -A
echo generate typescript class diagram for GetAdress
call tplant --input ..\ClientApp\src\api\GetAdressBase.ts ..\ClientApp\src\impl\GetAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\GetAdress.puml -A
echo generate typescript class diagram for SetAdress
call tplant --input ..\ClientApp\src\api\SetAdressBase.ts ..\ClientApp\src\impl\SetAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\SetAdress.puml -A

echo generate typescript class diagram for SampleCommand
call tplant --input ..\ClientApp\src\api\SampleCommandBase.ts ..\ClientApp\src\impl\SampleCommand.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\SampleCommand.puml -A

echo generate typescript class diagram for DeleteAdress
call tplant --input ..\ClientApp\src\api\DeleteAdressBase.ts ..\ClientApp\src\impl\DeleteAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\DeleteAdress.puml -A
