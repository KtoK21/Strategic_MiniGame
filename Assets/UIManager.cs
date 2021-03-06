﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text ResourceText_Player;
    public Text HealthText_Player;
    public Text ResourceText_Enemy;
    public Text HealthText_Enemy;

    public Text LeftLineText_Player;
    public Text MiddleLineText_Player;
    public Text RightLineText_Player;
    public Text LeftLineText_Enemy;
    public Text MiddleLineText_Enemy;
    public Text RightLineText_Enemy;

    public Text WinnerText;
    public Text TimeText;
    public float remainTime = 300.0f;
    public Button LeftLine;
    public Button MiddleLine;
    public Button RightLine;

    public Button DoubleIncomeButton;
    public Button ResourceObserveButton;
    public Button SwapLineButton;
    public Button FightButtion;

    public Button LeftLineSwapButton;
    public Button MiddleLineSwapButton;
    public Button RightLineSwapButton;

    public Button Debug_IncreaseResourceby10;
    public Button Debug_ToggleEnemyLineText;
    public Button ExitButton;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ToggleEnemyLineInfoText(false);
        ToggleEnemyResourceInfoText(false);
        ToggleLineSwapButton(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        TimeCalc();
        ResourceOnText();
        HealthOnText();
    }

    public void OnLinePress(string line)
    {
        //전투 진행중에는 공격로 충원 불가
        if(!gameManager.IsFightOn)
            gameManager.LineAdd(line, true);
                
    }

    public void ToggleEnemyLineInfoText(bool IsOn)
    {
        LeftLineText_Enemy.gameObject.SetActive(IsOn);
        MiddleLineText_Enemy.gameObject.SetActive(IsOn);
        RightLineText_Enemy.gameObject.SetActive(IsOn);   
    }

    public void ToggleEnemyResourceInfoText(bool IsOn)
    {
        ResourceText_Enemy.gameObject.SetActive(IsOn);
    }

    public void ToggleLineSwapButton(bool IsOn)
    {
        LeftLineSwapButton.gameObject.SetActive(IsOn);
        MiddleLineSwapButton.gameObject.SetActive(IsOn);
        RightLineSwapButton.gameObject.SetActive(IsOn);
    }

    public void OnFightButtonPress()
    {
        if (!gameManager.IsFightOn)
        {
           StartCoroutine( gameManager.FightCalc());
        }
    }

    public void LineCountOnText(Dictionary<string, int> LineCount)
    {
        LeftLineText_Player.text = LineCount["Left_Player"].ToString();
        MiddleLineText_Player.text = LineCount["Middle_Player"].ToString();
        RightLineText_Player.text = LineCount["Right_Player"].ToString();

        LeftLineText_Enemy.text = LineCount["Left_Enemy"].ToString();
        MiddleLineText_Enemy.text = LineCount["Middle_Enemy"].ToString();
        RightLineText_Enemy.text = LineCount["Right_Enemy"].ToString();
    }

    public void ShowWinner(string winner)
    {
        WinnerText.transform.parent.gameObject.SetActive(true);
        WinnerText.text = winner + "가\n승리했습니다!";
    }
    
    public void ResourceOnText()
    {
        ResourceText_Player.text = "자원 : " + gameManager.Resource_Player.ToString();
        ResourceText_Enemy.text = "자원 : " + gameManager.Resource_Enemy.ToString();
    }

    public void HealthOnText()
    {
        HealthText_Player.text = "체력 : " + gameManager.Health_Player.ToString();
        HealthText_Enemy.text = "체력 : " + gameManager.Health_Enemy.ToString();
    }

    public void DoubleIncome()
    {
        gameManager.DoubleIncome(true);
    }

    public void ResourceObserve()
    {
        StartCoroutine(gameManager.ResourceObserve());

    }

    public void SwapLine()
    {
        //고치고싶다...
        if (gameManager.Resource_Player >= 10)
        {
            gameManager.Resource_Player -= 10;
            ToggleLineSwapButton(true);
        }
    }

    public void OnSwapButtonPressed(string line)
    {
        if(gameManager.LineSwap(line))
            ToggleLineSwapButton(false);
    }

    public void OnDebug_IncreaseResourceButtonPressed(int amount)
    {
        gameManager.IncreaseResource(amount);
    }

    public void OnDebug_ShowENemyLinePressd()
    {
        ToggleEnemyLineInfoText(true);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    void TimeCalc()
    {
        remainTime -= Time.deltaTime;
        //제한시간이 0이면 승자를 찾아서 표시.
        if (remainTime < 0)
            gameManager.FindWinner(true);
        else
        {
            int sec = Mathf.FloorToInt(remainTime % 60);

            if (sec < 10)
                TimeText.text = "남은 시간 : " + Mathf.FloorToInt(remainTime / 60) + ":0" + sec;
            else
                TimeText.text = "남은 시간 : " + Mathf.FloorToInt(remainTime / 60) + ":" + sec;
        }
    }

    
}
