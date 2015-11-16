using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Cái này trực tiếp quản lý 2 thứ đó là GameControler ở từng màn và player controller
/// </summary>
public class MainController : MonoBehaviour
{



    /// <summary>
    /// Muốn thực hiện kiểm tra 1 cái gì đó thì đặt vào cái biến này!
    /// </summary>
    public bool isTesting;

    /// <summary>
    /// Level muốn test
    /// </summary>
    public int testLevel;

    public MainPlayer mainPlayer;

    //Tham chiếu đến controller của các levels
    public BaseGameController[] levelControllers;

    #region Các thuộc tính của Main Manager
    private static MainController _instance;
    public static MainController Instance { get { return _instance; } }


    /// <summary>
    /// Lưu level hiện tại đang chơi
    /// </summary>
    private static int _currentLevel = 0;
    public int CurrentLevel { get { return _currentLevel; } }
    public static void SetCurrentLevel(int lv)
    {
        _currentLevel = lv;
    }

    //Kiểm soát trạng thái pause
    private bool _isPause = false;
    public bool IsPause { get { return _isPause; } }

    public bool IsPlaying
    {
        get
        {
            return _isPause == false && mainPlayer.IsAlive;
        }
    }

    #endregion
    public BaseGameController CurrentGameController { get { return levelControllers[CurrentLevel]; } }
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //Debug.Log("MainController.Start");
        mainPlayer.OnPlayerDieEvent += OnPlayerDie;

        if (testLevel > 0)
            _currentLevel = testLevel;

        txt_diem.gameObject.SetActive(true);
        levelControllers[_currentLevel].Ready();
        mainPlayer.ActivePlayerControllerAtLevel(_currentLevel);
    }

    void Update()
    {
        txt_diem.text = mainPlayer.CurrentStar.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ui_GamePausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Hàm này được subcribe vào sự kiện MainPlayer.OnPlayerDieEvent
    /// </summary>
    void OnPlayerDie()
    {
        //Debug.Log("MainController.OnPlayerDie");
        float p = mainPlayer.CurrentLevelController.PhanTramQuangDuongDaDiDuoc();
        int k = (int)(p * 100);
        txt_phanTramQuangDuong.text = k.ToString() + "%";
        ui_GameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Được gọi đến trong MainPlayer.OnLevelFinishEvent, xử lý các controller
    /// </summary>
    public void OnLeveFinish()
    {
        Debug.Log("MainController.player_OnLeveFinish");
        levelControllers[_currentLevel].LevelFinish();
        _currentLevel++;
        levelControllers[_currentLevel].Ready();
    }


    #region Các tương tác với người dùng ngoài giao diện

    public GameObject ui_GameOverPanel;
    public GameObject ui_GamePausePanel;
    public GameObject ui_GameMenuPanel;

    public Text txt_diem;
    public Text txt_result;
    public Text txt_phanTramQuangDuong;

    public void Click_Replay()
    {
        Time.timeScale = 1;
        ui_GameOverPanel.SetActive(false);
        Debug.Log("MainController.Click_Replay");
        levelControllers[_currentLevel].ResetLevel();
        mainPlayer.ActivePlayerControllerAtLevel(_currentLevel);
    }


    public void Click_Menu()
    {
        Time.timeScale = 1;
        //Tắt mọi hoạt động game, hiển thị menu
        Application.LoadLevel(SceneName.Intro);
    }

    public void Click_Map()
    {
        Time.timeScale = 1;
        Application.LoadLevel(SceneName.Map);
    }

    public void Click_Pause()
    {
        //Thực hiện pause game và hiện thị menu pause
        ui_GamePausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Click_Resume()
    {
        //Ẩn menu pause, cho về chơi tiếp
        ui_GamePausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Click_Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    #endregion
}
