using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoonMonster.Codetest;

public class UIHUD : MonoBehaviour
{
    #region Editor Fields
    
    [Header("Reloading/Cooldown")]
    [SerializeField]
    private GameObject _reloadingText;

    [SerializeField]
    private TMP_Text _reloadingTimeText;

    #endregion

    #region Editor Fields

    public void UpdateTime(float newTime)
    {
        if (newTime > 0)
        {
            if (!_reloadingText.activeSelf)
                _reloadingText.SetActive(true);

            if (!_reloadingTimeText.gameObject.activeSelf)
                _reloadingTimeText.gameObject.SetActive(true);

            _reloadingTimeText.text = newTime.ToString("0.0");
        }
        else
        {
            if (_reloadingText.activeSelf)
                _reloadingText.SetActive(false);

            if (_reloadingTimeText.gameObject.activeSelf)
                _reloadingTimeText.gameObject.SetActive(false);
        }
    }

    #endregion
}
