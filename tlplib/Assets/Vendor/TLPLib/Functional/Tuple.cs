﻿/**
 * Scala code to generate this file.
 * 
 
import java.io._

val tupleCs = new PrintWriter("Tuple.cs")
val fCs = new PrintWriter("F.cs")
val tName = "Tpl";
 
(2 to 22).map { i =>
  val r = 1 to i
  val genPNames = r.map(n => s"P$n")
  val sTypes = genPNames.mkString(", ")
  val args = r.map(n => s"P$n p$n").mkString(", ")
  val argsNoTypes = r.map(n => s"p$n").mkString(", ")
  val propsNoTypes = r.map(n => s"_$n").mkString(", ")
  val setters = r.map(n => s"_$n = p$n").mkString("; ")
  val propsImpl = r.map(n => s"public P$n _$n { get; private set; }").mkString(" ")
  val toStringFmt = r.map(n => s"{${n - 1}}").mkString(",")
  val toStringArgs = r.map(n => s"_$n").mkString(", ")
  val iNames = Map(
    "\n#if UNITY_IOS\nclass\n#else\nstruct\n#endif\n" -> ("", "\n#if ! UNITY_IOS\n : this()\n#endif\n")
  ).map { case (kind, (prefix, baseConstructor)) =>
    val iName = s"${prefix}${tName}"
    val fType = s"$iName<$sTypes>"
    tupleCs.println(
s"""  [System.Serializable]
  public $kind $fType : 
  IComparable<$fType>, IEquatable<$fType> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    $propsImpl
  
    public $iName($args)$baseConstructor { $setters; }
  
    // Unapply.
    public void ua(Act<$sTypes> f) { f($propsNoTypes); }
 
    // Unapply with function.
    public R ua<R>(Fn<$sTypes, R> f) { return f($propsNoTypes); } 
 
    public override string ToString() {
      return string.Format("($toStringFmt)", $toStringArgs);
    }
 
	  public override bool Equals(object o) {
      return o is $fType && this.Equals(($fType) o);
    }

    public bool Equals($fType t) {
      return (
${r.map { i => 
  s"Smooth.Collections.EqComparer<P$i>.Default.Equals(_$i, t._$i)"
}.mkString(" &&\n")}
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
${r.map { i => 
  s"hash = 29 * hash + Smooth.Collections.EqComparer<P$i>.Default.GetHashCode(_$i);"
}.mkString("\n")}
        return hash;
      }
    }

    public int CompareTo($fType other) {
      int c;
${r.map { i => 
  s"c = Smooth.Collections.Comparer<P$i>.Default.Compare(_$i, other._$i);"
}.mkString(" if (c != 0) { return c; }\n")}
      return c;
    }

    public static bool operator == ($fType lhs, $fType rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != ($fType lhs, $fType rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > ($fType lhs, $fType rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < ($fType lhs, $fType rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= ($fType lhs, $fType rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= ($fType lhs, $fType rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }""")
    s"${prefix.toLowerCase}t" -> iName
  }
  (i, iNames, sTypes, args, argsNoTypes)
}.foreach { case (i, iNames, sTypes, args, argsNoTypes) =>
  iNames.foreach { case (prefix, iName) =>
    fCs.println(
s"public static $tName<$sTypes> $prefix<$sTypes>($args) { return new $iName<$sTypes>($argsNoTypes); }"
    )
  }
}
  
tupleCs.close()
fCs.close()
 
 * 
 **/

namespace System {
    [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2> : 
  IComparable<Tpl<P1, P2>>, IEquatable<Tpl<P1, P2>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; }
  
