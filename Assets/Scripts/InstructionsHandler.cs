using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SkippablePanel
{
    public GameObject instructionPanel;
    public int nbPanelToSkip;

    [HideInInspector]
    public bool isSkipped;

    [HideInInspector]
    public int index;
}

public class InstructionsHandler : MonoBehaviour
{
    public GameObject[] instructionsPanel;
    public TextMeshProUGUI stepText;
    public GameObject prevBtn;
    public GameObject nextBtn;
    public TextMeshPro nextBtnText;
    private int currentPanel = 0;
    private int nbPanel = 0;

    public SkippablePanel[] skippablePanels;

    void Start()
    {
        nbPanel = instructionsPanel.Length;
        prevBtn.SetActive(false);
        nextBtn.SetActive(true);

        prevBtn.AddComponent<OnClickHandler>().SetOnClick(PrevPanel);
        nextBtn.AddComponent<OnClickHandler>().SetOnClick(NextPanel);

        SetPanel(currentPanel);

        for (int i = 0; i < skippablePanels.Length; i++)
        {
            skippablePanels[i].index = Array.FindIndex(instructionsPanel, x => x == skippablePanels[i].instructionPanel);
        }
    }

    public void NextPanel()
    {
        SetPanel(currentPanel + 1);

        if (!prevBtn.activeSelf)
        {
            prevBtn.SetActive(true);
        }

        if (currentPanel == nbPanel - 1)
        {
            nextBtn.GetComponent<OnClickHandler>().SetOnClick(Done);
            nextBtnText.text = "Fini";
        }
    }


    public void PrevPanel()
    {
        int index = Array.FindIndex(skippablePanels, x => x.index + 1 + x.nbPanelToSkip == currentPanel);

        if (index != -1 && skippablePanels[index].isSkipped)
        {
            SetPanel(skippablePanels[index].index);
        }
        else
        {
            SetPanel(currentPanel - 1);
        }

        if (!nextBtn.activeSelf && currentPanel == nbPanel - 2)
        {
            nextBtn.SetActive(true);
        }

        if (currentPanel == 0)
        {
            prevBtn.SetActive(false);
        }

        if (currentPanel == nbPanel - 2)
        {
            nextBtn.GetComponent<OnClickHandler>().SetOnClick(NextPanel);
            nextBtnText.text = "Suiv.";
        }
    }

    public void SkipPanel()
    {
        int index = Array.FindIndex(skippablePanels, x => x.instructionPanel == instructionsPanel[currentPanel]);

        if (index != -1)
        {
            SetPanel(currentPanel + skippablePanels[index].nbPanelToSkip + 1);
            skippablePanels[index].isSkipped = true;
        }
    }

    public void Done()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetPanel(int step)
    {
        int indexToReset = Array.FindIndex(skippablePanels, x => x.instructionPanel == instructionsPanel[currentPanel]);

        if (indexToReset != -1)
        {
            skippablePanels[indexToReset].isSkipped = false;
        }
        instructionsPanel[currentPanel].SetActive(false);
        currentPanel = step;
        instructionsPanel[currentPanel].SetActive(true);
        stepText.text = "Etape " + (currentPanel + 1) + " sur " + nbPanel;

        int index = Array.FindIndex(skippablePanels, x => x.instructionPanel == instructionsPanel[currentPanel]);

        if (index != -1)
        {
            nextBtn.SetActive(false);
        }
        else if (!nextBtn.activeSelf)
        {
            nextBtn.SetActive(true);
        }
    }
}
