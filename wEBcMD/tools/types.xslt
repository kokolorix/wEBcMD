<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:filePath="http://www.w3.org/2005/02/xpath-functions"
                xmlns:xdt="http://www.w3.org/2005/02/xpath-dataTypes"
                xmlns:functx="http://www.functx.com"
                xmlns:wc="https://github.com/kokolorix/wEBcMD.git"
                xmlns:ts="https://www.typescriptlang.org/"
                xmlns:cs="https://docs.microsoft.com/en-us/dotnet/csharp/"
   >
   <xsl:output method="text" />
   <xsl:output name="text-def" method="text" />
   <xsl:output name="xml-def" method="xml" indent="yes" />
   <!-- <xsl:variable name="t1" select="'&#x9;'" /> -->
   <xsl:variable name="t1" select="'   '" />
   <xsl:variable name="t2" select="concat($t1, $t1)" />
   <xsl:variable name="t3" select="concat($t2, $t1)" />
   <xsl:variable name="t4" select="concat($t3, $t1)" />
   <xsl:variable name="t5" select="concat($t4, $t1)" />
   <xsl:variable name="nl1" select="'&#xA;'" />
   <xsl:variable name="nl2" select="concat($nl1, $nl1)" />
   <!--=======================================================================-->
   <!-- C# DTOs, wrappers and partially command implementations -->
   <!-- TS DTOs and wrappers with lazy evaluation of DTO properties -->
   <!--=======================================================================-->
   <xsl:template match="/">
      <xsl:call-template name="cs.command"/>
      <xsl:call-template name="cs.command.dispatcher"/>
      <xsl:call-template name="cs.command.types"/>
      <xsl:apply-templates select="Types/*" mode="cs.wrapper.impl" />
      <xsl:apply-templates select="Types/*" mode="ts.api.dto"/>
      <xsl:apply-templates select="Types/*" mode="ts.api.base" />
      <xsl:apply-templates select="Types/*" mode="ts.impl.wrapper" />

      <xsl:call-template name="cs.controller" />

      <xsl:call-template name="md.command.readme"/>

      <xsl:call-template name="ts.generate.diagrams.cmd"/>
   </xsl:template>
   <!--=======================================================================-->
   <!--=======================================================================-->
   <!--                                                                       -->
   <!--                                                                       -->
   <!--                             csharp                                    -->
   <!--                                                                       -->
   <!--                                                                       -->
   <!--=======================================================================-->
   <!--=======================================================================-->
   <xsl:template  name="cs.controller">
      <xsl:variable name="commands" select="./Types/CommandWrapper[@http]"/>
      <xsl:if test="count($commands)">
         <!-- <xsl:variable name="name" select="replace(@name, 'DTO', '')" /> -->
         <xsl:variable name="fn" select="concat(wc:file-name(.), 'Controller.cs')" />
         <xsl:variable name="d" select="wc:path-delimiter(.)"/>
         <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
         <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Controllers', $fn), $d)"/>
         <!-- <xsl:if test="not(unparsed-text-available($filePath, 'utf-8'))"> -->
         <!-- <xsl:message select="concat( 'TODO: ', $filePath, ' create (', name(.), ')')" /> -->
         <!-- <xsl:message select="."/> -->
         <xsl:result-document href="{$filePath}" format="text-def">
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
            <xsl:text/><xsl:apply-templates select="./Types" mode="cs.controller" /><xsl:text/>
}
         </xsl:result-document>
      </xsl:if>
   </xsl:template>
   <!--=======================================================================-->
   <!-- Serverside controller -->
   <!--=======================================================================-->
   <xsl:template match="Types" mode="cs.controller">
      <!-- <xsl:message select="'match Types'"/> -->
      <xsl:call-template name="cs.summary" >
         <xsl:with-param name="indent" select="$t1" />
      </xsl:call-template>
         <xsl:variable name="name" select="wc:pascalCase(wc:file-name(.))"/>
   [ApiController]
   [Route("[controller]")]
   public class <xsl:value-of select="$name"/>Controller : ControllerBase
   {
      private readonly ILogger&lt;<xsl:value-of select="$name"/>Controller&gt; _logger;
      /// &lt;summary&gt;
      /// Initialize the logger
      /// &lt;/summary&gt;
      public <xsl:value-of select="$name"/>Controller(ILogger&lt;<xsl:value-of select="$name"/>Controller&gt; logger) =&gt; _logger = logger;

<xsl:text/>
<xsl:apply-templates select="./CommandWrapper[@http]" mode="cs.controller"/>
<xsl:text/>
   }
<xsl:text/>
   </xsl:template>
   <!--=======================================================================-->
   <!-- Serverside classes -->
   <!--=======================================================================-->
   <xsl:template match="CommandWrapper" mode="cs.controller">
      <xsl:text/>
         <xsl:call-template name="cs.summary" >
            <xsl:with-param name="indent" select="$t2" />
         </xsl:call-template>
      <xsl:text/>
      [Http<xsl:value-of select="wc:pascalCase(@http)"/>]
      [Route("<xsl:value-of select="lower-case(@name)"/>")]
      <!-- <xsl:value-of select="cs:param-list(.)"/> -->
      public <xsl:value-of select="cs:result-type(.)"/>
      <xsl:text> </xsl:text>
      <xsl:value-of select="wc:pascalCase(@name)"/>
      <xsl:text>( </xsl:text>
      <xsl:value-of select="cs:param-list(.)"/>
      <xsl:text> )</xsl:text>
      {
         <xsl:value-of select="concat(@name, 'Wrapper')"/> wrapper = new();
         <xsl:for-each select="./ParameterType">
         wrapper.<xsl:value-of select="@name"/> = <xsl:value-of select="wc:camelCaseWord(@name)"/>;</xsl:for-each>
         <!-- wrapper.ExecuteCommand(); -->
			<xsl:value-of select="concat($nl2, $t3)"/>
			<xsl:if test="cs:result-type(.) != 'void'">return </xsl:if>wrapper.<xsl:value-of select="@name"/>(<xsl:text/>
				<xsl:for-each select="ParameterType">
					<xsl:if test="position() > 1">, </xsl:if>
					wrapper.<xsl:value-of select="@name"/>
				</xsl:for-each>
			);
      }
   </xsl:template>
   <!--=======================================================================-->
   <!-- Serverside classes -->
   <!--=======================================================================-->
   <xsl:template name="cs.command">
      <xsl:variable name="csFilePath" select="replace(base-uri(.),'.xml' ,'.cs')" />
      <xsl:message select="$csFilePath" />
      <xsl:result-document href="{$csFilePath}" format="text-def">
         <xsl:value-of select="concat('using System;', $nl1)" />
         <xsl:value-of select="concat('using System.Collections.Generic;', $nl2)" />
         <xsl:value-of select="concat('namespace wEBcMD', $nl1)" />
         <xsl:value-of select="concat('{', $nl1)" />
         <xsl:apply-templates select="Types/ObjectType" mode="cs.dto" />
         <xsl:apply-templates select="Types/ObjectWrapper" mode="cs.wrapper" />
         <xsl:apply-templates select="Types/CommandType" mode="cs.dto" />
         <xsl:apply-templates select="Types/CommandWrapper" mode="cs.wrapper" />
         <xsl:call-template name="cs.dispatcher"/>
         <xsl:call-template name="cs.types"/>
         <xsl:value-of select="concat('}', $nl1)" />
      </xsl:result-document>   </xsl:template>
   <!--=======================================================================-->
   <!-- Dispatcher, to find the matching wrapper -->
   <!--=======================================================================-->
   <xsl:template name="cs.dispatcher">
   static partial class <xsl:value-of select="wc:file-name(.)"/>
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         <xsl:for-each select="Types/CommandWrapper">
            <xsl:variable name="name" as="xs:string" select="concat(./@name, 'Wrapper')"/>
         else if(<xsl:value-of select="$name"/>.IsForMe(dto))
            return <xsl:value-of select="$name"/>.ExecuteCommand(dto);
         </xsl:for-each>
         return null;
      }
   }
