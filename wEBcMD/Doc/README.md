# wEBcMD Documentation

## Index of Types
[wEBcMD Types](../Types/README.md)

## The System
![System overview](SystemOverview.svg)

## The way of working

### The simplest relationships
![Principle](CommandPrinciple.svg)

### The detailed Command structure
![Detail](CommandDetail.svg)

While the general command objects are designed as pure data transfer objects, the wrappers contain the concrete logic and comfort of the call and execution.

### The very concrete workflow for designing a command

1. We define interface and data structures in an XML file in the Types folder


	````xml
	<?xml version="1.0" encoding="UTF-8"?>
	<Types xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="..\tools\types.xsd">
		<Summary>
			Detailed Description of the Command-Module.
			Meaning and purpose, intentions and limitations.
		</Summary>
		<ObjectType name="FirstDTO" category="Samples" id="707b0137-90c4-4ec4-aa2f-e6bb9196d200">
			<Summary>Description of the Data-Strucure.</Summary>
			<Base name="BaseDTO" category="Base"/>
			<PropertyType name="One" type="String" summary="First property, a strng/>
			<PropertyType name="Second" type="Boolean" summary="Second one, a boolean"/>
		</ObjectType>
		<CommandWrapper name="First" http="get" category="Samples" id="198a715b-b094-4c47-8f5d-73063685a75e">
			<Summary>The very first command we designed</Summary>
			<Base name="CommandWrapper" category="Base"/>
			<ParameterType name="Id" type="UuId" modifier="in" summary="The Id of the Data-Object"/>
			<Result type="FirstDTO" summary="The Data-Object" />
			<DTO name="CommandDTO"/>
		</CommandWrapper>
	</Types>
	```

2. We run the Code-Generator, to let us create the code skeletons

	The result are two C# files for the serverside code, once the data structure, second the partial individual implementations, if any.

	If an http command is specified, a controller with the corresponding endpoint is created

	Furthermore, the client-side typescript classes for the call are generated 
