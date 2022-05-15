using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yella.Core.Helper
{
    public static class FriendlyUrlHelper
    {
        public static string GetFriendlyTitle(string title, bool remapToAscii = false, int maxlength = 80)
        {
            if (title is null)
            {
                return string.Empty;
            }

            var length = title.Length;
            var prevdash = false;
            var stringBuilder = new StringBuilder(length);
            char c;

            for (var i = 0; i < length; ++i)
            {
                c = title[i];
                if (c is (>= 'a' and <= 'z') or (>= '0' and <= '9'))
                {
                    stringBuilder.Append(c);
                    prevdash = false;
                }
                else if (c is >= 'A' and <= 'Z')
                {
                    // tricky way to convert to lower-case
                    stringBuilder.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c is ' ' or ',' or '.' or '/' or '\\' or '-' or '_' or '=')
                {
                    if (!prevdash && (stringBuilder.Length > 0))
                    {
                        stringBuilder.Append('-');
                        prevdash = true;
                    }
                }
                else if (c >= 128)
                {
                    var previousLength = stringBuilder.Length;

                    if (remapToAscii)
                    {
                        stringBuilder.Append(RemapInternationalCharToAscii(c));
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }

                    if (previousLength != stringBuilder.Length)
                    {
                        prevdash = false;
                    }
                }

                if (stringBuilder.Length >= maxlength)
                {
                    break;
                }
            }

            if (prevdash || stringBuilder.Length > maxlength)
            {
                return stringBuilder.ToString()[..(stringBuilder.Length - 1)];
            }
            else
            {
                return stringBuilder.ToString();
            }
        }

        private static string RemapInternationalCharToAscii(char character)
        {
            var s = new string(character, 1).ToLowerInvariant();
            if ("àåáâäãåąā".Contains(s, StringComparison.Ordinal))
            {
                return "a";
            }
            else if ("èéêěëę".Contains(s, StringComparison.Ordinal))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s, StringComparison.Ordinal))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s, StringComparison.Ordinal))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s, StringComparison.Ordinal))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s, StringComparison.Ordinal))
            {
                return "c";
            }
            else if ("żźž".Contains(s, StringComparison.Ordinal))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s, StringComparison.Ordinal))
            {
                return "s";
            }
            else if ("ñń".Contains(s, StringComparison.Ordinal))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s, StringComparison.Ordinal))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s, StringComparison.Ordinal))
            {
                return "g";
            }
            else if ("ŕř".Contains(s, StringComparison.Ordinal))
            {
                return "r";
            }
            else if ("ĺľł".Contains(s, StringComparison.Ordinal))
            {
                return "l";
            }
            else if ("úů".Contains(s, StringComparison.Ordinal))
            {
                return "u";
            }
            else if ("đď".Contains(s, StringComparison.Ordinal))
            {
                return "d";
            }
            else if (character == 'ť')
            {
                return "t";
            }
            else if (character == 'ž')
            {
                return "z";
            }
            else if (character == 'ß')
            {
                return "ss";
            }
            else if (character == 'Þ')
            {
                return "th";
            }
            else if (character == 'ĥ')
            {
                return "h";
            }
            else if (character == 'ĵ')
            {
                return "j";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
