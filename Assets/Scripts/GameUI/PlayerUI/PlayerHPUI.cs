using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerUI : MonoBehaviour
{
    public Image spirit_ui_green;
    public Image spirit_ui_red;


    public TMP_Text playerName;

    public TMP_Text switchWeaponText;

    private void Start() 
    {
        Init();
    }
    public void Init()
    {

    }

    public void UpdateUI(float percent)
    {  
        spirit_ui_green.fillAmount = percent;

        DOVirtual.Float
        (
            spirit_ui_red.fillAmount, spirit_ui_green.fillAmount, 1f, (current_frame_value) =>
            {
                spirit_ui_red.fillAmount = current_frame_value;
            } 
        
        );
    }

    public void UpdateSwitchWeaponUIText(string text)
    {
        switchWeaponText.text = text;

/*        Sequence quence = DOTween.Sequence();

        quence.Append
        (
            DOVirtual.Vector3
            (
                new Vector3(180, 0, 0), new Vector3(180, 50, 0), 1f, (targetValue) =>
                {
                    switchWeaponText.rectTransform.localPosition = targetValue;
                }
            )
        );

        quence.Append
        (
            DOVirtual.Color
            (
                new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1f, (targetValue) =>
                {
                    switchWeaponText.color = targetValue;
                }
            )
        );*/
        DOVirtual.Vector3
        (
            new Vector3(0, 0, 0), new Vector3(0, 50, 0), 2f, (targetValue) =>
            {
                switchWeaponText.rectTransform.localPosition = targetValue;
            }
        );
        DOVirtual.Color
        (
            new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 2f, (targetValue) =>
            {
                switchWeaponText.color = targetValue;
            }
        );
    }
}
