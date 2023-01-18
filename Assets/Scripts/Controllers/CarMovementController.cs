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

        private bool _isNotStarted = true;
        private bool _isVertical = false;

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
            //Move();
        }



        private void Move()
        {
            if (_isNotStarted)
            {
                return;
            }

            _rig.velocity = new Vector3(_rig.velocity.x, _rig.velocity.y, _data.Speed);
        }

        public void OnInputDragged(InputParams inputParams)
        {
            if (!inputParams.CarTransform == transform)
            {
                return;
            }
            if (_isVertical)
            {
                Debug.Log(inputParams.ZValue);
                _rig.AddRelativeForce(Vector3.forward * inputParams.ZValue, ForceMode.Impulse);
            }
            else
            {
                Debug.Log(inputParams.XValue);
                _rig.AddRelativeForce(Vector3.forward * inputParams.XValue, ForceMode.Impulse);

            }
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