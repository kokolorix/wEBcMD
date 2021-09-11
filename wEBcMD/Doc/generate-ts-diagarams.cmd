@echo off
cd /d "%~dp0"

echo generate typescript class diageram for FindAdresses
call tplant --input ..\ClientApp\src\api\FindAdressesBase.ts ..\ClientApp\src\impl\FindAdresses.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\FindAdresses.puml -A
echo generate typescript class diageram for GetAdress
call tplant --input ..\ClientApp\src\api\GetAdressBase.ts ..\ClientApp\src\impl\GetAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\GetAdress.puml -A
echo generate typescript class diageram for SetAdress
call tplant --input ..\ClientApp\src\api\SetAdressBase.ts ..\ClientApp\src\impl\SetAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\SetAdress.puml -A

echo generate typescript class diageram for SampleCommand
call tplant --input ..\ClientApp\src\api\SampleCommandBase.ts ..\ClientApp\src\impl\SampleCommand.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\SampleCommand.puml -A
