using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortenerSharp.ServiceDefaults.Extensions
{
    public static class StringExtensions
    {
        extension(string value)
        {
            public bool IsNullOrEmpty()
            {
                return string.IsNullOrEmpty(value);
            }

            public bool IsNullOrWhiteSpace()
            {
                return string.IsNullOrWhiteSpace(value);
            }

            public bool IsNullOrEmptyOrWhiteSpace()
            {
                return string.IsNullOrWhiteSpace(value);
            }

            public bool IsUrl()
            {
                if (string.IsNullOrWhiteSpace(value))
                    return false;

                return Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult)
                       && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
        }
    }
}
