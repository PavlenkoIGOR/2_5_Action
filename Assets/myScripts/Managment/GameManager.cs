using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameControls gameControls;
    MySceneManager sceneManager;
    private bool _isPaused;


    public GameModeChanger _gameModeChanger;
    private string _gameMode;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameControls = new GameControls();
            sceneManager = new MySceneManager();
            DontDestroyOnLoad(gameObject); // Следить за тем, чтобы не уничтожить объект при смене сцены
        }
        else
            Destroy(gameObject);

        _gameModeChanger = new GameModeChanger();
        _gameMode = "FreeWalkMode";
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                _isPaused = true;
                instance.PauseGame();
                Debug.Log($"Is paused {_isPaused}");
            }
            else
            {
                _isPaused = false;
                instance.UnPauseGame();
            }
        }


    }

    public void ChangeMyGameScene(string scene)
    {
        instance.sceneManager.ChangeSceneMethod(scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void ChangeGameMode()
    {
        _gameModeChanger.ChangeMode();
    }
}
