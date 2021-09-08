# wEBcMD Backend

The C# implemented server part in form of a REST Api

The Api can be explored with Swagger https://localhost:5001/swagger


### Commands to build and run the entire app

Assuming that the shell was started in ```./wEBcMD/wEBcMD```

~~~~~~~~~~{cmd}
call dotnet run
~~~~~~~~~~~~~~~~
~~~~~~~~~~{cmd}
call dotnet build
~~~~~~~~~~~~~~~

### Commands to build and run the client
~~~~~~~~~~{cmd}
pushd wEBcMD/ClientApp
call ng serve
popd
~~~~~~~~~~~~~~~
~~~~~~~~~~{cmd}
pushd wEBcMD/ClientApp
call ng build
popd
~~~~~~~~~~~~~~~
### Commands to generate types and docs

Generate the types
~~~~~~~~~~{cmd}
pushd ..
call wEBcMD-Types
popd
~~~~~~~~~~~~~~~

Generate the Class-Diagrams

For C# 
~~~~~~~~~~{cmd}
call puml-gen .\Types .\Doc\cs -dir  -createAssociation -allInOne
~~~~~~~~~~~~~~~

~~~~~~~~~~{cmd}
call puml-gen . .\Doc\cs -dir -excludePaths .vscode,bin,ClientApp,Controllers,Doc,obj,Properties,tools,wwwroot,Install  -createAssociation -allInOne
~~~~~~~~~~~~~~~

For Typescript all Classes together
~~~~~~~~~~{cmd}
call tplant --input ClientApp\src\api\*.ts ClientApp\src\impl\*.ts --output Doc\ts\AllStuff.puml -A 
~~~~~~~~~~~~~~~

For Typescript a specific Command
~~~~~~~~~~{cmd}
call tplant --input ClientApp\src\api\FindAdressesBase.ts ClientApp\src\impl\FindAdresses.ts ClientApp\src\impl\CommandWrapper.ts --output Doc\ts\FindAdresses.puml -A
~~~~~~~~~~~~~~~

Generate svg from puml
~~~~~~~~~~{cmd}
call java -jar tools\plantuml.jar Doc\Types\*.puml -svg    ~~~~~~~~~~~~~~~

Install PlantUML C#-Generator
~~~~~~~~~~{cmd}
call dotnet tool install --global PlantUmlClassDiagramGenerator
~~~~~~~~~~~~~~~
Install PlantUML Typescrip-Generator
~~~~~~~~~~{cmd}
call npm install --global tplant
~~~~~~~~~~~~~~~

[Documentation of PlantUml](https://plantuml.com/de/)


