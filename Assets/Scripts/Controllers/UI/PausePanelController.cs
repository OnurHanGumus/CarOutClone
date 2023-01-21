using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PausePanelController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables
    [SerializeField] private TextMeshProUGUI moneyText;
        
    #endregion

    #region Private Variables
    #endregion
    #endregion

    public void OnPlay()
    {
        moneyText.text = ScoreSignals.Instance.onGetMoney?.Invoke().ToString();
    }
    public void ClosePausePanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.PausePanel);
        Time.timeScale = 1f;
    }
    public void MainMenuBtn()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.PausePanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
        Time.timeScale = 1f;

    }
    public void ExitBtn()
    {
        Application.Quit();
    }


}
