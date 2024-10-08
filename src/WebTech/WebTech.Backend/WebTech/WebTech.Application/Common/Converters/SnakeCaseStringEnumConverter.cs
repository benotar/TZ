﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebTech.Application.Common.Converters;

public class SnakeCaseStringEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Enum.Parse<TEnum>(reader.GetString(), ignoreCase: true);
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        var snakeCaseValue = ToSnakeCase(value.ToString());
        
        writer.WriteStringValue(snakeCaseValue);
    }

    private static string ToSnakeCase(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var strBld = new StringBuilder();

        for (int i = 0; i < value.Length; i++)
        {
            if (i > 0 && char.IsUpper(value[i]))
            {
                strBld.Append('_');
            }

            strBld.Append(char.ToLower(value[i]));
        }

        return strBld.ToString();
    }
}