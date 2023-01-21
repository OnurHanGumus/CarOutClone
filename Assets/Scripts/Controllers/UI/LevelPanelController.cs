using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI levelText;
    #endregion
    #region Private Variables


    #endregion
    #endregion
    public void OnPlay()
    {
        GetLevel();
    }


    private void GetLevel()
    {
        levelText.text = "Level " + LevelSignals.Instance.onGetLevelId?.Invoke();

    }

    public void OnRestartLevel()
    {
        
    }
}
