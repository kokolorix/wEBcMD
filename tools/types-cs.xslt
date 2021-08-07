<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:filePath="http://www.w3.org/2005/02/xpath-functions"
                xmlns:xdt="http://www.w3.org/2005/02/xpath-datatypes"
                xmlns:functx="http://www.functx.com"
                xmlns:wc="https://github.com/kokolorix/wEBcMD.git"
                xmlns:ts="https://www.typescriptlang.org/"
                xmlns:cs="https://docs.microsoft.com/en-us/dotnet/csharp/"
   >
   <xsl:output method="text" />
   <xsl:output name="text-def" method="text" />
   <xsl:output name="xml-def" method="xml" indent="yes" />
   <xsl:variable name="t1" select="'&#x9;'" />
   <xsl:variable name="t2" select="concat($t1, '&#x9;')" />
   <xsl:variable name="t3" select="concat($t2, '&#x9;')" />
   <xsl:variable name="t4" select="concat($t3, '&#x9;')" />
   <xsl:variable name="t5" select="concat($t4, '&#x9;')" />
   <xsl:variable name="nl1" select="'&#xA;'" />
   <xsl:variable name="nl2" select="concat($nl1, '&#xA;')" />
   <!--=======================================================================-->
   <!-- C# DTOs, wrappers and partially command implementations -->
   <!-- TS DTOs and wrappers with lazy evaluation of DTO properties -->
   <!--=======================================================================-->
   <xsl:template match="/">
      <xsl:variable name="filePath" select="replace(base-uri(.),'.xml' ,'.cs')" />
      <xsl:message select="$filePath" />
      <xsl:result-document href="{$filePath}" format="text-def">
           <xsl:value-of select="concat('using System;', $nl1)" />
           <xsl:value-of select="concat('using System.Collections.Generic;', $nl2)" />
           <xsl:value-of select="concat('namespace wEBcMD', $nl1)" />
           <xsl:value-of select="concat('{', $nl1)" />
           <xsl:apply-templates select="Types/ObjectType" mode="dto.cs" />
           <xsl:apply-templates select="Types/ObjectWrapper" mode="dto.cs" />
           <xsl:value-of select="concat('}', $nl1)" />
           </xsl:result-document>
           <xsl:apply-templates select="Types/ObjectWrapper" mode="wrapper.cs" />
      <xsl:apply-templates select="Types/ObjectType" mode="dto.ts"/>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript DTOs -->
   <!--=======================================================================-->
   <xsl:template match="ObjectType" mode="dto.ts">
      <!-- file name -->
      <xsl:variable name="fn" select="concat(@name, '.ts')" />
      <!-- delimiter -->
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <!-- path tokens -->
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'ClientApp', 'src', 'api', $fn), $d)"/>
      <xsl:message select="$filePath" />
      <xsl:result-document href="{$filePath}" format="text-def">
         <!-- Collect all DTO types without [] -->
         <xsl:variable name="property-types" as="xs:string *">
            <xsl:for-each select="PropertyType[matches(@type, '.+DTO(\[\])?')]">
               <xsl:sequence select="replace(@type, '\[\]', '')"/>
            </xsl:for-each>
         </xsl:variable>
         <xsl:variable name="base-type" as="xs:string" select="Base/@name"/>
         <!-- All necessary imports, each only once -->
         <xsl:for-each select="distinct-values(($property-types, $base-type))" >
            <!-- <xsl:message select="."/> -->
            <xsl:variable name="type" as="xs:string" select="."/>
            <xsl:if test="$type">
               <xsl:value-of select="concat('import { ', $type, ' } from &quot;./', $type, '&quot;;', $nl1)"/>
            </xsl:if>
         </xsl:for-each>
         <!--  -->
         <xsl:value-of select="$nl1"/>
         <xsl:value-of select="concat('export class ', @name, ts:extends(.), ' {', $nl1)"/>
         <xsl:apply-templates select="PropertyType" mode="dto.ts"/>
         <xsl:value-of select="concat('', '};')"/>
      </xsl:result-document>
   </xsl:template>
   <!--=======================================================================-->
   <!-- TypeScript Properties -->
   <!--=======================================================================-->
   <xsl:template match="PropertyType" mode="dto.ts">
      <xsl:value-of select="concat($t1, @name, '?: ', ts:data-type(.))"/>
      <xsl:value-of select="concat(';', $nl1)"/>
   </xsl:template>
   <!--=======================================================================-->
   <!--process an ObjectType node -->
   <!--=======================================================================-->
   <xsl:template match="ObjectType" mode="dto.cs">
      <xsl:variable name="category" select="@category" />
      <xsl:variable name="name" select="@name" />
      <xsl:variable name="id" select="@id" />
      <xsl:call-template name="Summary">
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
      <xsl:apply-templates select="Base" mode="dto.cs" />
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
      <xsl:value-of select="concat('Guid TypeId { get =&gt; Guid.Parse(&quot;', $id, '&quot;); }', $nl1)" />
      <!-- <xsl:if test="$name!='BaseDTO' and $name!='PropertyDTO'">
           <xsl:value-of select="concat($t2, '/// &lt;summary&gt;Id of ', $name,' type.&lt;/summary&gt;', $nl1)" />
           <xsl:value-of select="concat($t2, 'public override Guid Type { get =&gt; ', $name, '.TypeId; }', $nl1)" />
           </xsl:if> -->
      <xsl:apply-templates select="PropertyType" mode="dto.cs" />
      <xsl:value-of select="concat($t1, '};', $nl2)" />
   </xsl:template>
   <!--=======================================================================-->
   <!--process an ObjectWrapper node -->
   <!--=======================================================================-->
   <xsl:template match="ObjectWrapper" mode="dto.cs">
      <xsl:variable name="category" select="@category" />
      <xsl:variable name="name" select="@name" />
      <xsl:variable name="id" select="@id" />
      <xsl:variable name="base" select="./Base/@name" />
      <xsl:variable name="dto" select="./DTO/@name">
         <!-- <xsl:choose>
              <xsl:when test="$base = 'CommandWrapper'">
              <xsl:value-of select="CommandDTO"/>
              </xsl:when>
              <xsl:otherwise>
              <xsl:value-of select="ObjectDTO"/>
              </xsl:otherwise>
              </xsl:choose> -->
        </xsl:variable>
      <!--  -->
      <xsl:call-template name="Summary">
         <xsl:with-param name="indent" select="$t1" />
      </xsl:call-template>
      <xsl:value-of select="concat($t1, 'public partial class ', $name, ' : ', $base)" />
      <xsl:value-of select="concat($nl1, $t1, '{', $nl1)" />
      <!--  -->
      <xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Constructor of ', $name,'&lt;/summary&gt;', $nl1)" />
      <xsl:value-of select="concat($t2, 'public ', $name, '(', $dto, ' dto):base(dto){}', $nl1)" />
      <!--  -->
      <xsl:value-of select="concat($t2, '/// &lt;summary&gt;', $id, ' is the Id of ', $name,' type.&lt;/summary&gt;', $nl1)" />
      <xsl:value-of select="concat($t2, 'public static Guid TypeId { get =&gt; Guid.Parse(&quot;', $id, '&quot;); }', $nl1)" />
      <!--  -->
      <xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Checks if the type of the DTO fits', '&lt;/summary&gt;', $nl1)" />
      <xsl:value-of select="concat($t2, 'public static bool IsForMe(', $dto, ' dto) => dto.Type == ', $name, '.TypeId;', $nl1)" />
      <!--  -->
      <xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Create the wrapper and execute the command', '&lt;/summary&gt;', $nl1)" />
      <xsl:value-of select="concat($t2, 'public static ', $dto, ' ExecuteCommand(', $dto, ' dto) => new ', $name, '(dto).ExecuteCommand();', $nl1)" />
      <!--  -->
      <xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Execute the command', '&lt;/summary&gt;', $nl1)" />
      <xsl:value-of select="concat($t2, 'public partial ', $dto, ' ExecuteCommand();', $nl1)" />
      <xsl:apply-templates select="PropertyType" mode="wrapper.cs" />
      <xsl:value-of select="concat($t1, '};', $nl2)" />
   </xsl:template>
   <!--=======================================================================-->
   <!--create the Impl, if not exists -->
   <!--=======================================================================-->
   <xsl:template match="ObjectWrapper" mode="wrapper.cs">
      <xsl:variable name="name" select="@name" />
      <!-- file name -->
      <xsl:variable name="fn" select="concat($name, '.cs')" />
      <xsl:message select="$fn" />
      <!-- file name pattern -->
      <xsl:variable name="fnp" select="tokenize(base-uri(.),'/')[last()]"/>
      <xsl:message select="$fnp" />
      <!-- directory name -->
      <xsl:variable name="dn" select="'/Impl/'" />
      <!-- directory name pattern -->
      <xsl:message select="$dn" />
      <xsl:variable name="dnp" select="concat('/',tokenize(base-uri(.),'/')[last() - 1], '/')"/>
      <!-- path to impl file -->
      <xsl:message select="$dnp" />
      <xsl:variable name="uri" select="base-uri(.)" />
      <!-- delimiter -->
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <!-- path tokens -->
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <!-- <xsl:variable name="path" select="replace(replace($uri, $dnp, $dn), $fnp, $fn)"/> -->
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Impl', $fn), $d)"/>
      <xsl:if test="not(doc-available($filePath))">
         <xsl:message select="$filePath" />
         <xsl:result-document href="{$filePath}" format="text-def">
            <xsl:variable name="base" select="./Base/@name" />
            <xsl:variable name="dto" select="./DTO/@name"/>
            <xsl:value-of select="concat('using System;', $nl1)" />
            <xsl:value-of select="concat('using System.Reflection;', $nl2)" />
            <xsl:value-of select="concat('namespace wEBcMD', $nl1)" />
            <xsl:value-of select="concat('{', $nl1)" />
            <xsl:value-of select="concat($t1, 'public partial class ', $name, ' : ', $base)" />
            <xsl:value-of select="concat($nl1, $t1, '{', $nl1)" />
            <!--  -->
            <xsl:value-of select="concat($t2, '/// &lt;summary&gt;', 'Execute the command', '&lt;/summary&gt;', $nl1)" />
            <xsl:value-of select="concat($t2, 'public partial ', $dto, ' ExecuteCommand()', $nl1)" />
            <xsl:value-of select="concat($t2, '{', $nl1)" />
            <!--  -->
            <xsl:value-of select="concat($t3, 'Log.Trace($&quot;Implementation in {MethodBase.GetCurrentMethod()}&quot;);', $nl1)" />
            <xsl:value-of select="concat($t3, 'return Cmd;', $nl1)" />
            <!--  -->
            <xsl:value-of select="concat($t2, '}', $nl1)" />
            <!--  -->
            <xsl:value-of select="concat($t1, '};', $nl2)" />
            <xsl:value-of select="concat('}', $nl1)" />
         </xsl:result-document>
      </xsl:if>
   </xsl:template>
   <!--=======================================================================-->
   <!--process the Base node -->
   <!--=======================================================================-->
   <xsl:template match="Base" mode="dto.cs">
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
   <xsl:template match="PropertyType" mode="dto.cs">
      <xsl:variable name="name" select="@name" />
      <xsl:call-template name="Summary">
         <xsl:with-param name="indent" select="$t2" />
      </xsl:call-template>
      <xsl:value-of select="concat($t2, 'public virtual ', cs:data-type(.), ' ', $name)" />
      <xsl:choose>
         <xsl:when test="@fromList">
            <xsl:value-of select="concat(' {', $nl1)" />
            <xsl:value-of select="concat($t3, 'get { return ', @type, 'FromPropertyList( this.', @fromList, ', &quot;', @name, '&quot; ); }', $nl1)" />
            <xsl:value-of select="concat($t3, 'set { ', @type, 'ToPropertyList( this.', @fromList, ', &quot;', @name, '&quot;, value ); }', $nl1)" />
            <!-- <xsl:value-of select="concat($t4, 'get =&gt; this.', @fromList, '.Find(a =&gt; a.Name==&quot;', @name, '&quot;)?.Value;', $nl1)" />
                 <xsl:value-of select="concat($t4, 'set {', $nl1)" />
                 <xsl:value-of select="concat($t4, '}', $nl1)" /> -->
            <xsl:value-of select="concat($t2, '}')" />
         </xsl:when>
         <xsl:otherwise>
            <xsl:value-of select="concat(' { get; set; }', '')" />
         </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="@default">
         <xsl:value-of select="concat(' = ', @default, ';')" />
      </xsl:if>
      <xsl:value-of select="concat($nl1, '')" />
   </xsl:template>
   <!--=======================================================================-->
   <!--process an Property node for wrapper -->
   <!--=======================================================================-->
   <xsl:template match="PropertyType" mode="wrapper.cs">
      <xsl:variable name="name" select="@name" />
      <xsl:call-template name="Summary">
         <xsl:with-param name="indent" select="$t2" />
      </xsl:call-template>
      <xsl:value-of select="concat($t2, 'public ', cs:data-type(.), ' ', $name, ' {', $nl1)" />
      <xsl:value-of select="concat($t3, 'get =&gt; this.', @type, '[&quot;', @name, '&quot;];', $nl1)" />
      <xsl:value-of select="concat($t3, 'set =&gt; this.', @type, '[&quot;', @name, '&quot;] = value;', $nl1)" />
      <xsl:value-of select="concat($t2, '}', $nl1)" />
   </xsl:template>
   <!--=======================================================================-->
   <!--process the Summary node or Attribute -->
   <!--=======================================================================-->
   <xsl:template name="Summary">
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
   <!-- Evaluate the datatype for TypeScript -->
   <!--=======================================================================-->
   <xsl:function name="ts:data-type">
      <xsl:param name="pt" as="node()"/>
      <xsl:choose>
         <xsl:when test="$pt/@type='Boolean'">boolean</xsl:when>
         <xsl:when test="$pt/@type='Int32'">number</xsl:when>
         <xsl:when test="matches($pt/@type, 'DTO(\[\])?$')"><xsl:value-of select="$pt/@type"/></xsl:when>
         <xsl:otherwise>string</xsl:otherwise>
      </xsl:choose>
   </xsl:function>
   <!--=======================================================================-->
   <!-- Evaluate the datatype for C# -->
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
            <xsl:when test="$type='UuId'">
               <xsl:value-of select="concat(' ', 'Guid')" />
            </xsl:when>
            <xsl:otherwise>
               <xsl:value-of select="$type" />
            </xsl:otherwise>
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
   <!-- Changes the passed identifier to camelCase notation -->
   <!--=======================================================================-->
   <xsl:function name="wc:camelCaseWord" as="xs:string">
      <xsl:param name="text" />
      <xsl:variable name="first" select="translate(substring($text,1,1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
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
