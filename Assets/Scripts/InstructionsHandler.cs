using UnityEngine;
using UnityEngine.UI;
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

public class InstructionsHandler : MonoBehaviour
{
    public GameObject instructionsFolder;
    [HideInInspector]
    public List<GameObject> instructions;
    public Canvas fullscreenPopup;
    public TextMeshProUGUI stepText;
    public GameObject prevBtn;
    public GameObject nextBtn;
    public TextMeshPro nextBtnText;
    private int currentPanel = 0;
    private int nbPanel = 0;

    public List<SkippablePanel> skippablePanels;

    void Start()
    {
        // Initialize instructions list
        foreach (Transform child in instructionsFolder.transform)
        {
            if (child.tag == "Instruction")
            {
                instructions.Add(child.gameObject);
            }
        }

        // Initialize nbPanel
        nbPanel = instructions.Count;
        prevBtn.SetActive(false);
        nextBtn.SetActive(true);

        // Add onClick event to prev and next buttons
        prevBtn.AddComponent<OnClickHandler>().SetOnClick(PrevPanel);
        nextBtn.AddComponent<OnClickHandler>().SetOnClick(NextPanel);

        // Set the first panel
        SetPanel(currentPanel);

        // Set the index for each skippable panel
        for (int i = 0; i < skippablePanels.Count; i++)
        {
            skippablePanels[i].index = instructions.FindIndex(x => x == skippablePanels[i].instructionPanel);
        }

        // Add onClick event to fullscreen button
        foreach (GameObject instruction in instructions)
        {
            foreach (Transform child in instruction.transform)
            {
                if (child.gameObject.GetComponent<Image>() != null && child.gameObject.GetComponent<Button>() == null)
                {
                    Sprite sprite = child.gameObject.GetComponent<Image>().sprite;
                    float spriteRatio = (float)(sprite).texture.width / (float)(sprite).texture.height;

                    child.gameObject.AddComponent<Button>().onClick.AddListener(() => Fullscreen(child.gameObject, sprite, spriteRatio));
                }
            }
        }
    }

    public void NextPanel()
    {
        // Set the next panel
        SetPanel(currentPanel + 1);

        // Set the prev button active
        prevBtn.SetActive(true);

        // Change next button text and action if current panel is the last one
        if (currentPanel == nbPanel - 1)
        {
            nextBtn.GetComponent<OnClickHandler>().SetOnClick(Done);
            nextBtnText.text = "FINI";
        }
    }

    public void PrevPanel()
    {
        // Find the skippable panel
        int index = skippablePanels.FindIndex(x => x.index + 1 + x.nbPanelToSkip == currentPanel);

        // Check if previous panel is skippable and skipped
        if (index != -1 && skippablePanels[index].isSkipped)
        {
            // Set the previous skippable panel
            SetPanel(skippablePanels[index].index);
        }
        else
        {
            // Set the previous panel
            SetPanel(currentPanel - 1);
        }
        // Set the prev button active if current panel is not the first one
        prevBtn.SetActive(currentPanel != 0);

        // Set the next button active if current panel is not the second last one
        nextBtn.SetActive(currentPanel != nbPanel - 2);

        // Change next button text and action if current panel is the second last one
        if (currentPanel == nbPanel - 2)
        {
            nextBtn.GetComponent<OnClickHandler>().SetOnClick(NextPanel);
            nextBtnText.text = "SUIV.";
        }
    }

    public void SkipPanel()
    {
        // Find the skippable panel
        int index = skippablePanels.FindIndex(x => x.instructionPanel == instructions[currentPanel]);
        if (index == -1) return; // Return if the current panel is not skippable

        // Mark the panel as skipped
        skippablePanels[index].isSkipped = true;

        // Set the next panel
        SetPanel(currentPanel + skippablePanels[index].nbPanelToSkip + 1);
    }
    public void Fullscreen(GameObject img, Sprite sprite, float spriteRatio)
    {
        // Get the animation
        RuntimeAnimatorController animation = img.GetComponent<Animator>() != null ? img.GetComponent<Animator>().runtimeAnimatorController : null;

        // Activate the fullscreen popup
        fullscreenPopup.gameObject.SetActive(true);

        // Set the fullscreen image
        Transform FullscreenImage = fullscreenPopup.transform.GetChild(1);
        FullscreenImage.GetComponent<Image>().sprite = sprite;

        // Set the aspect ratio
        FullscreenImage.GetComponent<AspectRatioFitter>().aspectRatio = spriteRatio;

        // Set the animation
        if (animation != null)
        {
            // Play animation
            var animator = FullscreenImage.GetComponent<Animator>();
            animator.runtimeAnimatorController = animation;
            animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, -1, 0f);
        }

        // Set the close button position
        Transform CloseBtn = fullscreenPopup.transform.GetChild(2);
        CloseBtn.localPosition = new Vector3(FullscreenImage.GetComponent<RectTransform>().rect.width / 2, FullscreenImage.GetComponent<RectTransform>().rect.height / 2, 0);
    }

    public void Done()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetPanel(int step)
    {
        // Deactivate current panel
        instructions[currentPanel].SetActive(false);

        // Reset skip status for current panel
        int indexToReset = skippablePanels.FindIndex(x => x.instructionPanel == instructions[currentPanel]);
        if (indexToReset != -1)
        {
            skippablePanels[indexToReset].isSkipped = false;
        }
        // Update current panel index
        currentPanel = step;

        // Activate new panel
        instructions[currentPanel].SetActive(true);

        // Update step text
        stepText.text = "Etape " + (currentPanel + 1) + "/" + nbPanel;

        // Check if new panel is skippable
        int index = skippablePanels.FindIndex(x => x.instructionPanel == instructions[currentPanel]);
        if (index != -1)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
        }
    }
}
