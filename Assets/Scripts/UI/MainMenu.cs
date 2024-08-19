using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float transitionTime = 1f;
    private float counter, introCounter = 0;
    public bool transition, introTransition = false;
    public GameObject transitionPanel;
    public GameObject introTransitionPanel;
    // Start is called before the first frame update
    void Start()
    {
        introTransition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transition)
        {
            counter += Time.deltaTime;
            Color c = transitionPanel.GetComponent<Image>().color;
            c.a = Mathf.Lerp(0f, 1f, counter / transitionTime);
            transitionPanel.GetComponent<Image>().color = c;
        }

        if (counter >= transitionTime){
            SceneManager.LoadScene("Main");
        }

        if (introTransition)
        {
            introCounter += Time.deltaTime;
            Color c = introTransitionPanel.GetComponent<Image>().color;
            c.a = Mathf.Lerp(1f, 0f, introCounter / transitionTime);
            introTransitionPanel.GetComponent<Image>().color = c;
        }

        if (introCounter >= transitionTime)
        {
            Destroy(introTransitionPanel);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenKbxLink()
    {
        Application.OpenURL("https://kittenbox.games");
    }

    public void StartGame()
    {
        transitionPanel.SetActive(true);
        transition = true;
    }
}
