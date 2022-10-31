using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    public Transform playerTransform;
    public Transform stopTransform;
    public Transform winTransform;
    public Transform playerModel;

    public float speed;
    private int strength;

    [Header("UI")]
    public Text strengthText;
    public Text endgameText;
    public GameObject loseWindow;
    public GameObject winWindow;
    public GameObject navigateCanvas;

    [Header("Boolean Variables")]
    private bool isIdle;
    private bool isWin;
    private bool isRunWinScene;
    private bool canPlay;

    [Header("Animator")]
    public Animator playerAnimator;

    [Header("Others")]
    public GameObject chestClose;
    public GameObject chestOpen;

    private void Start()
    {
        // Player Start Running
        canPlay = false;
        //isIdle = false;
        isWin = false;
        isIdle = false;
        isRunWinScene = false;

        //playerAnimator.SetBool("IsIdle", true);
        playerAnimator.SetTrigger("Idle");
    }

    private void Update()
    {
        if (canPlay)
        {
            if (isWin)
            {
                //playerAnimator.SetTrigger("Move");
                MoveToWin();
            }
            else if (!isIdle)
            {
                MoveToWave();
            }
        }
    }

    public void MoveToWin()
    {
        if (Vector3.Distance(playerTransform.position, winTransform.position) >= 0.1f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, winTransform.position, speed * Time.deltaTime);
        }
        else
        {
            playerTransform.position = winTransform.position;
            if (!isRunWinScene)
            {
                isRunWinScene = true;
                Win();
            }
        }
    }

    public void MoveToWave()
    {
        if (Vector3.Distance(playerTransform.position, stopTransform.position) >= 0.1f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, stopTransform.position, speed * Time.deltaTime);
        }
        else
        {
            playerTransform.position = stopTransform.position;
            playerAnimator.SetTrigger("Idle");
            isIdle = true;
        }
    }

    public void SetStrength(int newStrength)
    {
        strength = newStrength;
        strengthText.text = strength.ToString();
    }

    public int GetStrength()
    {
        return strength;
    }

    public void IncreaseStrength(int increaseStrength)
    {
        strength += increaseStrength;
        strengthText.text = strength.ToString();
    }

    public void Lose()
    {
        // Lose Game
        StartCoroutine(DelayLose());
    }

    IEnumerator DelayLose()
    {
        yield return new WaitForSeconds(1.5f);
        loseWindow.SetActive(true);
    }

    public void RunToWin()
    {
        navigateCanvas.SetActive(false);
        StartCoroutine(DelayRunToWin());
    }

    IEnumerator DelayRunToWin()
    {
        playerAnimator.SetTrigger("Move");
        isWin = true;

        yield return new WaitForSeconds(2f);
    }

    public void Win()
    {
        playerModel.Rotate(0f, 180f, 0f, Space.Self);
        //playerAnimator.SetBool("IsIdle", true);
        //playerAnimator.SetBool("IsWin", true);
        playerAnimator.SetTrigger("Win");
        chestClose.SetActive(false);
        chestOpen.SetActive(true);
        winWindow.SetActive(true);
    }

    public void AllowPlay()
    {
        //playerAnimator.SetBool("IsIdle", false);
        playerAnimator.SetTrigger("Move");
        StartCoroutine(DelayPlay());
    }

    IEnumerator DelayPlay()
    {
        yield return new WaitForSeconds(0.5f);
        canPlay = true;
    }
}
