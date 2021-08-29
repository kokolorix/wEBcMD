<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
   >
   <xsl:output name="text-def" method="text" />
   <xsl:template match="/">
      <xsl:message select="base-uri(.)"/>
      <xsl:variable name="d" select="'/'"/>
      <xsl:variable name="pt" select="tokenize(base-uri(.), $d)"/>
      <xsl:variable name="fn" select="concat('Command', 'Controller.cs')" />
      <xsl:variable name="filePath" select="string-join((subsequence($pt, 1,count($pt) - 2), 'Controllers', $fn), $d)"/>
      <!-- <xsl:message select="$filePath"/> -->
      <xsl:variable name="lines" select="unparsed-text-lines($filePath)"/>
      <xsl:message select="concat('lines: ', count($lines))"/>
      <xsl:if test="not($lines[matches(.,'FindAdresses\.IsForMe')])">
         <xsl:message select="'must work :-('"/>
         <xsl:result-document href="{$filePath}" format="text-def">
            <xsl:for-each select="$lines">
            <xsl:message select="concat('pos: ', position())"/>
               <xsl:choose>
                  <xsl:when test="contains(.,'NEW DISPATCHERS INSERTED HERE')">
                     <xsl:value-of select="'         '"/>
                     <xsl:if test="$lines[matches(.,'return \w+\.ExecuteCommand')]">
                        <xsl:value-of select="'else '"/>
                     </xsl:if>
                     <xsl:value-of select="concat('if(FindAdresses.IsForMe(cmd))', '&#xA;')"/>
                     <xsl:value-of select="concat('            return FindAdresses.ExecuteCommand(cmd);', '&#xA;')"/>
                     <xsl:value-of select="concat(., '&#xA;')"/>
                  </xsl:when>
                  <xsl:otherwise>
                     <xsl:value-of select="concat(., '&#xA;')"/>
                  </xsl:otherwise>
               </xsl:choose>
            </xsl:for-each>
         </xsl:result-document>
      </xsl:if>
      <!-- <xsl:variable name="lines"> -->
      <!-- <xsl:for-each select="unparsed-text-lines($filePath)" expand-text="yes"> -->
      <!-- <xsl:sequence select="."/> -->
      <!-- <xsl:analyze-string select="." regex="return FindMyAdresses\.ExecuteCommand\(cmd\);">
           <xsl:matching-substring>
           <xsl:message select="concat('found: ', .)"/>
           <xsl:value-of select="true()"/>
           </xsl:matching-substring>
           <xsl:non-matching-substring>
           <xsl:if test="position() = count($lines)">
           <xsl:value-of select="false()"/>
           </xsl:if>
           </xsl:non-matching-substring>
           </xsl:analyze-string> -->
      <!-- </xsl:for-each> -->
      <!-- </xsl:variable> -->
      <!-- <xsl:message select="$lines"/> -->
      <!-- <xsl:variable name="lines" select="unparsed-text-lines($filePath)"/> -->
      <!-- <xsl:if test="not($found)">
           <xsl:message select="'need dispatcher'"/>
           </xsl:if> -->
     </xsl:template>
</xsl:stylesheet>
