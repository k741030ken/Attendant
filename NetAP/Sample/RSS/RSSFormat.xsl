<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" indent="yes"/>
    <xsl:param name="TITLE"/>

    <xsl:template match="rss">
        <!-- Do not show channel image -->
        <xsl:for-each select="channel/item">
            <xsl:if test="not(position() > 5)">
                <br>
                    <strong>
                        <a href="{link}" target="_blank">
                            <xsl:value-of select="title"/>
                        </a>
                    </strong>
                    <br></br>
                    <!-- only display markup for description if it's present -->
                    <xsl:value-of select="description" disable-output-escaping="yes"/>
                </br>
                <br></br>
            </xsl:if>
        </xsl:for-each>
    </xsl:template>

    <xsl:template match="description">
        <br>
            <xsl:value-of select="."/>
        </br>
    </xsl:template>
</xsl:stylesheet>

