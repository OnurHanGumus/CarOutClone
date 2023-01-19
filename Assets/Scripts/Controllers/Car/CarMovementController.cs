using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CarMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        #endregion
        #region Private Variables
        private Rigidbody _rig;
        private CarManager _manager;
        private CarData _data;

        private InputParams _inputParams;

        private bool _isNotStarted = true;
        private bool _isVertical = false;
        private bool _isClicked = false;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<CarManager>();
            _data = _manager.GetData();
            _isVertical = (transform.eulerAngles.y == 0) || (transform.eulerAngles.y == 180);
        }


        private void FixedUpdate()
        {
            if (!_isClicked)
            {
                return;
            }

            Move(Vector3.forward * (_isVertical? _inputParams.ZValue : _inputParams.XValue));
        }



        private void Move(Vector3 forceDirection)
        {
            if (_isNotStarted)
            {
                return;
            }
            if (_manager.IsCarCrashed)
            {
                _inputParams = new InputParams() { XValue = 0, ZValue = 0 };
                _manager.IsCarCrashed = false;
                _rig.mass = 1000;
                return;
            }
            _rig.mass = 1;
            _rig.velocity = transform.TransformDirection(forceDirection * _data.Speed);
        }

        public void OnInputDragged(InputParams inputParams)
        {
            if (!(inputParams.CarTransform == transform))
            {
                return;
            }
            if (new Vector2(inputParams.XValue, inputParams.ZValue).magnitude < 0.5f)
            {
                return;
            }
            Debug.Log(transform.name);
            _isClicked = true;
            _inputParams = inputParams;
        }

        public void OnReleased()
        {
            
        }


        public void OnPlay()
        {
            _isNotStarted = false;
        }
        public void OnLevelFailed()
        {
            _rig.angularVelocity = Vector3.zero;
            _rig.velocity = Vector3.zero;

        }
        public void OnLevelSuccess()
        {
            _rig.angularVelocity = Vector3.zero;
            _rig.velocity = Vector3.zero;
        }
        public void OnRestartLevel()
        {
            _isNotStarted = true;
            _rig.angularVelocity = Vector3.zero;
            _rig.velocity = Vector3.zero;
            //_isNotStarted = true;
            transform.position = new Vector3(_data.InitializePosX, _data.InitializePosY);
            transform.eulerAngles = Vector3.zero;
        }
    }
}