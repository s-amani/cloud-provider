using System;

namespace CloudProvider.SDK.Common
{
    public static class ObjectExtensions
    {
        public static void ThrowExceptionIfNull(this object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

    }
}
