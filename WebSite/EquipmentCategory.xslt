<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>
    <!-- 链接地址 例如：default.aspx? 或者defalut.aspx?s=GHY-JY&-->
    <xsl:param name="src"/>
    <!-- 连接指向iframe的name属性，或者_blank,_self....-->
    <xsl:param name="target"/>
    <!--展开深度-->
    <xsl:param name="expendDeep"/>
    <xsl:template match="/Categories">
        <xsl:apply-templates select="category" >
            <xsl:with-param name="deep">
                <xsl:value-of select="1"/>
            </xsl:with-param>
        </xsl:apply-templates>
    </xsl:template>

    <xsl:template name="item" match="category">
        <xsl:param name="deep"/>
        <li>
            <xsl:if test="$deep&gt;$expendDeep">
                <xsl:if test="child::*">
                    <xsl:attribute name="state">closed</xsl:attribute>
                </xsl:if>
            </xsl:if>
            <xsl:attribute name="id">
                <xsl:value-of select="@id"/>
            </xsl:attribute>
            <span>
                <xsl:if test="$src=''">
                    <xsl:value-of select="@name"/>
                </xsl:if>
                <xsl:if test="$src!=''">
                    <a>
                        <xsl:attribute name="href">
                            <xsl:value-of select="$src"/>
                            <xsl:value-of select="@id"/>
                        </xsl:attribute>

                        <xsl:if test="$target!=''">
                            <xsl:attribute name="target">
                                <xsl:value-of select="$target"/>
                            </xsl:attribute>
                        </xsl:if>
                        <xsl:value-of select="@name"/>
                    </a>
                </xsl:if>
            </span>
            <xsl:if test="child::*">
                <ul>
                    <xsl:apply-templates select="category">
                        <xsl:with-param name="deep">
                            <xsl:value-of select="$deep+1"/>
                        </xsl:with-param>
                    </xsl:apply-templates>
                </ul>
            </xsl:if>
        </li>
    </xsl:template>
</xsl:stylesheet>
