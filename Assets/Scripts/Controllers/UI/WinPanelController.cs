using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Enums;

public class WinPanelController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables
    [SerializeField] private TextMeshProUGUI moneyText, increasedMoneyText;

    #endregion

    #region Private Variables
    #endregion
    #endregion

    public void OnPlay()
    {
        moneyText.text = ScoreSignals.Instance.onGetMoney?.Invoke().ToString();
    }

    public void OnUpdateText(ScoreTypeEnums scoreType, int increaseValue)
    {
        moneyText.text = (int.Parse(moneyText.text) + increaseValue).ToString();
        increasedMoneyText.text = increaseValue.ToString();
    }
    public void OnRestartLevel()
    {

    }
}
