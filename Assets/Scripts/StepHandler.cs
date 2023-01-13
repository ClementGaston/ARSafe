using UnityEngine;
using TMPro;
public class StepHandler : MonoBehaviour
{
    public GameObject prevBtn;
    public GameObject nextBtn;

    public TextMeshPro displayStep;

    public int step = 0;
    public int maxStep = 3;

    private void Awake() {
        prevBtn.SetActive(false);  
        writeStep();
    }

    public void nextStep() {
        if (step < maxStep) {
            step++;
            writeStep();

            if (step == maxStep) {
                nextBtn.SetActive(false);
            }

            if(step > 0 && !prevBtn.activeSelf) {
                prevBtn.SetActive(true);
            }
        }
    }

    public void prevStep() {
        if (step > 0) {
            step--;
            writeStep();

            if (step < maxStep && !nextBtn.activeSelf) {
                nextBtn.SetActive(true);
            }

            if (step == 0) {
                prevBtn.SetActive(false);
            }
        }

    }

    public void writeStep() {
        displayStep.SetText(step.ToString() + "/" + maxStep.ToString());  
    }
}
