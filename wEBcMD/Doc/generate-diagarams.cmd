@echo off

call tplant --input ..\ClientApp\src\api\FindAdressesBase.ts ..\ClientApp\src\impl\FindAdresses.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\FindAdresses.puml -A
call tplant --input ..\ClientApp\src\api\GetAdressBase.ts ..\ClientApp\src\impl\GetAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\GetAdress.puml -A
call tplant --input ..\ClientApp\src\api\SetAdressBase.ts ..\ClientApp\src\impl\SetAdress.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\SetAdress.puml -A

call tplant --input ..\ClientApp\src\api\SampleCommandBase.ts ..\ClientApp\src\impl\SampleCommand.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\SampleCommand.puml -A