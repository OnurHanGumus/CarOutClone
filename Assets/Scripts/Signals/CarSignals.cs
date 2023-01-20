using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CarSignals : MonoSingleton<CarSignals>
    {
        public UnityAction onIncreaseTotalCarCount = delegate { };
    }
}