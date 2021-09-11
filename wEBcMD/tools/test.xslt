<?xml version="1.0" encoding="uwc-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:wc="https://github.com/kokolorix/this-file"
   >
   <xsl:output name="text-def" method="text" />
   <xsl:template match="/">
      <xsl:message select="base-uri(.)"/>
      <xsl:variable name="d" select="wc:path-delimiter(.)"/>
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Doc', 'generate-diagarams.cmd'), $d)"/>
      <xsl:variable name="lines" select="unparsed-text-lines($filePath)"/>
      <xsl:message select="$filePath"/>
      <xsl:message select="concat('lines: ', count($lines))"/>
      <xsl:result-document href="{$filePath}" format="text-def">
         <xsl:for-each select="$lines">
            <xsl:value-of select="concat(., '&#xA;')"/>
         </xsl:for-each>
         <xsl:variable name="commands" select="Types/CommandWrapper"/>
         <!-- if there any commands  -->
         <xsl:for-each select="$commands">
            <xsl:variable name="regex" select="concat('.+', @name, 'Base\.ts.+')"/>
            <xsl:message select="concat('search for ', $regex)"/>
            <xsl:if test="not($lines[matches(., $regex)])">
               <xsl:message select="'must work :-('"/>
               <xsl:text/>
call tplant --input ..\ClientApp\src\api\<xsl:value-of select="@name"/>Base.ts ..\ClientApp\src\impl\<xsl:value-of select="@name"/>.ts ..\ClientApp\src\impl\CommandWrapper.ts --output Types\ts\<xsl:value-of select="@name"/>.puml -A<xsl:text/>
            </xsl:if>
         </xsl:for-each>
      </xsl:result-document>
   </xsl:template>

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
