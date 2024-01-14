using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EasyUpdateDemoSDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    private void Awake()
    {
       Instance = this; 
    }

    public GameObject mainMenu;
    public RectTransform gameTitle;
    public Button startButton;


    public GameObject gameOver;
    public Image gameOverImage;
    public TMP_Text gameOverText;
    public GameObject gameReStartButton;


    public GameObject gameUI;
    public TMP_Text countText;

    private void Start()
    {
        UIAnimation();
        
        MsgSystem.instance.RegistMsgAction("PlayerDead", GameOverUILogic);
    }

    private void OnDestroy()
    {
        MsgSystem.instance.RemoveMsgAction("PlayerDead", GameOverUILogic);
    }
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        
    }

    public void GameStartUILogic()
    {
        mainMenu.gameObject.SetActive(false);
    }

    private void UIAnimation()
    {   
        startButton.transform.localScale = new Vector3(0, 0, 1);
        Sequence mainUISequence = DOTween.Sequence();
        mainUISequence.Append
        (
            DOVirtual.Vector3
            (
                new Vector3(0, 700, 0), new Vector3(0, 120, 0), 1f, (targetValue) =>
                {
                    gameTitle.localPosition = targetValue;
                }
            )
        );
        mainUISequence.Append
        (
            DOVirtual.Vector3
            (
                new Vector3(0, 0, 1), new Vector3(1, 1, 0), 1f, (targetValue) =>
                {
                    startButton.transform.localScale = targetValue;
                }
            )
        );
    }

    public void GameOverUILogic(object[] param)
    {
        gameOver.SetActive(true);
        gameReStartButton.SetActive(false);
        gameUI.SetActive(false);

        DOVirtual.Vector3
        (
            new Vector3(3, 3, 1), new Vector3(1, 1, 0), 1f, (targetValue) =>
            {
                gameOverText.transform.localScale = targetValue;
            }
        ).OnComplete( () => gameReStartButton.SetActive(true) );

        DOVirtual.Color
        (
            new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 0.7f), 1f, (targetValue) =>
            {
                gameOverImage.color = targetValue;
            }
        ).OnComplete( () => ReStartUIScale());


    }

    public void CloseGameOver()
    {
        gameOver.SetActive(false);

        gameUI.SetActive(true);

        countText.text = "Current Count : " + "<color=\"red\">"  + "0" + "</color>";
    }

    public void UpdataKillCount(int count)
    {
        countText.text = "Current Count : " + "<color=\"red\">"  + count.ToString() + "</color>";
    }

    private void ReStartUIScale()
    {
        DOVirtual.Color
        (
            new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 1), 1f, (targetValue) =>
            {
                gameReStartButton.GetComponentInChildren<TMP_Text>().color = targetValue;
            }
        );
    }
}
