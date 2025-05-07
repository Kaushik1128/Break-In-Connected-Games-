using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class TypingMiniGame : MonoBehaviourPun
{
    public GameObject miniGameUI;
    public TMP_Text wordDisplay;
    public TMP_InputField inputField;
    public Slider progressBar;
    public Slider alertBar;

    public TMP_Text currentMoneyText; 
    public TMP_Text moneyStolenText; 

    private string[] words = { "proxy", "breach", "exploit", "encrypt", "upload" };
    private string currentWord;
    private float timer;
    private float timeLimit = 7f;
    private float progress;
    private float maxProgress = 100f;
    private int moneyStolen = 0;

    private void Start()
    {
        miniGameUI.SetActive(false);
        if (moneyStolenText != null) moneyStolenText.text = "£0";
        if (currentMoneyText != null) currentMoneyText.text = "£0";
    }

    [PunRPC]
    public void EnableMiniGame()
    {
        if (!photonView.IsMine) return;

        miniGameUI.SetActive(true);
        ResetGame();
        GenerateWord();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }

    void Update()
    {
        if (!miniGameUI.activeInHierarchy) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnSubmit();
            inputField.Select();
            inputField.ActivateInputField();
        }

        timer += Time.deltaTime;
        if (timer > timeLimit)
        {
            alertBar.value += 0.2f;
            timer = 0f;
            inputField.text = "";
            GenerateWord();
        }

        if (alertBar.value >= 1f)
        {
            EndGame(false);
        }

        if (progress >= maxProgress)
        {
            EndGame(true);
        }
    }

    public void OnSubmit()
    {
        if (inputField.text == currentWord)
        {
            progress += 25f;
            progressBar.value = progress / maxProgress;

            moneyStolen += 10000;
            if (currentMoneyText != null)
                currentMoneyText.text = $"£{moneyStolen}";
        }
        else
        {
            alertBar.value += 0.1f;
        }

        inputField.text = "";
        timer = 0f;
        GenerateWord();
    }

    void GenerateWord()
    {
        currentWord = words[Random.Range(0, words.Length)];
        wordDisplay.text = currentWord;
    }

    void ResetGame()
    {
        progress = 0f;
        moneyStolen = 0;
        progressBar.value = 0f;
        alertBar.value = 0f;
        timer = 0f;

        if (currentMoneyText != null)
            currentMoneyText.text = "£0";
    }

    void EndGame(bool success)
{
    miniGameUI.SetActive(false);
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    if (success)
    {
        HeistGameState.hackerSuccess = true;
    }

    if (moneyStolenText != null)
    {
        moneyStolenText.text = success
            ? $"💰 Money Stolen: £{moneyStolen}"
            : "💥 Hack failed! £0 stolen";
    }

    Debug.Log(success
        ? $"Hack success! Total stolen: £{moneyStolen}"
        : "Hack failed!");
}
}



