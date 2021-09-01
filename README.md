# wEBcMD

A little Project to implement a command pattern on C# and Angular base

@startuml

ClientWrapper -> ServerWrapper: execute(cmd: ComandDTO)
activate ServerWrapper
ClientWrapper <- ServerWrapper: response(cmd: ComandDTO)
deactivate ServerWrapper

@enduml