<xsl:text/>
   </xsl:template>
   <!--=======================================================================-->
   <!-- Types from this module -->
   <!--=======================================================================-->
   <xsl:template name="cs.types">
	///&lt;summary&gt;Types from this module&lt;/summary&gt;
   static partial class <xsl:value-of select="wc:file-name(.)"/>
   {
		///&lt;summary&gt;List of cref=&quot;CommandTypeDTO&quot; from this module&lt;/summary&gt;
      public static void GetTypes(ref List&lt;CommandTypeDTO&gt; commandTypes)
      {
			commandTypes.AddRange(new CommandTypeDTO[] {<xsl:text/>
				<xsl:for-each select="Types/CommandWrapper">
				new() {
					Name = "<xsl:value-of select="@name"/>",
					Result = "<xsl:choose>
						<xsl:when test="./Result">
							<xsl:value-of select="./Result/@type"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>Void</xsl:text>
						</xsl:otherwise>
					</xsl:choose>",
					Parameters = new List&lt;ParamDTO&gt;()
					{
						<xsl:for-each select="ParameterType">
						<xsl:text/>new() {
							Name="<xsl:value-of select="@name"/>",
							Type="<xsl:value-of select="@type"/>",
						},<xsl:text/>
						</xsl:for-each>
					},
					Id = Guid.Parse("<xsl:value-of select="@id"/>"),
					Type = CommandTypeDTO.TypeId,
				},<xsl:text/>
				</xsl:for-each>
			});
      }
   }
<xsl:text/>
   </xsl:template>
	<!--=======================================================================-->
	<!--process an ObjectType node -->
	<!--=======================================================================-->
	<xsl:template match="ObjectType" mode="cs.dto">
		<!-- <xsl:message select="concat('process ObjectType mode cs.dto ', @name)"/> -->
		<xsl:variable name="category" select="@category" />
		<xsl:variable name="name" select="@name" />
		<xsl:variable name="id" select="@id" />
		<xsl:call-template name="cs.summary">
			<xsl:with-param name="indent" select="$t1" />
		</xsl:call-template>
		<xsl:choose>
			<xsl:when test="$name='BaseDTO'">
				<xsl:value-of select="concat($t1, 'public partial class ', $name)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="concat($t1, 'public class ', $name)" />
			</xsl:otherwise>
		</xsl:choose>
		<xsl:apply-templates select="Base" mode="cs.dto" />
		<xsl:value-of select="concat($nl1, $t1, '{', $nl1)" />
		<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', $id, ' is the Id of ', $name,' type.&lt;/summary&gt;', $nl1)" />
		<xsl:choose>
			<xsl:when test="./Base/@name=''">
				<xsl:value-of select="concat($t2, 'public static ')" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="concat($t2, 'new public static ')" />
			</xsl:otherwise>
		</xsl:choose>
		<xsl:value-of select="concat('Guid TypeId { get =&gt; System.Guid.Parse(&quot;', $id, '&quot;); }', $nl1)" />
		<!-- <xsl:if test="$name!='BaseDTO' and $name!='PropertyDTO'">
           <xsl:value-of select="concat($t2, '/// &lt;summary&gt;Id of ', $name,' type.&lt;/summary&gt;', $nl1)" />
           <xsl:value-of select="concat($t2, 'public override Guid Type { get =&gt; ', $name, '.TypeId; }', $nl1)" />
           </xsl:if> -->
		<xsl:apply-templates select="PropertyType" mode="cs.dto" />
		<xsl:value-of select="concat($t1, '};', $nl2)" />
	</xsl:template>
	<!--=======================================================================-->
	<!--process the Base node -->
	<!--=======================================================================-->
	<xsl:template match="Base" mode="cs.dto">
		<!-- <xsl:message select="concat('process Base mode cs.dto ', @name)"/> -->
		<xsl:variable name="category" select="@category" />
		<xsl:variable name="name" select="@name" />
		<xsl:variable name="base" select="concat($category, '::', $name)" />
		<xsl:if test="$name!=''">
			<xsl:value-of select="concat(' : ', $name)" />
		</xsl:if>
	</xsl:template>
	<!--=======================================================================-->
	<!--process an Property node for PropertyT -->
	<!--=======================================================================-->
	<xsl:template match="PropertyType" mode="cs.dto">
		<!-- <xsl:message select="concat('process PropertyType mode cs.dto ', @name)"/> -->
		<xsl:variable name="name" select="@name" />
		<xsl:call-template name="cs.summary">
			<xsl:with-param name="indent" select="$t2" />
		</xsl:call-template>
		<xsl:variable name="dataType" as="xs:string" select="cs:data-type(.)"/>
		<xsl:value-of select="concat($t2, 'public virtual ', $dataType)" />
		<xsl:value-of select="concat(' ', $name, ' { get; set; }')" />
		<xsl:if test="@default">
			<xsl:choose>
				<xsl:when test="starts-with($dataType, 'List')">
					<xsl:value-of select="concat(' = new ', @dataType, '(){', '};')" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="concat(' = ', @default, ';')" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
		<xsl:value-of select="concat($nl1, '')" />
	</xsl:template>
	<!--=======================================================================-->
	<!--process an CommandWrapper node -->
	<!--=======================================================================-->
	<xsl:template match="CommandWrapper" mode="cs.wrapper">
		<!-- <xsl:message select="concat('process CommandWrapper mode cs.wrapper ', @name)"/> -->
		<xsl:variable name="category" select="@category" />
		<xsl:variable name="name" select="concat(@name, 'Wrapper')" />
		<xsl:variable name="id" select="@id" />
		<xsl:variable name="base" select="./Base/@name" />
		<xsl:variable name="dto" select="./DTO/@name"/>
		<!--  -->
		<xsl:call-template name="cs.summary">
			<xsl:with-param name="indent" select="$t1" />
		</xsl:call-template>
		<xsl:value-of select="concat($t1, 'public partial class ', $name, ' : ', $base)" />
		<xsl:value-of select="concat($nl1, $t1, '{', $nl1)" />
		<!--  -->
		<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Constructor of ', $name,'&lt;/summary&gt;', $nl1)" />
		<xsl:value-of select="concat($t2, 'public ', $name, '(', $dto, ' dto = null):base(dto){}', $nl1)" />

		<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', $id, ' is the Id of ', $name,' type.&lt;/summary&gt;', $nl1)" />
		<xsl:value-of select="concat($t2, 'public static Guid TypeId { get =&gt; System.Guid.Parse(&quot;', $id, '&quot;); }', $nl1)" />
		<!--  -->
		<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Checks if the type of the DTO fits', '&lt;/summary&gt;', $nl1)" />
		<xsl:value-of select="concat($t2, 'public static bool IsForMe(', $dto, ' dto) => dto.Type == ', $name, '.TypeId;', $nl1)" />
		<!--
		<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Create the wrapper and execute the command', '&lt;/summary&gt;', $nl1)" />
		<xsl:value-of select="concat($t2, 'public static ', $dto, ' ExecuteCommand(', $dto, ' dto) => new ', $name, '(dto).ExecuteCommand();', $nl1)" />
		<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Execute the command', '&lt;/summary&gt;', $nl1)" />
		<xsl:value-of select="concat($t2, 'public partial ', $dto, ' ExecuteCommand();', $nl1)" />
		<xsl:value-of select="$nl1"/>
      -->
		/// &lt;summary&gt;Create the wrapper and execute the command&lt;/summary&gt;
		public static <xsl:value-of select="$dto"/> ExecuteCommand( <xsl:value-of select="$dto"/> dto )
		{
			<xsl:value-of select="$name"/> wrapper = new(dto);
			<xsl:if test="cs:result-type(.) != 'void'">
			wrapper.Result = <xsl:text/>
			</xsl:if>
			wrapper.<xsl:value-of select="@name"/>(<xsl:text/>
				<xsl:for-each select="ParameterType">
					<xsl:if test="position() > 1">, </xsl:if>
					wrapper.<xsl:value-of select="@name"/>
				</xsl:for-each>
			);

			return wrapper.Cmd;
		}

		<xsl:call-template name="cs.summary">
			<xsl:with-param name="indent" select="$t2"/>
		</xsl:call-template>
