﻿using UnityEngine;

namespace com.tinylabproductions.TLPLib.Extensions {
  public static class ComponentExts {
    public static A clone<A>(
      this A self, Vector3? position=null, Quaternion? rotation=null, Transform parent=null, 
      int? siblingIndex=null, bool? setActive=null
    ) where A : Component {

      // Setting parent through instantiate is faster than first creating object and then setting it's parent.
      // https://youtu.be/n-oZa4Fb12U?t=1386
      var cloned = parent != null ? Object.Instantiate(self, parent, false) : Object.Instantiate(self);
      if (position != null) cloned.transform.position = (Vector3) position;
      if (rotation != null) cloned.transform.rotation = (Quaternion) rotation;
      if (siblingIndex != null) cloned.transform.SetSiblingIndex((int) siblingIndex);
      if (setActive != null) cloned.gameObject.SetActive((bool) setActive);
      return cloned;
    }
  }
}
