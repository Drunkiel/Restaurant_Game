using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Page
{
    public string pageName;
    public GameObject pageObject;
}

public class PageController : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    private int currentPage;
    [SerializeField] private List<Page> allPages = new();

    public void ChangePage(int side) //-1 or 1
    {
        //Checks if given number is diffrent than -1 or 1
        if (side != -1 && side != 1)
        {
            Debug.LogError($"Given value: {side} is incorrect || -1 or 1 are correct values");
            return;
        }

        allPages[currentPage].pageObject.SetActive(false);
        currentPage += side;

        //Checks if new page index is out of bounds
        if (currentPage < 0) 
            currentPage = allPages.Count - 1;

        if (currentPage >= allPages.Count)
            currentPage = 0;

        //Loads new page
        nameText.text = allPages[currentPage].pageName;
        allPages[currentPage].pageObject.SetActive(true);
    }
}
