﻿using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace NWebDav.Server.Helpers;

public static class XmlHelper
{
    public static string GetXmlValue<TEnum>(TEnum value) where TEnum : struct
    {
        // Obtain the member information
        var memberInfo = typeof(TEnum).GetMember(value.ToString() ?? string.Empty).FirstOrDefault();
        if (memberInfo == null) throw new ArgumentException("Value not found", nameof(value));

        var xmlEnumAttribute = memberInfo.GetCustomAttribute<XmlEnumAttribute>();
        return xmlEnumAttribute?.Name ?? value.ToString() ?? throw new InvalidOperationException("Unable to determine value");
    }
}