<xsl:text/>		public partial <xsl:value-of select="cs:result-type(.)"/>
		<xsl:text> </xsl:text>
		<xsl:value-of select="@name"/>(<xsl:text/>
		<xsl:value-of select="cs:param-list(.)"/>);

		/// &lt;summary&gt;Serialize / Deserialize concrete <xsl:value-of select="@name"/> to generic CommandDTO &lt;/summary&gt;
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				<xsl:for-each select="ParameterType">
				<!-- parameter name -->
				<xsl:variable name="pn" as="xs:string" select="@name"/>
				this.Set(cmd, "<xsl:value-of select="$pn"/>", <xsl:value-of select="$pn"/>);<xsl:text/>
				</xsl:for-each>
				<xsl:if test="cs:result-type(.) != 'void'">
				this.Set(cmd, "Result", Result);
				</xsl:if>
				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				<xsl:for-each select="ParameterType">
				<!-- parameter name, data type -->
				<xsl:variable name="pn" as="xs:string" select="@name"/>
				this.Get(cmd, "<xsl:value-of select="$pn"/>",  (()=>this.<xsl:value-of select="$pn"/>, x => this.<xsl:value-of select="$pn"/>	= x));<xsl:text/>
				</xsl:for-each>
				<!-- this.Get(cmd, "Id",  (()=>this.Id, x => this.Id = x)); -->
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
<xsl:apply-templates select="ParameterType" mode="cs.wrapper" />
<xsl:apply-templates select="Result" mode="cs.wrapper" />

		<xsl:value-of select="concat($nl1, $t1, '};', $nl2)" />
	</xsl:template>
	<!--=======================================================================-->
	<!--process an Property node for wrapper -->
	<!--=======================================================================-->
	<xsl:template match="PropertyType" mode="cs.wrapper">
		<xsl:variable name="name" select="@name" />
		<xsl:call-template name="cs.summary" >
			<xsl:with-param name="indent" select="$t2" />
		</xsl:call-template>
		<xsl:value-of select="concat($t2, 'public ', cs:data-type(.), ' ', $name, ' {', $nl1)" />
		<xsl:value-of select="concat($t3, 'get =&gt; this.', @type, '[&quot;', @name, '&quot;];', $nl1)" />
		<xsl:value-of select="concat($t3, 'set =&gt; this.', @type, '[&quot;', @name, '&quot;] = value;', $nl1)" />
		<xsl:value-of select="concat($t2, '}', $nl1)" />
	</xsl:template>
	<!--=======================================================================-->
	<!--process an Parameter node for wrapper -->
	<!--=======================================================================-->
	<xsl:template match="ParameterType" mode="cs.wrapper">
		<!-- <xsl:message select="concat('process ParameterType mode cs.wrapper ', @name)"/> -->
		<xsl:variable name="name" select="@name" />
		<!-- data type -->
		<xsl:variable name="dataType" as="xs:string" select="cs:data-type(.)"/>
		<!-- access name -->
		<xsl:variable name="accessName" as="xs:string" select="cs:access-name(.)"/>
		<xsl:call-template name="cs.summary" >
			<xsl:with-param name="indent" select="$t2" />
		</xsl:call-template>
		<xsl:value-of select="concat($t2, 'public ', $dataType, ' ', $name, ' { get; set; }', $nl1)" />
	</xsl:template>
	<!--=======================================================================-->
	<!--process the Result node for wrapper -->
	<!--=======================================================================-->
	<xsl:template match="Result" mode="cs.wrapper">
		<xsl:call-template name="cs.summary" >
			<xsl:with-param name="indent" select="$t2" />
		</xsl:call-template>
		public <xsl:value-of select="cs:data-type(.)"/>
		Result { get; set; }<xsl:text/>
	</xsl:template>
	<!--=======================================================================-->
	<!--create the Impl, if not exists -->
	<!--=======================================================================-->
	<xsl:template match="CommandWrapper" mode="cs.wrapper.impl">
		<!-- <xsl:message select="concat('process CommandWrapper mode cs.wrapper.impl ', @name)"/> -->
		<xsl:variable name="name" select="concat(@name, 'Wrapper')" />
		<!-- file name -->
		<xsl:variable name="fn" select="concat(@name, '.cs')" />
		<!-- <xsl:message select="$fn" /> -->
		<!-- delimiter -->
		<xsl:variable name="d" select="wc:path-delimiter(.)"/>
		<!-- path tokens -->
		<xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
		<!-- <xsl:variable name="path" select="replace(replace($uri, $dnp, $dn), $fnp, $fn)"/> -->
		<xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Impl', $fn), $d)"/>
		<xsl:if test="not(unparsed-text-available($filePath, 'utf-8'))">
			<xsl:message select="concat( $filePath, ' created')" />
			<xsl:result-document href="{$filePath}" format="text-def">
				<xsl:variable name="base" select="./Base/@name" />
				<xsl:variable name="dto" select="./DTO/@name"/>
				<xsl:value-of select="concat('using System;', $nl1)" />
				<xsl:value-of select="concat('using System.Reflection;', $nl1)" />
				<xsl:value-of select="concat('using System.Collections.Generic;', $nl2)" />
				<xsl:value-of select="concat('namespace wEBcMD', $nl1)" />
				<xsl:value-of select="concat('{', $nl1)" />
				<xsl:value-of select="concat($t1, 'public partial class ', $name, ' : ', $base)" />
				<xsl:value-of select="concat($nl1, $t1, '{', $nl1)" />
				<!--
   			<xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Execute the command', '&lt;/summary&gt;', $nl1)" />
				<xsl:value-of select="concat($t2, 'public partial ', $dto, ' ExecuteCommand()', $nl1)" />
				<xsl:value-of select="concat($t2, '{', $nl1)" />
				<xsl:value-of select="concat($t3, 'Log.Trace($&quot;Implementation in {MethodBase.GetCurrentMethod()}&quot;);', $nl1)" />
				<xsl:value-of select="concat($t3, 'return Cmd;', $nl1)" />
				<xsl:value-of select="concat($t2, '}', $nl1)" />
				 -->
				<xsl:call-template name="cs.summary">
					<xsl:with-param name="indent" select="$t2"/>
				</xsl:call-template>
		public partial <xsl:value-of select="cs:result-type(.)"/>
		<xsl:text> </xsl:text>
		<xsl:value-of select="@name"/>(<xsl:text/>
		<xsl:value-of select="cs:param-list(.)"/>)
		{
			Log.Trace($&quot;Implementation in {MethodBase.GetCurrentMethod()}&quot;);
			return default;
		}
