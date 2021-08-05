<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:filePath="http://www.w3.org/2005/02/xpath-functions"
                xmlns:xdt="http://www.w3.org/2005/02/xpath-datatypes">
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
   <!--header with guards ans includes -->
   <!--=======================================================================-->
   <xsl:template match="/">
      <xsl:variable name="filePath" select="replace(base-uri(.),'.xml' ,'.cs')" />
      <!-- <xsl:message select="$filePath" /> -->
      <!-- <xsl:result-document href="{$filePath}" format="text-def">
           <xsl:value-of select="concat('using System;', $nl1)" />
           <xsl:value-of select="concat('using System.Collections.Generic;', $nl2)" />
           <xsl:value-of select="concat('namespace wEBcMD', $nl1)" />
           <xsl:value-of select="concat('{', $nl1)" />
           <xsl:apply-templates select="Types/ObjectType" mode="types" />
           <xsl:apply-templates select="Types/ObjectWrapper" mode="types" />
           <xsl:value-of select="concat('}', $nl1)" />
           </xsl:result-document>
           <xsl:apply-templates select="Types/ObjectWrapper" mode="impl" /> -->
      <xsl:apply-templates select="Types/ObjectType" mode="dto.ts"/>
   </xsl:template>
   <!--=======================================================================-->
   <!--  -->
   <!--=======================================================================-->
   <xsl:template match="ObjectType" mode="dto.ts">
      <!-- file name -->
      <xsl:variable name="fn" select="concat(@name, '.ts')" />
      <xsl:variable name="d">
         <xsl:choose>
            <xsl:when test="contains(base-uri(.), ':/')">/</xsl:when>
            <xsl:otherwise>\\</xsl:otherwise>
         </xsl:choose>
      </xsl:variable>
      <!-- path tokens -->
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'ClientApp', 'src', 'api', $fn), $d)"/>
      <xsl:message select="$filePath" />
      <xsl:result-document href="{$filePath}" format="text-def">
         <xsl:apply-templates select="PropertyType" mode="import.ts"/>
         <xsl:value-of select="$nl1"/>
         <xsl:value-of select="concat('export interface ', @name, ' {', $nl1)"/>
         <xsl:apply-templates select="PropertyType" mode="dto.ts"/>
         <xsl:value-of select="concat('', '};')"/>
      </xsl:result-document>
   </xsl:template>
   <xsl:template match="PropertyType" mode="import.ts">
      <xsl:variable name="type" select="replace(@type, '\[\]', '')" />
      <xsl:if test="matches(@type, 'DTO(\[\])?$')">
         <xsl:value-of select="concat('import { ', $type, ' } from &quot;./', $type, '&quot;;', $nl1)"/>
      </xsl:if>
   </xsl:template>
   <xsl:template match="PropertyType" mode="dto.ts">
      <xsl:value-of select="concat($t1, @name, '?: ')"/><xsl:call-template name="DataType.ts"/>
      <xsl:value-of select="concat(';', $nl1)"/>
   </xsl:template>
   <xsl:template name="DataType.ts">
      <xsl:choose>
         <xsl:when test="@type='Boolean'">boolean</xsl:when>
         <xsl:when test="@type='Int32'">number</xsl:when>
         <xsl:when test="matches(@type, 'DTO(\[\])?$')"><xsl:value-of select="@type"/></xsl:when>
         <xsl:otherwise>string</xsl:otherwise>
      </xsl:choose>
   </xsl:template>
   <!--=======================================================================-->
   <!--process an ObjectType node -->
   <!--=======================================================================-->
   <xsl:template match="ObjectType" mode="types">
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
      <xsl:apply-templates select="Base" mode="types" />
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
      <xsl:apply-templates select="PropertyType" mode="types" />
      <xsl:value-of select="concat($t1, '};', $nl2)" />
   </xsl:template>
   <!--=======================================================================-->
   <!--process an ObjectWrapper node -->
   <!--=======================================================================-->
   <xsl:template match="ObjectWrapper" mode="types">
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
      <xsl:apply-templates select="PropertyType" mode="impl" />
      <xsl:value-of select="concat($t1, '};', $nl2)" />
   </xsl:template>
   <!--=======================================================================-->
   <!--create the Impl, if not exists -->
   <!--=======================================================================-->
   <xsl:template match="ObjectWrapper" mode="impl">
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
      <!-- <xsl:variable name="path" select="replace(replace(base-uri(.), $dnp, $dn), $fnp, $filePath)"/> -->
      <!-- delimiter -->
      <xsl:variable name="d">
         <xsl:choose>
            <xsl:when test="contains(base-uri(.), '://')">/</xsl:when>
            <xsl:otherwise>\\</xsl:otherwise>
         </xsl:choose>
      </xsl:variable>
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
   <xsl:template match="Base" mode="types">
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
   <xsl:template match="PropertyType" mode="types">
      <xsl:variable name="name" select="@name" />
      <xsl:call-template name="Summary">
         <xsl:with-param name="indent" select="$t2" />
      </xsl:call-template>
      <xsl:value-of select="concat($t2, 'public')" />
      <!-- <xsl:if test="not(../Base/@Name)"> -->
      <xsl:value-of select="concat(' ', 'virtual', ' ')" />
      <!-- </xsl:if> -->
      <xsl:call-template name="DataType" />
      <xsl:value-of select="concat(' ', $name)" />
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
   <xsl:template match="PropertyType" mode="impl">
      <xsl:variable name="name" select="@name" />
      <xsl:call-template name="Summary">
         <xsl:with-param name="indent" select="$t2" />
      </xsl:call-template>
      <xsl:value-of select="concat($t2, 'public', ' ')" />
      <xsl:call-template name="DataType" />
      <xsl:value-of select="concat(' ', $name, ' {', $nl1)" />
      <xsl:value-of select="concat($t3, 'get =&gt; this.', @type, '[&quot;', @name, '&quot;];', $nl1)" />
      <xsl:value-of select="concat($t3, 'set =&gt; this.', @type, '[&quot;', @name, '&quot;] = value;', $nl1)" />
      <xsl:value-of select="concat($t2, '}', $nl1)" />
   </xsl:template>
   <!--=======================================================================-->
   <!--process DataType -->
   <!--=======================================================================-->
   <xsl:template name="DataType">
      <xsl:variable name="type" select="replace(@type, '\[\]', '')" />
      <xsl:text></xsl:text>
      <xsl:if test="ends-with(@type, '[]')">
         <xsl:text>List&lt;</xsl:text>
      </xsl:if>
      <xsl:choose>
         <xsl:when test="$type='UuId'">
            <xsl:value-of select="concat(' ', 'Guid')" />
         </xsl:when>
         <xsl:otherwise>
            <xsl:value-of select="$type" />
         </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="ends-with(@type, '[]')">
         <xsl:text>&gt;</xsl:text>
      </xsl:if>
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
   <!-- -->
   <!--=======================================================================-->
   <xsl:template name="camelCaseWord">
      <xsl:param name="text" />
      <xsl:value-of select="translate(substring($text,1,1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
      <xsl:value-of select="substring($text,2,string-length($text)-1)" />
   </xsl:template>
</xsl:stylesheet>