    public Tpl(P1 p1, P2 p2)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; }
  
    // Unapply.
    public void ua(Act<P1, P2> f) { f(_1, _2); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, R> f) { return f(_1, _2); } 
 
    public override string ToString() {
      return string.Format("({0},{1})", _1, _2);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2> && this.Equals((Tpl<P1, P2>) o);
    }

    public bool Equals(Tpl<P1, P2> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2);
      return c;
    }

    public static bool operator == (Tpl<P1, P2> lhs, Tpl<P1, P2> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2> lhs, Tpl<P1, P2> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2> lhs, Tpl<P1, P2> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2> lhs, Tpl<P1, P2> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2> lhs, Tpl<P1, P2> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2> lhs, Tpl<P1, P2> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3> : 
  IComparable<Tpl<P1, P2, P3>>, IEquatable<Tpl<P1, P2, P3>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3> f) { f(_1, _2, _3); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, R> f) { return f(_1, _2, _3); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2})", _1, _2, _3);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3> && this.Equals((Tpl<P1, P2, P3>) o);
    }

    public bool Equals(Tpl<P1, P2, P3> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3> lhs, Tpl<P1, P2, P3> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3> lhs, Tpl<P1, P2, P3> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3> lhs, Tpl<P1, P2, P3> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3> lhs, Tpl<P1, P2, P3> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3> lhs, Tpl<P1, P2, P3> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3> lhs, Tpl<P1, P2, P3> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4> : 
  IComparable<Tpl<P1, P2, P3, P4>>, IEquatable<Tpl<P1, P2, P3, P4>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4> f) { f(_1, _2, _3, _4); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, R> f) { return f(_1, _2, _3, _4); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3})", _1, _2, _3, _4);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4> && this.Equals((Tpl<P1, P2, P3, P4>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4> lhs, Tpl<P1, P2, P3, P4> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4> lhs, Tpl<P1, P2, P3, P4> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4> lhs, Tpl<P1, P2, P3, P4> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4> lhs, Tpl<P1, P2, P3, P4> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4> lhs, Tpl<P1, P2, P3, P4> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4> lhs, Tpl<P1, P2, P3, P4> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5> : 
  IComparable<Tpl<P1, P2, P3, P4, P5>>, IEquatable<Tpl<P1, P2, P3, P4, P5>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5> f) { f(_1, _2, _3, _4, _5); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, R> f) { return f(_1, _2, _3, _4, _5); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4})", _1, _2, _3, _4, _5);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5> && this.Equals((Tpl<P1, P2, P3, P4, P5>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5> lhs, Tpl<P1, P2, P3, P4, P5> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5> lhs, Tpl<P1, P2, P3, P4, P5> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5> lhs, Tpl<P1, P2, P3, P4, P5> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5> lhs, Tpl<P1, P2, P3, P4, P5> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5> lhs, Tpl<P1, P2, P3, P4, P5> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5> lhs, Tpl<P1, P2, P3, P4, P5> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6> f) { f(_1, _2, _3, _4, _5, _6); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, R> f) { return f(_1, _2, _3, _4, _5, _6); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5})", _1, _2, _3, _4, _5, _6);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6> lhs, Tpl<P1, P2, P3, P4, P5, P6> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6> lhs, Tpl<P1, P2, P3, P4, P5, P6> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6> lhs, Tpl<P1, P2, P3, P4, P5, P6> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6> lhs, Tpl<P1, P2, P3, P4, P5, P6> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6> lhs, Tpl<P1, P2, P3, P4, P5, P6> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6> lhs, Tpl<P1, P2, P3, P4, P5, P6> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7> f) { f(_1, _2, _3, _4, _5, _6, _7); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, R> f) { return f(_1, _2, _3, _4, _5, _6, _7); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6})", _1, _2, _3, _4, _5, _6, _7);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8> f) { f(_1, _2, _3, _4, _5, _6, _7, _8); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7})", _1, _2, _3, _4, _5, _6, _7, _8);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8})", _1, _2, _3, _4, _5, _6, _7, _8, _9);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; } public P17 _17 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16, P17 p17)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; _17 = p17; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16) &&
