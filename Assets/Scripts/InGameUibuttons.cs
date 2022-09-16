using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class InGameUibuttons : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button pause;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject ui;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ui.activeSelf)
            {
                OnClose();
            }
            else
            {
                OnOpen();
            }
        }
    }
    public void Exit()
    {
        OnClose();
        SceneManager.LoadScene(0);
    }
    public void Reload()
    {
        OnClose();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNext()
    {
        OnClose();
        if (!SaveManager.instance) { return; }
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings >= next)
        {
            SceneManager.LoadScene(next);
        }
    }
    public void UpdateButtons()
    {
        if (!SaveManager.instance) { return; }
        text.text = "Level: " + SceneManager.GetActiveScene().buildIndex.ToString();
        next.interactable = SceneManager.GetActiveScene().buildIndex + 1 <= SaveManager.instance.HighestLevel;
        
    }
    private void Start()
    {
        UpdateButtons();
    }
    public void OnOpen()
    {
        UpdateButtons();
        ui.SetActive(true);
        pause.gameObject.SetActive(false);
        GameObject bm = GameObject.FindGameObjectWithTag("BeatManager");
        Time.timeScale = 0;
        if (bm == null) { return; }
        bm.GetComponent<BeatManager>().Pause();
        
    }
    public void OnClose()
    {
        ui.SetActive(false);
        pause.gameObject.SetActive(true);
        GameObject bm = GameObject.FindGameObjectWithTag("BeatManager");
        if (bm == null) { return; }
        bm.GetComponent<BeatManager>().Pause();
        Time.timeScale = 1;
    }
}
