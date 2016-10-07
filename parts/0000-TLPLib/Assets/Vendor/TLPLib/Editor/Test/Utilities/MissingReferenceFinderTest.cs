﻿using System;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using com.tinylabproductions.TLPLib.Test;
using com.tinylabproductions.TLPLib.Utilities.Editor;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;
// ReSharper disable ClassNeverInstantiated.Local, NotNullMemberIsNotInitialized
#pragma warning disable 169

namespace com.tinylabproductions.TLPLib.Editor.Test.Utilities {
  public class MissingReferenceFinderTest {
    class TestClass : MonoBehaviour {
      public GameObject field;
    }

    class NotNullPublicField : MonoBehaviour {
      [NotNull] public GameObject field;
    }

    class NotNullSerializedField : MonoBehaviour {
      [NotNull, SerializeField] GameObject field;
      public void setField (GameObject go) { field = go; }
    }

    [Serializable]
    public struct InnerNotNull {
      [NotNull] public GameObject field;  
    }

    class NullReferencePublicField : MonoBehaviour {
      public InnerNotNull field;
    }

    class NullReferenceSerializedField : MonoBehaviour {
      [SerializeField] InnerNotNull field;
      public void setField (InnerNotNull inn) { field = inn; }
    }

    [Test] public void WhenMissingReference() => test<TestClass>(
      a => {
        a.field = new GameObject();
        Object.DestroyImmediate(a.field);
      },
      ReferencesInPrefabs.ErrorType.MISSING_REF.some()
    );
    [Test] public void WhenReferenceNotMissing() => test<TestClass>(
      a => {
        a.field = new GameObject();
      }
    );
    [Test] public void WhenMissingReferenceInner() => test<NullReferencePublicField>(
      a => {
        a.field.field = new GameObject();
        Object.DestroyImmediate(a.field.field);
      },
      ReferencesInPrefabs.ErrorType.MISSING_REF.some()
    );
    [Test] public void WhenReferenceNotMissingInner() => test<NullReferencePublicField>(
      a => {
        a.field.field = new GameObject();
      }
    );

    [Test] public void WhenNotNullPublicField() => test<NotNullPublicField>(
      errorType: ReferencesInPrefabs.ErrorType.NULL_REF.some()
    );
    [Test] public void WhenNotNullPublicFieldSet() => test<NotNullPublicField>(
      a => {
        a.field = new GameObject();
      }
    );
    [Test] public void WhenNotNullSerializedField() => test<NotNullSerializedField>(
      errorType: ReferencesInPrefabs.ErrorType.NULL_REF.some()
    );
    [Test] public void WhenNotNullSerializedFieldSet() => test<NotNullSerializedField>(
      a => {
        a.setField(new GameObject());
      }
    );

    [Test] public void WhenNullInsideMonoBehaviorPublicField() => test<NullReferencePublicField>(
      errorType: ReferencesInPrefabs.ErrorType.NULL_REF.some()
    );
    [Test] public void WhenNullInsideMonoBehaviorPublicFieldSet() => test<NullReferencePublicField>(
      a => {
        a.field = new InnerNotNull {field = new GameObject()};
      }
    );
    [Test] public void WhenNullInsideMonoBehaviorSerializedField() => test<NullReferenceSerializedField>(
      errorType: ReferencesInPrefabs.ErrorType.NULL_REF.some()
    );
    [Test] public void WhenNullInsideMonoBehaviorSerializedFieldSet() => test<NullReferenceSerializedField>(
      a => {
        a.setField(new InnerNotNull {field = new GameObject()});
      }
    );

    static void test<A>(
      Act<A> setupA = null,
      Option<ReferencesInPrefabs.ErrorType> errorType = new Option<ReferencesInPrefabs.ErrorType>()
    ) where A : Component {
      var go = new GameObject();
      var a = go.AddComponent<A>();
      setupA?.Invoke(a);
      var errors = ReferencesInPrefabs.findMissingReferences("", new [] { go }, false);
      errorType.voidFold(
        () => errors.shouldBeEmpty(),
        type => errors.shouldMatch(t => t.Exists(x => x.errorType == type))
      );
    }
  }
}