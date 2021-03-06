﻿using System;
using com.tinylabproductions.TLPLib.Concurrent;

namespace com.tinylabproductions.TLPLib.Functional {
  /// <summary>
  /// C# does not have higher kinded types, so we need to specify every combination of monads here
  /// as an extension method. However this reduces visual noise in the usage sites.
  /// </summary>
  public static class MonadTransformers {
    #region Option

    #region of Either

    public static Option<Either<A, BB>> mapT<A, B, BB>(
      this Option<Either<A, B>> m, Fn<B, BB> mapper
    ) => m.map(_ => _.mapRight(mapper));

    public static Option<Either<A, BB>> flatMapT<A, B, BB>(
      this Option<Either<A, B>> m, Fn<B, Either<A, BB>> mapper
    ) => m.map(_ => _.flatMapRight(mapper));

    #endregion

    #endregion

    #region Either

    #region ofOption

    public static Either<A, Option<BB>> mapT<A, B, BB>(
      this Either<A, Option<B>> m, Fn<B, BB> mapper
    ) => m.mapRight(_ => _.map(mapper));

    public static Either<A, Option<BB>> flatMapT<A, B, BB>(
      this Either<A, Option<B>> m, Fn<B, Option<BB>> mapper
    ) => m.mapRight(_ => _.flatMap(mapper));

    #endregion

    #endregion

    #region Future

    #region of Option

    public static Future<Option<B>> mapT<A, B>(
      this Future<Option<A>> m, Fn<A, B> mapper
    ) => m.map(_ => _.map(mapper));

    public static Future<Option<B>> flatMapT<A, B>(
      this Future<Option<A>> m, Fn<A, Option<B>> mapper
    ) => m.map(_ => _.flatMap(mapper));

    public static Future<Option<B>> flatMapT<A, B>(
      this Future<Option<A>> m, Fn<A, Future<Option<B>>> mapper
    ) => m.flatMap(_ => _.fold(
      () => Future.successful(F.none<B>()),
      mapper
    ));

    #endregion

    #region of Either

    public static Future<Either<A, BB>> mapT<A, B, BB>(
      this Future<Either<A, B>> m, Fn<B, BB> mapper
    ) => m.map(_ => _.mapRight(mapper));

    public static Future<Either<A, BB>> flatMapT<A, B, BB>(
      this Future<Either<A, B>> m, Fn<B, Either<A, BB>> mapper
    ) => m.map(_ => _.flatMapRight(mapper));

    public static Future<Either<A, BB>> flatMapT<A, B, BB>(
      this Future<Either<A, B>> m, Fn<B, Future<Either<A, BB>>> mapper
    ) => m.flatMap(_ => _.fold(
      a => Future.successful(Either<A, BB>.Left(a)),
      mapper
    ));

    public static Future<Either<A, BB>> flatMapT<B, BB, A>(
      this Future<Either<A, B>> m, Fn<B, Future<BB>> mapper
    ) => m.flatMap(_ => _.fold(
      err => Future.successful(Either<A, BB>.Left(err)),
      from => mapper(from).map(Either<A, BB>.Right)
    ));


    #endregion

    #region of Try

    public static Future<Try<To>> flatMapT<From, To>(
      this Future<Try<From>> m, Fn<From, Future<To>> mapper
    ) => m.flatMap(_ => _.fold(
      from => mapper(from).map(F.scs),
      err => Future.successful(F.err<To>(err))
    ));

    #endregion

    #endregion

    #region LazyVal

    #region of Try

    public static LazyVal<Try<B>> mapT<A, B>(
      this LazyVal<Try<A>> m, Fn<A, B> mapper
    ) => m.map(_ => _.map(mapper));

    #endregion

    #endregion
  }
}