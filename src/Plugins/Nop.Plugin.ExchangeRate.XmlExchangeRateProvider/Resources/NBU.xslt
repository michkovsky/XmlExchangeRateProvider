<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
>
  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/exchange">
    <xsl:element name="root">
      <xsl:for-each select="currency">
        <xsl:element name="ExchangeRate">
          <xsl:element name="CurrencyCode">
            <xsl:value-of select="cc"/>
          </xsl:element>
          <xsl:element name="Rate">
            <xsl:value-of select="rate"/>
          </xsl:element>
          <xsl:element name="UpdatedOn">
            <xsl:call-template name="parse-date">
              <xsl:with-param name="str-date" select="exchangedate" />
            </xsl:call-template>
          </xsl:element>
        </xsl:element>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
  <xsl:template name="parse-date">
    <xsl:param name="str-date" />
    <xsl:comment>
      input date-string format "dd.MM.YYYY" is xsd:date incompatible
    </xsl:comment>
    <xsl:value-of select="concat(
                  substring($str-date,7,4),'-',
                  substring($str-date,4,2),'-',
                  substring($str-date,1,2)
                  )"/>
  </xsl:template>
</xsl:stylesheet>