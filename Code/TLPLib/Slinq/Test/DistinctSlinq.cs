using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.tinylabproductions.TLPLib.Functional;

using Smooth.Slinq;

#if !UNITY_3_5
namespace Smooth.Slinq.Test {
#endif
	
	public class DistinctSlinq : MonoBehaviour {
		private void Update() {
			for (int i = 0; i < SlinqTest.loops; ++i) {
				SlinqTest.Tpls1.Slinq().Distinct(SlinqTest.eq_1).Count();
			}
		}
	}

#if !UNITY_3_5
}
#endif
