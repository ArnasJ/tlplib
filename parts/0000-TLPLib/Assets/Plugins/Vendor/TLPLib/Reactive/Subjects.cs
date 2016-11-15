﻿using System.Collections.Generic;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

namespace com.tinylabproductions.TLPLib.Reactive {
  public interface ISubject<A> : IObservable<A>, IObserver<A> {}

  /** 
   * A subject is something that is Observable and Observer at the same
   * time.
   **/
  public class Subject<A> : Observable<A>, ISubject<A> {
    public void push(A value) => submit(value);
    public void finish() => finishObservable();
  }

  /**
   * Replay subject stores all the events that comes into it and resubmits 
   * them upon subscription.
   **/
  public class ReplaySubject<A> : Observable<A>, ISubject<A> {
    // None - stream finished, Some(value) - submited;
    readonly List<Option<A>> events = new List<Option<A>>();

    public override ISubscription subscribe(IObserver<A> observer) {
      foreach (var opt in events) {
        if (opt.isDefined) observer.push(opt.get);
        else observer.finish();
      }
      return base.subscribe(observer);
    }

    public void push(A value) {
      submit(value);
      events.Add(value.some());
    }

    public void finish() {
      finishObservable();
      events.Add(Option<A>.None);
    }

    /** Clears the event backlog. */
    public void clear() => events.Clear();
  }
}