<xsl:text/>
				<xsl:value-of select="concat($t1, '};', $nl2)" />
				<xsl:value-of select="concat('}', $nl1)" />
			</xsl:result-document>
		</xsl:if>
	</xsl:template>
	<!--=======================================================================-->
	<!--process the Summary node or Attribute for csharp -->
	<!--=======================================================================-->
	<xsl:template name="cs.summary">
		<xsl:param name="indent" />
		<xsl:choose>
			<xsl:when test="./Summary">
				<!-- <xsl:variable name="summary" select="replace(./Summary, $nl1,concat($nl1, '///'))"></xsl:variable> -->
				<xsl:value-of select="concat($indent, '/// &lt;summary&gt;')"/>
				<xsl:for-each select="tokenize(./Summary, $nl1)">
					<xsl:value-of select="concat($nl1, $indent, '/// ', normalize-space(.))"/>
				</xsl:for-each>
				<xsl:value-of select="concat($nl1, $indent, '/// &lt;/summary&gt;', $nl1)"/>
			</xsl:when>
			<xsl:when test="./@summary">
				<xsl:value-of select="concat($indent, '/// &lt;summary&gt;', ./@summary, '&lt;/summary&gt;', $nl1)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="concat($indent, '/// &lt;summary&gt;', ./@name, '&lt;/summary&gt;', $nl1)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--=======================================================================-->
	<!-- The dispatcher for alle commands -->
	<!--=======================================================================-->
	<xsl:template name="cs.command.dispatcher">
		<xsl:variable name="filePath" select="wc:join-path(., 2, tokenize('Controllers/CommandController.cs', '/'))"/>
		<xsl:variable name="lines" select="unparsed-text-lines($filePath)"/>
		<!-- <xsl:message select="concat('lines: ', count($lines))"/> -->
		<xsl:variable name="name" select="wc:file-name(.)"/>
		<xsl:variable name="regex" select="concat($name, '\.Dispatch')"/>
		<xsl:if test="not($lines[matches(., $regex)])">
			<xsl:message select="'must work :-('"/>
			<xsl:result-document href="{$filePath}" format="text-def">
				<xsl:for-each select="$lines">
					<!-- <xsl:message select="concat('pos: ', position())"/> -->
					<xsl:choose>
						<xsl:when test="contains(.,'NEW DISPATCHERS INSERTED HERE')">
							<xsl:text>			</xsl:text>result = <xsl:value-of select="$name"/>.Dispatch(cmd);<xsl:value-of select="'&#xA;'"/>
							<xsl:text/>			if(null != result)<xsl:value-of select="'&#xA;'"/>
							<xsl:text/>				return result;<xsl:value-of select="'&#xA;'"/>
							<xsl:value-of select="concat('&#xA;', ., '&#xA;')"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="concat(., '&#xA;')"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</xsl:result-document>
		</xsl:if>
	</xsl:template>
	<!--=======================================================================-->
	<!-- The types for alle commands -->
	<!--=======================================================================-->
	<xsl:template name="cs.command.types">
		<xsl:variable name="filePath" select="wc:join-path(., 2, tokenize('Controllers/CommandController.cs', '/'))"/>
		<xsl:variable name="lines" select="unparsed-text-lines($filePath)"/>
		<!-- <xsl:message select="concat('lines: ', count($lines))"/> -->
		<xsl:variable name="name" select="wc:file-name(.)"/>
		<xsl:variable name="regex" select="concat($name, '\.GetTypes')"/>
		<xsl:if test="not($lines[matches(., $regex)])">
			<!-- <xsl:message select="'must work :-('"/> -->
			<xsl:result-document href="{$filePath}" format="text-def">
				<xsl:for-each select="$lines">
					<!-- <xsl:message select="concat('pos: ', position())"/> -->
					<xsl:choose>
						<xsl:when test="contains(.,'NEW COMMANDTYPEGETTERS INSERTED HERE')">
							<xsl:value-of select="'         '"/><xsl:text/>
							<xsl:value-of select="$name"/>.GetTypes(ref commandTypes);
							<xsl:text/>
							<xsl:value-of select="concat('&#xA;', ., '&#xA;')"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="concat(., '&#xA;')"/>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</xsl:result-document>
		</xsl:if>
	</xsl:template>
	<!--=======================================================================-->
	<!-- get a path, relative from base uri -->
	<!--=======================================================================-->
	<xsl:function name="wc:join-path" as="xs:string">
		<xsl:param name="node" as="node()"/>
		<xsl:param name="backwards" as="xs:integer"/>
		<xsl:param name="subpath" as="xs:string*"/>
		<xsl:variable name="d" select="wc:path-delimiter($node)"/>
		<xsl:variable name="pt" select="tokenize(base-uri($node), $d)"/>
		<xsl:variable name="path" select="string-join((subsequence($pt, 1,count($pt) - $backwards), $subpath), $d)"/>
		<xsl:message select="concat('join-path: ', $path)"/>
		<xsl:value-of select="$path"/>
	</xsl:function>
   <!--=======================================================================-->
   <!-- build a csharp parameter list -->
   <!--=======================================================================-->
   <xsl:function name="cs:param-list">
      <!-- wrapper node -->
      <xsl:param name="wrapper" as="node()"/>
      <xsl:variable name="params" select="$wrapper/ParameterType"/>
      <xsl:variable name="result" >
         <xsl:for-each select="$params">
            <xsl:if test="position()>1">, </xsl:if>
            <xsl:value-of select="cs:data-type(.)"/>
            <xsl:text> </xsl:text>
            <xsl:value-of select="wc:camelCaseWord(@name)"/>
         </xsl:for-each>
      </xsl:variable>
      <xsl:value-of select="$result"/>
   </xsl:function>
	<!--=======================================================================-->
	<!-- get result type for csharp -->
	<!--=======================================================================-->
	<xsl:function name="cs:result-type" as="xs:string">
		<xsl:param name="ot" as="node()"/>
		<!-- <xsl:variable name="result" select="$ot/ParameterType[lower-case(@name)='result']"/> -->
		<xsl:variable name="result" select="$ot/Result"/>
		<xsl:choose>
			<xsl:when test="$result">
				<xsl:value-of select="cs:data-type($result)"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>void</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:function>
	<!--=======================================================================-->
	<!-- Evaluate the parameters access name for C# -->
	<!--=======================================================================-->
	<xsl:function name="cs:access-name" as="xs:string">
		<xsl:param name="pt" as="node()"/>
		<xsl:variable name="dataType" as="xs:string" select="cs:data-type($pt)" />
		<xsl:variable name="name" as="xs:string" select="$pt/@name"/>
		<!-- <xsl:value-of select="concat('_', wc:camelCaseWord($name))"/> -->
		<xsl:variable name="accessName">
			<xsl:choose>
				<xsl:when test="starts-with($dataType, 'List&lt;')">
					<xsl:value-of select="concat('_', wc:camelCaseWord($name))"/>
				</xsl:when>
				<xsl:when test="ends-with($dataType, 'DTO')">
					<xsl:value-of select="concat('_', wc:camelCaseWord($name))"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="concat('_', wc:camelCaseWord($dataType))"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$accessName"/>
	</xsl:function>
	<!--=======================================================================-->
	<!-- Evaluate the dataType for C# -->
	<!--=======================================================================-->
	<xsl:function name="cs:data-type" as="xs:string">
		<xsl:param name="pt" as="node()"/>
		<xsl:variable name="isList" as="xs:boolean" select="ends-with($pt/@type, '[]')"/>
		<xsl:variable name="type" select="replace($pt/@type, '\[\]', '')" />
		<!-- <xsl:if test="ends-with($pt/@type, '[]')">
           <xsl:text></xsl:text>
           </xsl:if> -->
		<xsl:variable name="cs-type" as="xs:string">
			<xsl:choose>
				<xsl:when test="$type='UuId'">Guid</xsl:when>
				<xsl:when test="$type!=''">
					<xsl:value-of select="$type" />
				</xsl:when>
				<xsl:otherwise>void</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$isList">
				<xsl:value-of select="concat('List&lt;', $cs-type, '&gt;')"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$cs-type"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:function>
	<!--=======================================================================-->
	<!--=======================================================================-->
	<!--                                                                       -->
	<!--                                                                       -->
	<!--                          typescript                                   -->
	<!--                                                                       -->
	<!--                                                                       -->
	<!--=======================================================================-->
	<!--=======================================================================-->
	<!--=======================================================================-->
	<!-- Create extend code for derived classes -->
	<!--=======================================================================-->
	<xsl:function name="ts:extends" as="xs:string">
		<xsl:param name="ot" as="node()"/>
		<xsl:variable name="base-type" as="xs:string" select="$ot/Base/@name"/>
		<xsl:choose>
			<xsl:when test="$base-type">
				<xsl:value-of select="concat(' extends ', $base-type)"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:function>
	<!--=======================================================================-->
   <!-- TypeScript DTOs -->
   <!--=======================================================================-->
   <xsl:template match="ObjectType" mode="ts.api.dto">
      <!-- file name -->
      <xsl:variable name="fn" select="concat(@name, '.ts')" />
      <!-- delimiter -->
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <!-- path tokens -->
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'ClientApp', 'src', 'api', $fn), $d)"/>
      <xsl:message select="$filePath" />
      <xsl:variable name="base-type" as="xs:string" select="Base/@name"/>
      <!-- <xsl:if test="not(unparsed-text-available($filePath)) or $base-type"> -->
      <xsl:result-document href="{$filePath}" format="text-def">
         <!-- Collect all DTO types without [] -->
         <xsl:variable name="property-types" as="xs:string *">
            <xsl:for-each select="PropertyType[matches(@type, '.+DTO(\[\])?')]">
               <xsl:sequence select="replace(@type, '\[\]', '')"/>
            </xsl:for-each>
         </xsl:variable>
         <!-- All necessary imports, each only once -->
         <xsl:value-of select="concat('import { Guid} from &quot;guid-typescript&quot;;', $nl1)"/>
         <xsl:for-each select="distinct-values(($property-types, $base-type))" >
            <!-- <xsl:message select="."/> -->
            <xsl:variable name="type" as="xs:string" select="."/>
            <xsl:if test="$type">
               <xsl:value-of select="concat('import { ', $type, ' } from &quot;./', $type, '&quot;;', $nl1)"/>
            </xsl:if>
         </xsl:for-each>
         <!--  -->
         <xsl:value-of select="$nl1"/>
         <xsl:call-template name="ts.summary"/>
         <xsl:value-of select="concat('export class ', @name, ts:extends(.), ' {', $nl2)"/>
         <!--  -->
         <xsl:value-of select="concat($t1, '/** ', @id, ' is the Id of ', @name, ' type. */', $nl1)" />
         <xsl:value-of select="concat($t1, 'static get TypeId(): Guid { return Guid.parse(&quot;', @id, '&quot;); }', $nl2)" />
         <!--  -->
         <xsl:apply-templates select="PropertyType" mode="ts.api.dto"/>
         <xsl:value-of select="concat('', '};')"/>
      </xsl:result-document>
      <!-- </xsl:if> -->
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript DTO Properties -->
   <!--=======================================================================-->
   <xsl:template match="PropertyType" mode="ts.api.dto">
      <xsl:call-template name="ts.summary">
         <xsl:with-param name="indent" select="$t1" />
      </xsl:call-template>
      <xsl:value-of select="concat($t1, @name, '?: ', ts:dto-data-type(.))"/>
      <xsl:if test="@default">
         <xsl:value-of select="concat(' = ', @default)"/>
      </xsl:if>
      <xsl:value-of select="concat(';', $nl1)"/>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript object wrapper -->
   <!--=======================================================================-->
   <xsl:template match="CommandWrapper" mode="ts.impl.wrapper">
      <!-- file name -->
      <xsl:variable name="fn" select="concat(@name, '.ts')" />
      <!-- delimiter -->
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <!-- path tokens -->
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'ClientApp', 'src', 'impl', $fn), $d)"/>
      <!-- <xsl:if test="not(unparsed-text-available($filePath))"> -->
      <xsl:message select="concat($filePath, ' generated')" />
      <xsl:result-document href="{$filePath}" format="text-def">
         <!-- Collect all DTO types without [] -->
         <xsl:variable name="param-types" as="xs:string *">
            <xsl:for-each select="ParameterType[matches(@type, '.+DTO(\[\])?')]">
               <xsl:sequence select="replace(@type, '\[\]', '')"/>
            </xsl:for-each>
         </xsl:variable>
         <xsl:variable name="base-type" as="xs:string" select="Base/@name"/>
         <!-- All necessary imports, each only once -->
         <xsl:value-of select="concat('import { Guid} from &quot;guid-typescript&quot;;', $nl1)"/>
         <xsl:for-each select="distinct-values(($param-types))" >
            <!-- <xsl:message select="."/> -->
            <xsl:variable name="type" as="xs:string" select="."/>
            <xsl:if test="$type">
               <xsl:value-of select="concat('import { ', $type, ' } from &quot;../api/', $type, '&quot;;', $nl1)"/>
            </xsl:if>
         </xsl:for-each>
         <xsl:value-of select="concat('import { ', @name, 'Base } from &quot;../api/', @name, 'Base&quot;;', $nl1)"/>
         <xsl:value-of select="concat('import { CommandDTO } from &quot;../api/CommandDTO&quot;;', $nl1)"/>
         <!--  -->
         <xsl:value-of select="$nl1"/>
         <xsl:call-template name="ts.summary"/>
         <xsl:value-of select="concat('export class ', @name, ' extends ', @name, 'Base {', $nl2)"/>
         <xsl:value-of select="concat($t1, 'constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : ', @name, 'Base.TypeId)}', $nl2)"/>
         <xsl:value-of select="concat('', '};')"/>
      </xsl:result-document>
      <!-- </xsl:if> -->
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript access wrappers -->
   <!--=======================================================================-->
   <xsl:template match="CommandWrapper" mode="ts.api.base">
      <!-- file name -->
      <xsl:variable name="fn" select="concat(@name, 'Base.ts')" />
      <!-- delimiter -->
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <!-- path tokens -->
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'ClientApp', 'src', 'api', $fn), $d)"/>
      <xsl:message select="concat($filePath, ' generated')" />
      <xsl:result-document href="{$filePath}" format="text-def">
         <!-- Collect all DTO types without [] -->
         <xsl:variable name="property-types" as="xs:string *">
            <xsl:for-each select="ParameterType[matches(@type, '.+DTO(\[\])?')]|Result[matches(@type, '.+DTO(\[\])?')]">
               <xsl:sequence select="replace(@type, '\[\]', '')"/>
            </xsl:for-each>
         </xsl:variable>
			<xsl:message select="$property-types"/>
         <xsl:variable name="base-type" as="xs:string" select="Base/@name"/>
         <!-- <xsl:variable name="res-type" as="xs:string" select="ts:result-type(.)"/> -->
        <!-- All necessary imports, each only once -->
         <xsl:value-of select="concat('import { Guid } from &quot;guid-typescript&quot;;', $nl1)"/>
         <xsl:value-of select="concat('import { equalsGuid } from &quot;src/utils&quot;;', $nl1)"/>
         <xsl:for-each select="distinct-values($property-types)" >
            <!-- <xsl:message select="."/> -->
            <xsl:variable name="type" as="xs:string" select="."/>
            <xsl:if test="$type">
               <xsl:value-of select="concat('import { ', $type, ' } from &quot;./', $type, '&quot;;', $nl1)"/>
            </xsl:if>
         </xsl:for-each>
         <xsl:value-of select="concat('import { ', $base-type, ' } from &quot;../impl/', $base-type, '&quot;;', $nl1)"/>
         <xsl:value-of select="concat('import { CommandDTO } from &quot;./CommandDTO&quot;;', $nl1)"/>
         <!--  -->
         <xsl:value-of select="$nl1"/>
         <xsl:call-template name="ts.summary"/>
         <xsl:value-of select="concat('export class ', @name, 'Base ', ts:extends(.), ' {', $nl2)"/>
         <xsl:value-of select="concat($t1, 'constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : ', @name, 'Base.TypeId)}', $nl2)"/>
         <!--  -->
         <xsl:value-of select="concat($t1, '/** ', @id, ' is the Id of ', @name, ' type. */', $nl1)" />
         <xsl:value-of select="concat($t1, 'static get TypeId(): Guid { return Guid.parse(&quot;', @id, '&quot;); }', $nl2)" />
         <!--  -->
         <xsl:value-of select="concat($t1, '/** ', 'Checks if the type of the DTO fits', ' */', $nl1)" />
         <xsl:value-of select="concat($t1, 'static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, ', @name, 'Base.TypeId); }', $nl2)" />
         <!-- static isForMe(dto: CommandDTO) { return dto.Type === SampleCommandBase.TypeId; } -->
         <xsl:apply-templates select="ParameterType" mode="ts.api.base"/>
         <xsl:apply-templates select="Result" mode="ts.api.base"/>
         <xsl:variable name="resultType" as="xs:string" select="ts:result-type(.)"/>
   /// &lt;summary&gt;Calls the command&lt;/summary&gt;<xsl:text/>
   execute(<xsl:value-of select="ts:param-list(.)"/>): <xsl:text/>
      <xsl:text/>Promise&lt;<xsl:value-of select="ts:result-type(.)"/>|undefined&gt; {<xsl:text/>
      <xsl:apply-templates select="ParameterType" mode="ts.api.access.execute"/>
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return<xsl:if test="$resultType!='void'"> new <xsl:value-of select="@name"/>Base(cmd).Result</xsl:if>;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise&lt;<xsl:value-of select="$resultType"/>&gt;(null);
      });
   }<xsl:value-of select="concat($nl1, '};')"/>
      </xsl:result-document>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript access Parameters -->
   <!--=======================================================================-->
   <xsl:template match="ParameterType" mode="ts.api.base">
      <xsl:call-template name="ts.summary">
         <xsl:with-param name="indent" select="$t1" />
      </xsl:call-template>
      <xsl:call-template name="ts.api.base.getter"/>
      <xsl:if test="not(lower-case(@modifier)='out')">
         <xsl:call-template name="ts.api.base.setter"/>
      </xsl:if>
   </xsl:template>
   <!--=======================================================================-->
   <!--process the Result node for wrapper -->
   <!--=======================================================================-->
   <xsl:template match="Result" mode="ts.api.base">
      <xsl:call-template name="ts.summary" >
         <xsl:with-param name="indent" select="$t2" />
      </xsl:call-template>
      <xsl:call-template name="ts.api.base.getter"/>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript access getter -->
   <!--=======================================================================-->
   <xsl:template name="ts.api.base.getter">
      <xsl:variable name="name">
         <xsl:call-template name="ts.api.base.paramName"/>
      </xsl:variable>
      <xsl:variable name="dataType" as="xs:string" select="ts:wrapper-data-type(.)"/>
      <xsl:value-of select="concat($t1, 'get ', $name, '() : ', $dataType, '|undefined {', $nl1)"/>
      <xsl:variable name="paramName" as="xs:string" select="wc:camelCaseWord($name)"/>
      <xsl:value-of select="concat($t2, 'let ', $paramName, ' = this.getArgument(&quot;', $name, '&quot;);', $nl1)"/>
      <xsl:choose>
         <xsl:when test="$dataType='boolean'">
            <xsl:value-of select="concat($t2, 'if (!', $paramName, ')', $nl1)"/>
            <xsl:value-of select="concat($t3, 'return false;', $nl1)"/>
            <xsl:value-of select="concat($t2, 'return Boolean(JSON.parse(', $paramName, '));', $nl1)"/>
         </xsl:when>
         <xsl:when test="$dataType='Guid'">
            <xsl:value-of select="concat($t2, 'if (!', $paramName, ')', $nl1)"/>
            <xsl:value-of select="concat($t3, 'return Guid.parse(Guid.EMPTY);', $nl1)"/>
            <xsl:value-of select="concat($t2, 'return Guid.parse(', $paramName, ');', $nl1)"/>
         </xsl:when>
         <xsl:when test="matches($dataType,'.+DTO')">
            <xsl:value-of select="concat($t2, 'if (!', $paramName, ')', $nl1)"/>
            <xsl:value-of select="concat($t3, 'return undefined;', $nl1)"/>
            <xsl:value-of select="concat($t2, 'return JSON.parse(', $paramName, ')', ' as ', $dataType, ' ;', $nl1)"/>
         </xsl:when>
         <xsl:otherwise>
            <xsl:value-of select="concat($t3, 'return ', $paramName,';', $nl1)"/>
         </xsl:otherwise>
      </xsl:choose>
      <xsl:value-of select="concat($t1, '}', $nl1)"/>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript access setter -->
   <!--=======================================================================-->
   <xsl:template name="ts.api.base.setter">
      <xsl:variable name="name">
         <xsl:call-template name="ts.api.base.paramName"/>
      </xsl:variable>
      <xsl:variable name="dataType" as="xs:string" select="ts:wrapper-data-type(.)"/>
      <xsl:value-of select="concat($t1, 'set ', $name, '( val : ', $dataType, ') {', $nl1)"/>
      <xsl:choose>
         <xsl:when test="matches($dataType,'(boolean)|(Guid)')">
            <xsl:value-of select="concat($t2, 'this.setArgument(&quot;', $name, '&quot;, val.toString());', $nl1)"/>
         </xsl:when>
         <xsl:when test="matches($dataType,'.+DTO')">
            <xsl:value-of select="concat($t2, 'this.setArgument(&quot;', $name, '&quot;, JSON.stringify(val));', $nl1)"/>
         </xsl:when>
         <xsl:otherwise>
            <xsl:value-of select="concat($t2, 'this.setArgument(&quot;', $name, '&quot;, val);', $nl1)"/>
         </xsl:otherwise>
      </xsl:choose>
      <xsl:value-of select="concat($t1, '}', $nl2)"/>
   </xsl:template>
	<!--=======================================================================-->
	<!-- build a typescript parameter list -->
	<!--=======================================================================-->
	<xsl:function name="ts:param-list">
		<!-- wrapper node -->
		<xsl:param name="wrapper" as="node()"/>
		<xsl:variable name="params" select="$wrapper/ParameterType"/>
		<xsl:variable name="result" >
			<xsl:for-each select="$params">
				<xsl:if test="position()>1">, </xsl:if>
				<xsl:value-of select="wc:camelCaseWord(@name)"/>
				<xsl:text>: </xsl:text>
				<xsl:value-of select="ts:wrapper-data-type(.)"/>
			</xsl:for-each>
		</xsl:variable>
		<xsl:value-of select="$result"/>
	</xsl:function>
	<xsl:template name="ts.api.base.paramName" as="xs:string">
      <xsl:choose>
         <xsl:when test="@name">
            <xsl:value-of select="@name"/>
         </xsl:when>
         <xsl:otherwise>
            <xsl:value-of select="name()"/>
         </xsl:otherwise>
      </xsl:choose>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript assignment of the passed values -->
   <!--=======================================================================-->
   <xsl:template match="ParameterType" mode="ts.api.access.execute">
      this.<xsl:value-of select="@name"/> = <xsl:value-of select="wc:camelCaseWord(@name)"/>;<xsl:text/>
   </xsl:template>
   <!--=======================================================================-->
   <!--process the Summary node or Attribute for typescript-->
   <!--=======================================================================-->
   <xsl:template name="ts.summary">
      <xsl:param name="indent" select="''"/>
      <xsl:value-of select="concat($indent, '/**')"/>
      <xsl:choose>
         <xsl:when test="./Summary">
            <xsl:for-each select="tokenize(./Summary, $nl1)">
               <xsl:if test="normalize-space(.)">
                  <xsl:value-of select="concat($nl1, $indent, ' * ', .)"/>
               </xsl:if>
            </xsl:for-each>
            <xsl:value-of select="concat($nl1, $indent, ' */', $nl1)"/>
         </xsl:when>
         <xsl:when test="./@summary">
            <xsl:value-of select="concat(' ', ./@summary, ' */', $nl1)" />
         </xsl:when>
         <xsl:otherwise>
            <xsl:value-of select="concat(' ', ./@name, ' */', $nl1)" />
         </xsl:otherwise>
      </xsl:choose>
   </xsl:template>
	<!--=======================================================================-->
	<!-- get result type for typescript -->
	<!--=======================================================================-->
	<xsl:function name="ts:result-type" as="xs:string">
		<xsl:param name="ot" as="node()"/>
		<!-- <xsl:variable name="result" select="$ot/ParameterType[lower-case(@name)='result']"/> -->
		<xsl:variable name="result" select="$ot/Result"/>
		<xsl:choose>
			<xsl:when test="$result">
				<xsl:value-of select="ts:wrapper-data-type($result)"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>void</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:function>
	<!--=======================================================================-->
	<!-- Evaluate the dataType for TypeScript -->
	<!--=======================================================================-->
	<xsl:function name="ts:wrapper-data-type">
		<xsl:param name="pt" as="node()"/>
		<xsl:choose>
			<xsl:when test="$pt/@type='Boolean'">boolean</xsl:when>
			<xsl:when test="$pt/@type='Int32'">number</xsl:when>
			<xsl:when test="$pt/@type='UuId'">Guid</xsl:when>
			<xsl:when test="matches($pt/@type, 'DTO(\[\])?$')">
				<xsl:value-of select="$pt/@type"/>
			</xsl:when>
			<xsl:otherwise>string</xsl:otherwise>
		</xsl:choose>
	</xsl:function>
	<!--=======================================================================-->
	<!-- Evaluate the DTO-data type for TypeScript -->
	<!--=======================================================================-->
	<xsl:function name="ts:dto-data-type">
		<xsl:param name="pt" as="node()"/>
		<xsl:choose>
			<xsl:when test="$pt/@type='Boolean'">boolean</xsl:when>
			<xsl:when test="$pt/@type='Int32'">number</xsl:when>
			<!-- <xsl:when test="$pt/@type='UuId'">Guid</xsl:when> -->
			<xsl:when test="matches($pt/@type, 'DTO(\[\])?$')">
				<xsl:value-of select="$pt/@type"/>
			</xsl:when>
			<xsl:otherwise>string</xsl:otherwise>
		</xsl:choose>
	</xsl:function>
	<!--=======================================================================-->
	<!--=======================================================================-->
	<!--                                                                       -->
	<!--                                                                       -->
	<!--                           markdown                                    -->
	<!--                                                                       -->
	<!--                                                                       -->
	<!--=======================================================================-->
	<!--=======================================================================-->
	<!--=======================================================================-->
   <!-- The Entry in Readme, if not exist -->
   <!--=======================================================================-->
   <xsl:template name="md.command.readme">
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="fn" select="'README.md'" />
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Types', $fn), $d)"/>
      <xsl:variable name="lines" select="unparsed-text-lines($filePath)"/>
      <xsl:variable name="name" select="wc:file-name(.)"/>
      <xsl:variable name="regex" select="concat('- \[',$name, '\]\(\.\./Doc/Types/', $name, '\.md\)')"/>
      <xsl:if test="not($lines[matches(., $regex)])">
         <xsl:message select="concat('not found ', $regex)"/>
         <xsl:result-document href="{$filePath}" format="text-def">
            <xsl:for-each select="$lines">
               <xsl:choose>
                  <xsl:when test="contains(.,'HERE INSERT DOCUMENT LINK')">
