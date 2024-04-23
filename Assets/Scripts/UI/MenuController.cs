using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject[] UIs;
    [SerializeField] private GameObject continueBTN;

    private void Start()
    {
        if (continueBTN == null)
            return;

        if (GameController.isDataLoaded)
            continueBTN.SetActive(true);
        else
            continueBTN.SetActive(false);
    }

    void Update()
    {
        if (pauseMenuUI == null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePauseState();
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
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
