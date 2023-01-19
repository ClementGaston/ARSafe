using System.Collections.Generic;
using UnityEngine;

public class PagesHandler : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> pages = new List<GameObject>();
    public GameObject pagesFolder;
    public GameObject nextBtn;
    public GameObject prevBtn;
    private int currentPage = 0;

    private void Start()
    {
        foreach (Transform child in pagesFolder.transform)
        {
            pages.Add(child.gameObject);
        }

        pages[currentPage].SetActive(true);
        prevBtn.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < pages.Count - 1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
        }
        if (currentPage == pages.Count - 1)
        {
            nextBtn.SetActive(false);
        }
        if (currentPage > 0)
        {
            prevBtn.SetActive(true);
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].SetActive(false);
            currentPage--;
            pages[currentPage].SetActive(true);
        }
        if (currentPage == 0)
        {
            prevBtn.SetActive(false);
        }
        if (currentPage < pages.Count - 1)
        {
            nextBtn.SetActive(true);
        }
    }
}
