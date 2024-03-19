using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject[] UIs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePauseState();
    }

    public void ChangePauseState()
    {
        GameController.isGamePaused = !GameController.isGamePaused;
        pauseMenuUI.SetActive(GameController.isGamePaused);
        Time.timeScale = GameController.isGamePaused ? 0f : 1f;
    }

    public void OpenCloseMenu(int i)
    {
        UIs[i].SetActive(!UIs[i].activeSelf);

        for (int j = 0; j < UIs.Length; j++)
        {
            if (j != i)
                UIs[j].SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
