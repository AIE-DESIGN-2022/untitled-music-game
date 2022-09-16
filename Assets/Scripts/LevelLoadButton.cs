using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelLoadButton : MonoBehaviour
{
    [SerializeField]int level;
    Button button;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        level = transform.GetSiblingIndex()+1;
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = level.ToString();
        button = GetComponent<Button>();
        //check if level is available
        
        
        if(level > SaveManager.instance.HighestLevel)
        {
            button.interactable = false;
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(level);
    }
}
