using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public class PathHelper
    {
        public string ConcatenatePath(string referenceItem, ref StringBuilder pathStringBuilder, ref IList<string> targetList, char separator)
        {
            if (targetList == null) throw new ArgumentNullException(nameof(targetList));
            targetList = referenceItem.Split(new char[] { separator })
                                      .Where(x => !string.IsNullOrEmpty(x))
                                      .ToList();
            if (string.IsNullOrEmpty(pathStringBuilder.ToString()))
                pathStringBuilder.Append(separator);
            foreach (string target in targetList)
            {
                if (pathStringBuilder.ToString().IndexOf(target, StringComparison.Ordinal) < 0)
                    pathStringBuilder.Append(target + separator);
            }
            return pathStringBuilder.ToString();
        }
    }
}
