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
    public class CarManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool IsCarCrashed = false;
        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        private CarData _data;
        private CarMovementController _movementController;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
            _movementController = GetComponent<CarMovementController>();
        }
        public CarData GetData() => Resources.Load<CD_Car>("Data/CD_Car").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onPlay += _movementController.OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful += _movementController.OnLevelSuccess;
            CoreGameSignals.Instance.onLevelFailed += _movementController.OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel += _movementController.OnRestartLevel;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;

            InputSignals.Instance.onInputDragged += _movementController.OnInputDragged;
            InputSignals.Instance.onInputReleased += _movementController.OnReleased;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onPlay -= _movementController.OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -= _movementController.OnLevelSuccess;
            CoreGameSignals.Instance.onLevelFailed -= _movementController.OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel -= _movementController.OnRestartLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;

            InputSignals.Instance.onInputDragged -= _movementController.OnInputDragged;
            InputSignals.Instance.onInputReleased -= _movementController.OnReleased;

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