Smooth.Collections.EqComparer<P17>.Default.Equals(_17, t._17)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
hash = 29 * hash + Smooth.Collections.EqComparer<P17>.Default.GetHashCode(_17);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P17>.Default.Compare(_17, other._17);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; } public P17 _17 { get; private set; } public P18 _18 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16, P17 p17, P18 p18)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; _17 = p17; _18 = p18; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16) &&
Smooth.Collections.EqComparer<P17>.Default.Equals(_17, t._17) &&
Smooth.Collections.EqComparer<P18>.Default.Equals(_18, t._18)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
hash = 29 * hash + Smooth.Collections.EqComparer<P17>.Default.GetHashCode(_17);
hash = 29 * hash + Smooth.Collections.EqComparer<P18>.Default.GetHashCode(_18);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P17>.Default.Compare(_17, other._17); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P18>.Default.Compare(_18, other._18);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; } public P17 _17 { get; private set; } public P18 _18 { get; private set; } public P19 _19 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16, P17 p17, P18 p18, P19 p19)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; _17 = p17; _18 = p18; _19 = p19; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16) &&
Smooth.Collections.EqComparer<P17>.Default.Equals(_17, t._17) &&
Smooth.Collections.EqComparer<P18>.Default.Equals(_18, t._18) &&
Smooth.Collections.EqComparer<P19>.Default.Equals(_19, t._19)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
hash = 29 * hash + Smooth.Collections.EqComparer<P17>.Default.GetHashCode(_17);
hash = 29 * hash + Smooth.Collections.EqComparer<P18>.Default.GetHashCode(_18);
hash = 29 * hash + Smooth.Collections.EqComparer<P19>.Default.GetHashCode(_19);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P17>.Default.Compare(_17, other._17); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P18>.Default.Compare(_18, other._18); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P19>.Default.Compare(_19, other._19);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; } public P17 _17 { get; private set; } public P18 _18 { get; private set; } public P19 _19 { get; private set; } public P20 _20 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16, P17 p17, P18 p18, P19 p19, P20 p20)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; _17 = p17; _18 = p18; _19 = p19; _20 = p20; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16) &&
Smooth.Collections.EqComparer<P17>.Default.Equals(_17, t._17) &&
Smooth.Collections.EqComparer<P18>.Default.Equals(_18, t._18) &&
Smooth.Collections.EqComparer<P19>.Default.Equals(_19, t._19) &&
Smooth.Collections.EqComparer<P20>.Default.Equals(_20, t._20)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
hash = 29 * hash + Smooth.Collections.EqComparer<P17>.Default.GetHashCode(_17);
hash = 29 * hash + Smooth.Collections.EqComparer<P18>.Default.GetHashCode(_18);
hash = 29 * hash + Smooth.Collections.EqComparer<P19>.Default.GetHashCode(_19);
hash = 29 * hash + Smooth.Collections.EqComparer<P20>.Default.GetHashCode(_20);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P17>.Default.Compare(_17, other._17); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P18>.Default.Compare(_18, other._18); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P19>.Default.Compare(_19, other._19); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P20>.Default.Compare(_20, other._20);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; } public P17 _17 { get; private set; } public P18 _18 { get; private set; } public P19 _19 { get; private set; } public P20 _20 { get; private set; } public P21 _21 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16, P17 p17, P18 p18, P19 p19, P20 p20, P21 p21)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; _17 = p17; _18 = p18; _19 = p19; _20 = p20; _21 = p21; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16) &&
Smooth.Collections.EqComparer<P17>.Default.Equals(_17, t._17) &&
Smooth.Collections.EqComparer<P18>.Default.Equals(_18, t._18) &&
Smooth.Collections.EqComparer<P19>.Default.Equals(_19, t._19) &&
Smooth.Collections.EqComparer<P20>.Default.Equals(_20, t._20) &&
Smooth.Collections.EqComparer<P21>.Default.Equals(_21, t._21)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
hash = 29 * hash + Smooth.Collections.EqComparer<P17>.Default.GetHashCode(_17);
hash = 29 * hash + Smooth.Collections.EqComparer<P18>.Default.GetHashCode(_18);
hash = 29 * hash + Smooth.Collections.EqComparer<P19>.Default.GetHashCode(_19);
hash = 29 * hash + Smooth.Collections.EqComparer<P20>.Default.GetHashCode(_20);
hash = 29 * hash + Smooth.Collections.EqComparer<P21>.Default.GetHashCode(_21);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P17>.Default.Compare(_17, other._17); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P18>.Default.Compare(_18, other._18); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P19>.Default.Compare(_19, other._19); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P20>.Default.Compare(_20, other._20); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P21>.Default.Compare(_21, other._21);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }
  [System.Serializable]
  public 