<xsl:value-of select="concat('- [', $name, '](../Doc/', $name, '.md)&#xA;&#xA;')"/>
                     <xsl:message select="concat('insert ', .)"/>
<xsl:value-of select="concat(., '&#xA;')"/>
                  </xsl:when>
                  <xsl:otherwise>
                     <xsl:value-of select="concat(., '&#xA;')"/>
                  </xsl:otherwise>
               </xsl:choose>
            </xsl:for-each>
         </xsl:result-document>
      </xsl:if>
      <xsl:variable name="mdFilePath" select="concat(string-join((subsequence($pt, 1,count($pt) - 2), 'Doc/Types', wc:file-name(.)), $d), '.md')"/>
      <xsl:message select="$mdFilePath"/>
      <xsl:result-document href="{$mdFilePath}" format="text-def">

[wEBcMD Documentation](../README.md)

[wEBcMD Types](../../Types/README.md)

## <xsl:value-of select="wc:file-name(.)"/> Documentation

      <xsl:variable name="customMdFilePath" select="concat(string-join((subsequence($pt, 1,count($pt) - 2), 'Doc/Types', wc:file-name(.)), $d), wc:file-name(.), '.md')"/>
      <xsl:message select="concat('search for the file ', $customMdFilePath)"/>
      <xsl:choose>
         <xsl:when test="doc-available($customMdFilePath)">

