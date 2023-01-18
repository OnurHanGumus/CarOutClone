using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Dreamteck.Splines;

namespace Managers
{
    public class SplineManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        private CarData _data;
        private SplineComputer _computer;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            //_data = GetData();
            _computer = GetComponent<SplineComputer>();
        }
        //public CarData GetData() => Resources.Load<CD_Car>("Data/CD_Player").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;

            SplineSignals.Instance.onGetSpline += OnGetSplineComputer;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;
            SplineSignals.Instance.onGetSpline -= OnGetSplineComputer;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnPlay()
        {

        }
        private SplineComputer OnGetSplineComputer()
        {
            return _computer;
        }
        private void OnResetLevel()
        {

        }
    }
}