#if UNITY_IOS
class
#else
struct
#endif
 Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> : 
  IComparable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22>>, IEquatable<Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22>> {
    // Unity compiler bug. If you make these readonly values there's a bug 
    // where accessing _* elements yields wrong results:
    // https://gist.github.com/arturaz/1645200a73ac55eaf72e
    public P1 _1 { get; private set; } public P2 _2 { get; private set; } public P3 _3 { get; private set; } public P4 _4 { get; private set; } public P5 _5 { get; private set; } public P6 _6 { get; private set; } public P7 _7 { get; private set; } public P8 _8 { get; private set; } public P9 _9 { get; private set; } public P10 _10 { get; private set; } public P11 _11 { get; private set; } public P12 _12 { get; private set; } public P13 _13 { get; private set; } public P14 _14 { get; private set; } public P15 _15 { get; private set; } public P16 _16 { get; private set; } public P17 _17 { get; private set; } public P18 _18 { get; private set; } public P19 _19 { get; private set; } public P20 _20 { get; private set; } public P21 _21 { get; private set; } public P22 _22 { get; private set; }
  
    public Tpl(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, P12 p12, P13 p13, P14 p14, P15 p15, P16 p16, P17 p17, P18 p18, P19 p19, P20 p20, P21 p21, P22 p22)
#if ! UNITY_IOS
 : this()
#endif
 { _1 = p1; _2 = p2; _3 = p3; _4 = p4; _5 = p5; _6 = p6; _7 = p7; _8 = p8; _9 = p9; _10 = p10; _11 = p11; _12 = p12; _13 = p13; _14 = p14; _15 = p15; _16 = p16; _17 = p17; _18 = p18; _19 = p19; _20 = p20; _21 = p21; _22 = p22; }
  
    // Unapply.
    public void ua(Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> f) { f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22); }
 
    // Unapply with function.
    public R ua<R>(Fn<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22, R> f) { return f(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22); } 
 
    public override string ToString() {
      return string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21})", _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22);
    }
 
  public override bool Equals(object o) {
      return o is Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> && this.Equals((Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22>) o);
    }

    public bool Equals(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> t) {
      return (
Smooth.Collections.EqComparer<P1>.Default.Equals(_1, t._1) &&
Smooth.Collections.EqComparer<P2>.Default.Equals(_2, t._2) &&
Smooth.Collections.EqComparer<P3>.Default.Equals(_3, t._3) &&
Smooth.Collections.EqComparer<P4>.Default.Equals(_4, t._4) &&
Smooth.Collections.EqComparer<P5>.Default.Equals(_5, t._5) &&
Smooth.Collections.EqComparer<P6>.Default.Equals(_6, t._6) &&
Smooth.Collections.EqComparer<P7>.Default.Equals(_7, t._7) &&
Smooth.Collections.EqComparer<P8>.Default.Equals(_8, t._8) &&
Smooth.Collections.EqComparer<P9>.Default.Equals(_9, t._9) &&
Smooth.Collections.EqComparer<P10>.Default.Equals(_10, t._10) &&
Smooth.Collections.EqComparer<P11>.Default.Equals(_11, t._11) &&
Smooth.Collections.EqComparer<P12>.Default.Equals(_12, t._12) &&
Smooth.Collections.EqComparer<P13>.Default.Equals(_13, t._13) &&
Smooth.Collections.EqComparer<P14>.Default.Equals(_14, t._14) &&
Smooth.Collections.EqComparer<P15>.Default.Equals(_15, t._15) &&
Smooth.Collections.EqComparer<P16>.Default.Equals(_16, t._16) &&
Smooth.Collections.EqComparer<P17>.Default.Equals(_17, t._17) &&
Smooth.Collections.EqComparer<P18>.Default.Equals(_18, t._18) &&
Smooth.Collections.EqComparer<P19>.Default.Equals(_19, t._19) &&
Smooth.Collections.EqComparer<P20>.Default.Equals(_20, t._20) &&
Smooth.Collections.EqComparer<P21>.Default.Equals(_21, t._21) &&
Smooth.Collections.EqComparer<P22>.Default.Equals(_22, t._22)
      );
    }

    public override int GetHashCode() {
      unchecked {
        int hash = 17;
hash = 29 * hash + Smooth.Collections.EqComparer<P1>.Default.GetHashCode(_1);
hash = 29 * hash + Smooth.Collections.EqComparer<P2>.Default.GetHashCode(_2);
hash = 29 * hash + Smooth.Collections.EqComparer<P3>.Default.GetHashCode(_3);
hash = 29 * hash + Smooth.Collections.EqComparer<P4>.Default.GetHashCode(_4);
hash = 29 * hash + Smooth.Collections.EqComparer<P5>.Default.GetHashCode(_5);
hash = 29 * hash + Smooth.Collections.EqComparer<P6>.Default.GetHashCode(_6);
hash = 29 * hash + Smooth.Collections.EqComparer<P7>.Default.GetHashCode(_7);
hash = 29 * hash + Smooth.Collections.EqComparer<P8>.Default.GetHashCode(_8);
hash = 29 * hash + Smooth.Collections.EqComparer<P9>.Default.GetHashCode(_9);
hash = 29 * hash + Smooth.Collections.EqComparer<P10>.Default.GetHashCode(_10);
hash = 29 * hash + Smooth.Collections.EqComparer<P11>.Default.GetHashCode(_11);
hash = 29 * hash + Smooth.Collections.EqComparer<P12>.Default.GetHashCode(_12);
hash = 29 * hash + Smooth.Collections.EqComparer<P13>.Default.GetHashCode(_13);
hash = 29 * hash + Smooth.Collections.EqComparer<P14>.Default.GetHashCode(_14);
hash = 29 * hash + Smooth.Collections.EqComparer<P15>.Default.GetHashCode(_15);
hash = 29 * hash + Smooth.Collections.EqComparer<P16>.Default.GetHashCode(_16);
hash = 29 * hash + Smooth.Collections.EqComparer<P17>.Default.GetHashCode(_17);
hash = 29 * hash + Smooth.Collections.EqComparer<P18>.Default.GetHashCode(_18);
hash = 29 * hash + Smooth.Collections.EqComparer<P19>.Default.GetHashCode(_19);
hash = 29 * hash + Smooth.Collections.EqComparer<P20>.Default.GetHashCode(_20);
hash = 29 * hash + Smooth.Collections.EqComparer<P21>.Default.GetHashCode(_21);
hash = 29 * hash + Smooth.Collections.EqComparer<P22>.Default.GetHashCode(_22);
        return hash;
      }
    }

    public int CompareTo(Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> other) {
      int c;
c = Smooth.Collections.Comparer<P1>.Default.Compare(_1, other._1); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P2>.Default.Compare(_2, other._2); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P3>.Default.Compare(_3, other._3); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P4>.Default.Compare(_4, other._4); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P5>.Default.Compare(_5, other._5); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P6>.Default.Compare(_6, other._6); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P7>.Default.Compare(_7, other._7); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P8>.Default.Compare(_8, other._8); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P9>.Default.Compare(_9, other._9); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P10>.Default.Compare(_10, other._10); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P11>.Default.Compare(_11, other._11); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P12>.Default.Compare(_12, other._12); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P13>.Default.Compare(_13, other._13); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P14>.Default.Compare(_14, other._14); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P15>.Default.Compare(_15, other._15); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P16>.Default.Compare(_16, other._16); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P17>.Default.Compare(_17, other._17); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P18>.Default.Compare(_18, other._18); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P19>.Default.Compare(_19, other._19); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P20>.Default.Compare(_20, other._20); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P21>.Default.Compare(_21, other._21); if (c != 0) { return c; }
c = Smooth.Collections.Comparer<P22>.Default.Compare(_22, other._22);
      return c;
    }

    public static bool operator == (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> rhs) {
      return lhs.Equals(rhs);
    }

    public static bool operator != (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> rhs) {
      return !lhs.Equals(rhs);
    }

    public static bool operator > (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> rhs) {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator < (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> rhs) {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> rhs) {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator <= (Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> lhs, Tpl<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, P21, P22> rhs) {
      return lhs.CompareTo(rhs) <= 0;
    } 

  }

}
