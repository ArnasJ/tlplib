﻿using System;
using com.tinylabproductions.TLPLib.Extensions;

namespace com.tinylabproductions.TLPLib.Functional {
  public 
#if UNITY_IOS
  class
#else
  struct
#endif
  Either<A, B> {
    public static Either<A, B> Left(A value) { return new Either<A, B>(value); }
    public static Either<A, B> Right(B value) { return new Either<A, B>(value); }

    private readonly A _leftValue;
    private readonly B _rightValue;
    private readonly bool _isLeft;

    public Either(A value) {
      _leftValue = value;
      _rightValue = default(B);
      _isLeft = true;
    }
    public Either(B value) {
      _leftValue = default(A);
      _rightValue = value;
      _isLeft = false;
    }

    public bool isLeft { get { return _isLeft; } }
    public bool isRight { get { return ! _isLeft; } }
    public Option<A> leftValue { get { return isLeft.opt(_leftValue); } }
    public Option<B> rightValue { get { return (! isLeft).opt(_rightValue); } }

    public override string ToString() {
      return isLeft ? "Left(" + _leftValue + ")" : "Right(" + _rightValue + ")";
    }

    public Either<C, B> flatMapLeft<C>(Fn<A, Either<C, B>> mapper) 
      { return fold(mapper, F.right<C, B>); }

    public Either<A, C> flatMapRight<C>(Fn<B, Either<A, C>> mapper) 
      { return fold(F.left<A, C>, mapper); }

    public Either<C, B> mapLeft<C>(Fn<A, C> mapper) 
      { return fold(v => F.left<C, B>(mapper(v)), F.right<C, B>); }

    public Either<A, C> mapRight<C>(Fn<B, C> mapper) 
      { return fold(F.left<A, C>, v => F.right<A, C>(mapper(v))); }

    public C fold<C>(Fn<A, C> onLeft, Fn<B, C> onRight) 
      { return isLeft ? onLeft(_leftValue) : onRight(_rightValue); }

    public void voidFold(Act<A> onLeft, Act<B> onRight) 
      { if (isLeft) onLeft(_leftValue); else onRight(_rightValue); }

    public Option<B> toOpt() { return rightValue; }
  }

  public static class EitherBuilderExts {
    public static LeftEitherBuilder<A> left<A>(this A value) {
      return new LeftEitherBuilder<A>(value);
    }
    public static RightEitherBuilder<B> right<B>(this B value) {
      return new RightEitherBuilder<B>(value);
    }
  }

  public 
#if UNITY_IOS
    class
#else
    struct 
#endif
    LeftEitherBuilder<A> {
    public readonly A leftValue;

    public LeftEitherBuilder(A leftValue) {
      this.leftValue = leftValue;
    }

    public Either<A, B> r<B>() { return new Either<A, B>(leftValue); }
  }

  public 
#if UNITY_IOS
    class
#else
    struct 
#endif
    RightEitherBuilder<B> {
    public readonly B rightValue;

    public RightEitherBuilder(B rightValue) {
      this.rightValue = rightValue;
    }

    public Either<A, B> l<A>() { return new Either<A, B>(rightValue); }
  }
}
