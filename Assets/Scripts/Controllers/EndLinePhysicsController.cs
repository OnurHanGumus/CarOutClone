using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Signals;
using Managers;

public class EndLinePhysicsController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private EndLineManager manager;
    #endregion
    #region Private Variables
    private int _counter = 0;
    #endregion
    #endregion
    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            ++_counter;
            if (_counter == manager.TotalCarCount)
            {
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                ScoreSignals.Instance.onScoreIncrease?.Invoke(Enums.ScoreTypeEnums.Money, _counter * 5);
            }
        }
    }
}
