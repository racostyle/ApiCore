﻿using Common;
using Common.DisplayClasses;
using System.Text.Json;

namespace ResourceReader.SUtils
{
    public static class Extensions
    {
        public static string[] GenerateArrayFromJson(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return Array.Empty<string>();

            try
            {
                var content = JsonSerializer.Deserialize<string[]>(json);

                for (int i = 0; i < content.Length; i++)
                {
                    var index = content[i].IndexOf("||");
                    content[i] = content[i].Substring(index + 2);
                }

                return content;
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        public static string ToStringAndFormat(this IMachineData result)
        {
            return $"{result.ToString()}{Environment.NewLine}{Environment.NewLine}";
        }
    }
}
