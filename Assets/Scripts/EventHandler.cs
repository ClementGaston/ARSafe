using UnityEngine;
using TMPro;

public class EventHandler : MonoBehaviour {
    public string[] instructions;
    public TextMeshProUGUI DisplayStep;
    public TextMeshProUGUI DisplayInstruction;
    public GameObject PrevButton;
    public GameObject NextButton;

    private int nbStep;
    private int curStep = 0;

    private void Awake() {
        nbStep = instructions.Length - 1;
        PrevButton.SetActive(false);
        SetStep();
    }

    public void NextStep() {
        curStep++;

        if(curStep > 0 && !PrevButton.activeSelf) {
            PrevButton.SetActive(true);
        }


        if(curStep == nbStep) {
            NextButton.SetActive(false);
        }
        SetStep();
    }

    public void PrevStep() {
        curStep--;

        if(curStep == 0) {
            PrevButton.SetActive(false);
        }
        
        if(curStep < nbStep && !NextButton.activeSelf) {
            NextButton.SetActive(true);
        }
        SetStep();
    }

    public void SetStep() {
        DisplayStep.SetText((curStep + 1).ToString());
        DisplayInstruction.SetText(instructions[curStep]);
    }
}