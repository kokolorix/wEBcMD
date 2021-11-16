# wEBcMD Backend

The C# implemented server part in form of a REST Api

The Api can be explored with Swagger https://localhost:5001/swagger

[How it works](./Doc/README.md)

[Types and what they do](./Types/README.md)

### Commands to build and run the entire app

Assuming that the shell was started in ```./wEBcMD/wEBcMD```

`````````` {cmd}
dotnet run
``````````

`````````` {cmd}
dotnet build
``````````

### Commands to build and run the client

`````````` {cmd}
pushd wEBcMD/ClientApp
ng serve
popd
``````````

`````````` {cmd}
pushd wEBcMD/ClientApp
ng build
popd
``````````

### Commands to generate types and docs

Generate the types

`````````` {cmd}
pushd ..
wEBcMD-Types
popd
``````````

Generate the Class-Diagrams

For C#

`````````` {cmd}
puml-gen .\Types .\Doc\cs -dir  -createAssociation -allInOne
``````````

`````````` {cmd}
puml-gen . .\Doc\cs -dir -excludePaths .vscode,bin,ClientApp,Controllers,Doc,obj,Properties,tools,wwwroot,Install  -createAssociation -allInOne
``````````

For Typescript all Classes together

`````````` {cmd}
tplant --input ClientApp\src\api\*.ts ClientApp\src\impl\*.ts --output Doc\ts\AllStuff.puml -A
``````````

For Typescript a specific Command

`````````` {cmd}
tplant --input ClientApp\src\api\FindAdressesBase.ts ClientApp\src\impl\FindAdresses.ts ClientApp\src\impl\CommandWrapper.ts --output Doc\ts\FindAdresses.puml -A
``````````

Generate svg from puml

`````````` {cmd}
java -jar tools\plantuml.jar Doc\Types\*.puml -svg
``````````

Install PlantUML C#-Generator

`````````` {cmd}
dotnet tool install --global PlantUmlClassDiagramGenerator
``````````

Install PlantUML Typescrip-Generator

`````````` {cmd}
npm install --global tplant
``````````

[Documentation of PlantUml](https://plantuml.com/de/)
