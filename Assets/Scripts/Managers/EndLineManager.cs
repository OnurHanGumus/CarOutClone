using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class EndLineManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public int TotalCarCount = 0;
        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;
            CarSignals.Instance.onIncreaseTotalCarCount += OnIncreaseTotalCarCount;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;
            CarSignals.Instance.onIncreaseTotalCarCount -= OnIncreaseTotalCarCount;
        }

        private void OnIncreaseTotalCarCount()
        {
            ++TotalCarCount;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnPlay()
        {

        }
        private void OnResetLevel()
        {

        }
    }
}