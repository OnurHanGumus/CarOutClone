using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Signals;
using Managers;

public class CarPhysicsController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private CarManager carManager;
    #endregion
    #region Private Variables
    private SplineComputer _spline;
    private SplineFollower _follower;
    private bool _isOutOfWay = false;
    private SplineSample _sample;
    #endregion
    #endregion
    private void Start()
    {
        _spline = SplineSignals.Instance.onGetSpline();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carManager.IsCarCrashed = true;
        }
        else if (other.CompareTag("OldMan"))
        {
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_isOutOfWay)
        {
            return;
        }
        if (other.CompareTag("Platform"))
        {
            _spline.Project(transform.position, ref _sample);

            _isOutOfWay = true;
            _follower = transform.parent.gameObject.AddComponent<SplineFollower>();
            _follower.spline = _spline;
            _follower.followSpeed = 10;
            _follower.updateMethod = SplineUser.UpdateMethod.FixedUpdate;
            _follower.physicsMode = SplineTracer.PhysicsMode.Rigidbody;

            _follower.SetClipRange(_sample.percent, 1d);

        }
    }
}
