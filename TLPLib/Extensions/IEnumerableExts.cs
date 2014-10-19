﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace com.tinylabproductions.TLPLib.Extensions {
  public static class IEnumerableExts {
    /* This should really be used only for debugging. It is pretty slow. */
    public static String asString(
      this IEnumerable enumerable, 
      bool newlines=true, bool fullClasses=false
    ) {
      var items = (
        from object item in enumerable
        let str = item as String // String is IEnumerable as well
        let enumItem = item as IEnumerable
        select str ?? (
          enumItem == null 
            ? item.ToString() : enumItem.asString(newlines, fullClasses)
        )
      ).ToArray();
      var itemsStr = 
        string.Join(string.Format(",{0} ", newlines ? "\n " : ""), items);
      if (items.Length != 0 && newlines) itemsStr = "\n  " + itemsStr + "\n";

      var type = enumerable.GetType();
      return string.Format(
        "{0}[{1}]",
        fullClasses ? type.FullName : type.Name,
        itemsStr
      );
    }

    public static IEnumerable<A> Yield<A>(this A any) {
      yield return any;
    }
  }
}
