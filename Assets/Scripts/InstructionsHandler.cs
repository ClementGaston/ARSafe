using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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

[System.Serializable]
public class Instruction
{
    public GameObject panel;
    public GameObject fullscreen;
}

public class InstructionsHandler : MonoBehaviour
{
    public List<Instruction> instructions;
    public TextMeshProUGUI stepText;
    public GameObject prevBtn;
    public GameObject nextBtn;
    public TextMeshPro nextBtnText;
    private int currentPanel = 0;
    private int nbPanel = 0;

    public List<SkippablePanel> skippablePanels;

    void Start()
    {
        nbPanel = instructions.Count;
        prevBtn.SetActive(false);
        nextBtn.SetActive(true);

        prevBtn.AddComponent<OnClickHandler>().SetOnClick(PrevPanel);
        nextBtn.AddComponent<OnClickHandler>().SetOnClick(NextPanel);

        SetPanel(currentPanel);

        for (int i = 0; i < skippablePanels.Count; i++)
        {
            skippablePanels[i].index = instructions.FindIndex(x => x.panel == skippablePanels[i].instructionPanel);
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
            nextBtnText.text = "FINI";
        }
    }


    public void PrevPanel()
    {
        int index = skippablePanels.FindIndex(x => x.index + 1 + x.nbPanelToSkip == currentPanel);

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
            nextBtnText.text = "SUIV.";
        }
    }

    public void SkipPanel()
    {
        int index = skippablePanels.FindIndex(x => x.instructionPanel == instructions[currentPanel].panel);

        if (index != -1)
        {
            SetPanel(currentPanel + skippablePanels[index].nbPanelToSkip + 1);
            skippablePanels[index].isSkipped = true;
        }
    }

    public void Fullscreen()
    {
        Debug.Log("Fullscreen");
        // instructions[currentPanel].fullscreen.SetActive(true);
    }

    public void Done()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetPanel(int step)
    {
        int indexToReset = skippablePanels.FindIndex(x => x.instructionPanel == instructions[currentPanel].panel);

        if (indexToReset != -1)
        {
            skippablePanels[indexToReset].isSkipped = false;
        }
        instructions[currentPanel].panel.SetActive(false);
        currentPanel = step;
        instructions[currentPanel].panel.SetActive(true);
        stepText.text = "Etape " + (currentPanel + 1) + " sur " + nbPanel;

        int index = skippablePanels.FindIndex(x => x.instructionPanel == instructions[currentPanel].panel);

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