[<xsl:value-of select="wc:file-name(.)"/> Documentation](<xsl:value-of select="$customMdFilePath"/>)

         </xsl:when>
         <xsl:otherwise>

### Serverside Classes for <xsl:value-of select="wc:file-name(.)"/>

![Serverside Classes for <xsl:value-of select="wc:file-name(.)"/>](<xsl:value-of select="concat('./cs/', wc:file-name(.), '.svg')"/>)

### Clientside Classes for <xsl:value-of select="wc:file-name(.)"/>
         <xsl:variable name="commands" select="Types/CommandWrapper"/>
         <!-- for all commands we serach the call in lines -->
         <xsl:for-each select="$commands">

#### Classes for <xsl:value-of select="@name"/>

![Classes for <xsl:value-of select="@name"/>](<xsl:value-of select="concat('./ts/', @name, '.svg')"/>)
         </xsl:for-each>
        </xsl:otherwise>
      </xsl:choose>
      </xsl:result-document>
   </xsl:template>
   <!--=======================================================================-->
   <!-- The cmd file to generate the plantuml diagrams -->
   <!--=======================================================================-->
   <xsl:template name="ts.generate.diagrams.cmd">
      <xsl:message select="base-uri(.)"/>
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Doc', 'generate-ts-diagrams.cmd'), $d)"/>
      <xsl:variable name="lines" select="unparsed-text-lines($filePath)"/>
      <xsl:message select="$filePath"/>
      <xsl:message select="concat('lines: ', count($lines))"/>
      <xsl:result-document href="{$filePath}" format="text-def">
         <!-- first we coppy all lines -->
         <xsl:for-each select="$lines">
            <xsl:value-of select="concat(., '&#xA;')"/>
         </xsl:for-each>
         <xsl:variable name="commands" select="Types/CommandWrapper"/>
         <!-- for all commands we serach the call in lines -->
         <xsl:for-each select="$commands">
            <xsl:variable name="regex" select="concat('.+', @name, 'Base\.ts.+')"/>
            <xsl:message select="concat('search for ', $regex)"/>
            <!-- if we not found, add them at end of new file -->
            <xsl:if test="not($lines[matches(., $regex)])">
               <xsl:message select="'must work :-('"/>
               <xsl:text/>
