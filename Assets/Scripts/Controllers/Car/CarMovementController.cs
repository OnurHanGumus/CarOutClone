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
        private bool _isMoveable = false;
        private bool _isClicked = false;
        private bool _isPositiveRotation = false;

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
            _isPositiveRotation = (transform.eulerAngles.y == 0) || (transform.eulerAngles.y == 90);
        }


        private void FixedUpdate()
        {
            if (!_isMoveable)
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

            _rig.mass = 1;
            _rig.velocity = transform.TransformDirection(forceDirection * _data.Speed * (_isPositiveRotation? 1 : -1));
        }

        public void CarCrashed()
        {
            _isMoveable = false;
            _isClicked = false;
            _rig.mass = 1000;
        }

        public void OnClicked(Transform clickedTransform)
        {
            if (!(clickedTransform == transform))
            {
                return;
            }
            _isClicked = true;
        }

        public void OnInputDragged(InputParams inputParams)
        {
            if (!_isClicked)
            {
                return;
            }
            if (new Vector2(inputParams.XValue, inputParams.ZValue).magnitude < _data.CarMoveTreshold)
            {
                return;
            }
            _isMoveable = true;
            _inputParams = inputParams;
        }

        public void OnReleased()
        {
            _isClicked = false;
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
            transform.position = new Vector3(_data.InitializePosX, _data.InitializePosY);
            transform.eulerAngles = Vector3.zero;
        }
    }
}