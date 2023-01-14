using UnityEngine;
using TMPro;

public class EventHandler : MonoBehaviour
{
    public string[] instructions;
    public TextMeshProUGUI displayStep;
    public TextMeshProUGUI displayInstruction;
    public GameObject prevButton;
    public GameObject nextButton;

    private bool isLaserCutterFound = false;
    private int nbStep;
    private int curStep = 0;

    private void Awake()
    {
        nbStep = instructions.Length - 1;
        prevButton.SetActive(false);
        SetStep();
    }

    public void onDecoupeuseFound()
    {
        if (!isLaserCutterFound)
        {
            isLaserCutterFound = true;
            curStep++;
            SetStep();
        }
    }

    public void NextStep()
    {
        curStep++;

        if (curStep > 1 && !prevButton.activeSelf)
        {
            prevButton.SetActive(true);
        }


        if (curStep == nbStep)
        {
            nextButton.SetActive(false);
        }
        SetStep();
    }

    public void PrevStep()
    {
        curStep--;

        if (curStep == 1)
        {
            prevButton.SetActive(false);
        }

        if (curStep < nbStep && !nextButton.activeSelf)
        {
            nextButton.SetActive(true);
        }
        SetStep();
    }

    public void SetStep()
    {
        displayStep.SetText("Etape n°" + curStep.ToString());
        displayInstruction.SetText(instructions[curStep]);
    }
}
