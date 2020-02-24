using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    int score = 0;
    [SerializeField]
    int multiplier;
    [SerializeField]
    int numberSlices;
    [SerializeField]
    int countTouches;
    [SerializeField]
    float timerStartAt = 20f;
    float timer;
    bool playerWon = false;
    [SerializeField]
    float minRandom = 4f;
    [SerializeField]
    float maxRandom = 30f;

    [Header("UI")]
    [SerializeField]
    Text multiplierText;
    [SerializeField]
    Text slicesCutText;
    [SerializeField]
    Text slicesNeededText;
    [SerializeField]
    Text timerText;
    [SerializeField]
    Text scoreText;

    //Select random number of slices
    public void RandomNumberOfSlices() {
        numberSlices = (int)Random.Range(minRandom, maxRandom);
    }
    //Check Win Condition
    public void CheckWin() {
        if (countTouches == numberSlices)
        {
            playerWon = true;
            RandomNumberOfSlices();
            ResetCounter();
            ResetTimer();
            ScoreAdd();
            MultiplierAdd();
            UpdateUI();
        }
        else if (countTouches > numberSlices) {
            ResetAll();
           
        }
    }
    //Score on Win
    public void ScoreAdd() {
        score += (100 * multiplier);
    }
    //Add 1 to multiplier
    public void MultiplierAdd() {
        multiplier += 1;
    }
    //Reset everithing when losing
    public void ResetAll() {
        playerWon = false;
        RandomNumberOfSlices();
        ResetTimer();
        ResetCounter();
        multiplier = 1;
        UpdateUI();
    }
    //Reset Counter of touches
    public void ResetCounter() {
        countTouches = 0;
    }
    //Add touches to ounter of touches
    public void AddTouchesToCounter() {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began) {
                countTouches += 1;
            }
        }
    }

    //Update UI;
    public void UpdateUI()
    {
        multiplierText.text = "X" + multiplier;
        scoreText.text= score.ToString();
        slicesCutText.text = countTouches.ToString();
        slicesNeededText.text = numberSlices.ToString();

    }
    //Reset Timer
    public void ResetTimer() {
        timer = timerStartAt;
    }
    //Timer per game
    public void Clock() {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString();
        if (timer <= 0) {
            CheckWin();
            if (!playerWon)
            {
                ResetAll();
            }
            else {playerWon = false;}
        }
    }
    private void Start()
    {
        ResetAll();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            AddTouchesToCounter();
            UpdateUI();
        }
        else {CheckWin();}
        Clock();
    }
}