echo generate typescript class diagram for <xsl:value-of select="@name"/>
call tplant --input ..\ClientApp\src\api\<xsl:value-of select="@name"/>Base.ts ..\ClientApp\src\impl\<xsl:value-of select="@name"/>.ts ..\ClientApp\src\impl\ts.CommandWrapper --output Types\ts\<xsl:value-of select="@name"/>.puml -A<xsl:text/>
            </xsl:if>
         </xsl:for-each>
      </xsl:result-document>
   </xsl:template>
   <!--=======================================================================-->
   <!-- Get the file name of base-uri without extension -->
   <!--=======================================================================-->
   <xsl:function name="wc:file-name" as="xs:string">
      <xsl:param name="node" as="node()"/>
      <!-- <xsl:variable name="d" select="wc:path-delimiter($node)"/> -->
      <xsl:variable name="pt" select="tokenize(base-uri($node), '\\|/')"/>
      <xsl:variable name="fn" as="xs:string" select="replace(subsequence($pt, count($pt), 1), '.xml', '')"/>
      <xsl:value-of select="$fn"/>
   </xsl:function>
   <!--=======================================================================-->
   <!-- Changes the passed identifier to camelCase notation -->
   <!--=======================================================================-->
   <xsl:function name="wc:camelCaseWord" as="xs:string">
      <xsl:param name="text" />
      <xsl:variable name="first" select="translate(substring($text,1,1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
      <xsl:variable name="everythingElse" select="substring($text,2,string-length($text)-1)" />
      <xsl:value-of select="concat($first, $everythingElse)"/>
   </xsl:function>
   <!--=======================================================================-->
   <!-- Changes the passed identifier to PascalCase notation -->
   <!--=======================================================================-->
   <xsl:function name="wc:pascalCase" as="xs:string">
      <xsl:param name="text" />
      <xsl:variable name="first" select="translate(substring($text,1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
      <xsl:variable name="everythingElse" select="substring($text,2,string-length($text)-1)" />
      <xsl:value-of select="concat($first, $everythingElse)"/>
   </xsl:function>
   <!--=======================================================================-->
   <!-- determines the correct delimiter for the expanded path -->
   <!--=======================================================================-->
   <xsl:function name="wc:path-delimiter" as="xs:string">
      <xsl:param name="node" as="node()"/>
      <xsl:choose>
         <xsl:when test="contains(base-uri($node), ':/')">/</xsl:when>
         <xsl:otherwise>\\</xsl:otherwise>
      </xsl:choose>
   </xsl:function>
</xsl:stylesheet>
