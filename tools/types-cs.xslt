<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" xmlns:xdt="http://www.w3.org/2005/02/xpath-datatypes">
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
      <xsl:variable name="file" select="replace(tokenize(base-uri(.), '/')[last()],'.xml','')" />
      <xsl:variable name="fn" select="replace(base-uri(.),'.xml' ,'.cs')" />
      <xsl:result-document href="{$fn}" format="text-def">
         <xsl:value-of select="concat('using System;', $nl1)" />
         <xsl:value-of select="concat('using System.Collections.Generic;', $nl2)" />
         <xsl:value-of select="concat('namespace wEBcMD', $nl1)" />
         <xsl:value-of select="concat('{', $nl1)" />
         <xsl:apply-templates select="Types/ObjectType" mode="types" />
         <xsl:value-of select="concat('}', $nl1)" />
      </xsl:result-document>
   </xsl:template>
   <!--=======================================================================-->
   <!--process an Object node -->
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
      <xsl:apply-templates select="PropertyType" mode="types" />
      <xsl:value-of select="concat($t1, '};', $nl2)" />
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

         <!-- public virtual String FirstOne {
            get => Arguments.Find(a => a.Name=="FirstOne")?.Value;
            set {
               var p = Arguments.Find(a => a.Name=="FirstOne");
               if(p == null){
                  p = new PropertyDTO(){Name = "FirstOne", Value = value};
                  Arguments.Add(p);
               }
               else
                  p.Value = value;
            }
         } -->




         <xsl:when test="@fromList">
            <xsl:value-of select="concat(' {', $nl1)" />
            <xsl:value-of select="concat($t4, 'get =&gt; this.', @fromList, '.Find(a =&gt; a.Name==&quot;', @name, '&quot;)?.Value;', $nl1)" />
            <xsl:value-of select="concat($t4, 'set {', $nl1)" />
            <xsl:value-of select="concat($t4, '}', $nl1)" />
            <xsl:value-of select="concat($t3, '}')" />
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
