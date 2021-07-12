<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" xmlns:xdt="http://www.w3.org/2005/02/xpath-datatypes">
	<xsl:output method="text"/>
	<xsl:output name="text-def" method="text"/>
	<xsl:output name="xml-def" method="xml" indent="yes"/>
	<xsl:variable name="tab1" select="'&#x9;'"/>
	<xsl:variable name="tab2" select="concat($tab1, '&#x9;')"/>
	<xsl:variable name="tab3" select="concat($tab2, '&#x9;')"/>
	<xsl:variable name="tab4" select="concat($tab3, '&#x9;')"/>
	<xsl:variable name="tab5" select="concat($tab4, '&#x9;')"/>
	<xsl:variable name="newLine1" select="'&#xA;'"/>
	<xsl:variable name="newLine2" select="concat($newLine1, '&#xA;')"/>
	<!--=======================================================================-->
	<!--header with guards ans includes -->
	<!--=======================================================================-->
	<xsl:template match="/">
		<xsl:variable name="file" select="replace(tokenize(base-uri(.), '/')[last()],'.xml','')"/>
		<xsl:variable name="fn" select="replace(base-uri(.),'.xml' ,'.cs')"/>
		<xsl:result-document href="{$fn}" format="text-def">
			<xsl:value-of select="concat('using System;', $newLine1)"/>
			<xsl:value-of select="concat('using System.Collections.Generic;', $newLine2)"/>
			<xsl:value-of select="concat('namespace wEBcMD', $newLine1)"/>
			<xsl:value-of select="concat('{', $newLine1)"/>
			<xsl:apply-templates select="Types/ObjectType" mode="types"/>
			<xsl:value-of select="concat('}', $newLine1)"/>
		</xsl:result-document>
	</xsl:template>
	<!--=======================================================================-->
	<!--process an Object node -->
	<!--=======================================================================-->
	<xsl:template match="ObjectType" mode="types">
		<xsl:variable name="category" select="@category"/>
		<xsl:variable name="name" select="@name"/>
		<xsl:variable name="id" select="@id"/>

		<xsl:value-of select="concat($tab1, 'public class ', $name)"/>
		<xsl:apply-templates select="Base" mode="types"/>
		
		<xsl:value-of select="concat($newLine1, $tab1, '{', $newLine1)"/>
		<xsl:apply-templates select="PropertyType" mode="types"/>

		<xsl:value-of select="concat($tab1, '};', $newLine2)"/>
	</xsl:template>
	<!--=======================================================================-->
	<!--process the Base node -->
	<!--=======================================================================-->
	<xsl:template match="Base" mode="types">
		<xsl:variable name="category" select="@category"/>
		<xsl:variable name="name" select="@name"/>
		<xsl:variable name="base" select="concat($category, '::', $name)"/>
		<xsl:if test="$name!=''">
			<xsl:value-of select="concat(' : ', $name)"/>
		</xsl:if>
	</xsl:template>
	<!--=======================================================================-->
	<!--process an Property node for PropertyT -->
	<!--=======================================================================-->
	<xsl:template match="PropertyType" mode="types">
		<xsl:variable name="name" select="@name"/>

		<xsl:value-of select="concat($tab2, 'public')"/>
		<xsl:call-template name="DataType"/>
		<xsl:value-of select="concat(' ', $name)"/>
		<xsl:value-of select="concat(' { get; set; }', $newLine1)"/>
	</xsl:template>
	<!--=======================================================================-->
	<!--process DataType -->
	<!--=======================================================================-->
	<xsl:template name="DataType">
		<xsl:variable name="type" select="replace(@type, '\[\]', '')"/>
		<xsl:text> </xsl:text>
		<xsl:if test="ends-with(@type, '[]')">
			<xsl:text>List&lt;</xsl:text>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="$type='UuId'">
				<xsl:value-of select="concat(' ', 'Guid')"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$type"/>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:if test="ends-with(@type, '[]')">
			<xsl:text>&gt;</xsl:text>
		</xsl:if>
	</xsl:template>
	<!--=======================================================================-->
	<!-- -->
	<!--=======================================================================-->
	<xsl:template name="camelCaseWord">
		<xsl:param name="text"/>
		<xsl:value-of select="translate(substring($text,1,1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')"/>
		<xsl:value-of select="substring($text,2,string-length($text)-1)"/>
	</xsl:template>
</xsl:stylesheet>