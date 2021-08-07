<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:filePath="http://www.w3.org/2005/02/xpath-functions"
                xmlns:xdt="http://www.w3.org/2005/02/xpath-datatypes"
                xmlns:functx="http://www.functx.com"
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
         <!-- Collect all DTO types without [] -->
         <xsl:variable name="property-types" as="xs:string *">
            <xsl:for-each select="PropertyType[matches(@type, '.+DTO(\[\])?')]">
               <xsl:sequence select="replace(@type, '\[\]', '')"/>
            </xsl:for-each>
         </xsl:variable>
         <xsl:variable name="base-type" as="xs:string" select="Base/@name"/>
         <!-- All necessary imports, each only once -->
         <xsl:for-each select="distinct-values(($property-types, $base-type))" >
            <xsl:message select="."/>
            <xsl:value-of select="concat('import { ', ., ' } from &quot;./', ., '&quot;;', $nl1)"/>
         </xsl:for-each>
         <!--  -->
         <xsl:value-of select="$nl1"/>
         <xsl:value-of select="concat('export interface ', @name, ' {', $nl1)"/>
         <xsl:apply-templates select="PropertyType" mode="dto.ts"/>
         <xsl:value-of select="concat('', '};')"/>
      </xsl:result-document>
   </xsl:template>
   <!--=======================================================================-->
   <!--  -->
   <!--=======================================================================-->
   <xsl:template match="Base" mode="import.ts">
      <xsl:variable name="type" select="@name" />
      <xsl:if test="matches($type, '.+DTO$')">
         <xsl:value-of select="concat('import { ', $type, ' } from &quot;./', $type, '&quot;;', $nl1)"/>
      </xsl:if>
   </xsl:template>
   <!--=======================================================================-->
   <!--  -->
   <!--=======================================================================-->
   <xsl:template match="PropertyType" mode="import.ts">
      <xsl:variable name="type" select="replace(@type, '\[\]', '')" />
      <xsl:if test="matches($type, '.+DTO$')">
         <xsl:value-of select="concat('import { ', $type, ' } from &quot;./', $type, '&quot;;', $nl1)"/>
      </xsl:if>
   </xsl:template>
</xsl:stylesheet>
