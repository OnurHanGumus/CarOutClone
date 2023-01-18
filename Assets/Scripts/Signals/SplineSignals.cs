using Dreamteck.Splines;
using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SplineSignals : MonoSingleton<SplineSignals>
    {
        public Func<SplineComputer> onGetSpline = delegate { return null; };
    }
}
