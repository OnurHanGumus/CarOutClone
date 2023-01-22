using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] FloatingJoystick joystick; //SimpleJoystick paketi eklenmeli


        #endregion

        #region Private Variables

        private bool _isPlayerDead = false;
        private Ray _ray;
        private Transform _clickedTransform;

        #endregion

        #endregion


        private void Awake()
        {
            Init();
        }


        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;

        private void Init()
        {
            Data = GetInputData();
            _clickedTransform = transform;
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(_ray, out hit))
                {
                    _clickedTransform = hit.transform;
                }
                InputSignals.Instance.onClicked?.Invoke(_clickedTransform);
            }
            if (Input.GetMouseButton(0))
            {
                if (!_clickedTransform.gameObject.CompareTag("Car"))
                {
                    return;
                }
                InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
                {
                    XValue = joystick.Horizontal,
                    ZValue = joystick.Vertical,
                });
            }

            if (Input.GetMouseButtonUp(0))
            {
                InputSignals.Instance.onInputReleased?.Invoke();
            }

        }

        private void OnEnableInput()
        {
            
        }

        private void OnDisableInput()
        {
            
        }

        private void OnPlay()
        {
            
        }

        //private bool IsPointerOverUIElement() //Joystick'i doğru konumlandırırsan buna gerek kalmaz
        //{
        //    var eventData = new PointerEventData(EventSystem.current);
        //    eventData.position = Input.mousePosition;
        //    var results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventData, results);
        //    return results.Count > 0;
        //}

        private void OnRestartLevel()
        {
            _clickedTransform = transform;
        }

        private void OnChangePlayerLivingState()
        {
            _isPlayerDead = !_isPlayerDead;
        }

    }
}