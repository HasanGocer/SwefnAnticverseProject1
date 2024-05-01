using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak
    [Header("Global_Panel")]
    [Space(10)]

    [SerializeField] private GameObject _globalPanel;
    public TMP_Text moneyText, levelText;

    [Header("Start_Panel")]
    [Space(10)]

    public GameObject startPanel;
    [SerializeField] Button _startButton;

    [Header("Setting_Panel")]
    [Space(10)]

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] Image _soundOnImage, _soundOffImage;
    [SerializeField] private Button _musicButton;
    [SerializeField] Image _musicOnImage, _musicOffImage;
    [SerializeField] private Button _vibrationButton;
    [SerializeField] Image _vibrationOnImage, _vibrationOffImage;

    [Header("Finish_Panel")]
    [Space(10)]

    public GameObject winPanel;
    public GameObject failPanel;
    public GameObject barPanel;
    [SerializeField] private Button _winPrizeButton, _winEmptyButton, _failButton;
    [SerializeField] int finishWaitTime;

    public TMP_Text finishGameMoneyText;

    [Header("Loading_Panel")]
    [Space(10)]

    [SerializeField] List<GameObject> _loadingPanel = new List<GameObject>();
    [SerializeField] int _loadingScreenCountdownTime, _loadingLerpSpeed;
    [SerializeField] int _startSceneCount;
    [SerializeField] Image _loadingLerpBar;
    [SerializeField] GameObject _mainLoadingPanel;

    private void Start()
    {
        ButtonPlacement();
        SettingPlacement();
        levelText.text = GameManager.Instance.level.ToString();
    }

    public IEnumerator LoadingScreen()
    {
        _mainLoadingPanel.SetActive(true);
        _loadingPanel[Random.Range(0, _loadingPanel.Count)].SetActive(true);
        _globalPanel.SetActive(false);
        startPanel.SetActive(false);
        float lerpCount = 0;

        while (true)
        {
            lerpCount += Time.deltaTime * _loadingLerpSpeed;
            _loadingLerpBar.fillAmount = Mathf.Lerp(0, 1, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (_loadingLerpBar.fillAmount == 1) break;
        }

        _mainLoadingPanel.SetActive(false);
        for (int i = 0; i < _loadingPanel.Count; i++)
            _loadingPanel[i].SetActive(false);
        _globalPanel.SetActive(true);
        startPanel.SetActive(true);

        CharacterManager.Instance.StartCharacterManager();
        ItemManager.Instance.ItemCountTextPlacement();
        VillageManager.Instance.CharacterStatStart();
        SoundSystem.Instance.MainMusicPlay();
    }
    public IEnumerator NoThanxOnActive()
    {
        _winEmptyButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        _winEmptyButton.gameObject.SetActive(true);
    }

    public void SettingPanelOff()
    {
        SettingBackButton();
    }

    private void SettingPlacement()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.vibration == 1)
        {
            _vibrationOnImage.gameObject.SetActive(true);
            _vibrationOffImage.gameObject.SetActive(false);
        }
        else
        {
            _vibrationOffImage.gameObject.SetActive(true);
            _vibrationOnImage.gameObject.SetActive(false);
        }

        if (gameManager.sound == 1)
        {
            _soundOnImage.gameObject.SetActive(true);
            _soundOffImage.gameObject.SetActive(false);
        }
        else
        {
            _soundOffImage.gameObject.SetActive(true);
            _soundOnImage.gameObject.SetActive(false);
        }

        if (gameManager.music == 1)
        {
            _musicOnImage.gameObject.SetActive(true);
            _musicOffImage.gameObject.SetActive(false);
        }
        else
        {
            _musicOffImage.gameObject.SetActive(true);
            _musicOnImage.gameObject.SetActive(false);
        }

    }
    private void ButtonPlacement()
    {
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _musicButton.onClick.AddListener(MusicButton);
        _soundButton.onClick.AddListener(SoundButton);
        _winPrizeButton.onClick.AddListener(() => StartCoroutine(WinPrizeButton()));
        _winEmptyButton.onClick.AddListener(() => StartCoroutine(WinButton()));
        _failButton.onClick.AddListener(() => StartCoroutine(FailButton()));
        _startButton.onClick.AddListener(StartButton);
    }

    private void StartButton()
    {
        GameManager.Instance.gameStat = GameManager.GameStat.start;
        startPanel.SetActive(false);

    }
    private IEnumerator WinButton()
    {
        GameManager gameManager = GameManager.Instance;

        _winPrizeButton.enabled = false;
        gameManager.SetLevel();
        BarSystem.Instance.BarStopButton(0);
        MoneySystem.Instance.MoneyTextRevork(gameManager.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);

        gameManager.SetLevel();

        SceneManager.LoadScene(_startSceneCount);
    }
    private IEnumerator WinPrizeButton()
    {
        GameManager gameManager = GameManager.Instance;

        _winPrizeButton.enabled = false;
        BarSystem.Instance.BarStopButton(gameManager.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);

        gameManager.SetLevel();

        SceneManager.LoadScene(_startSceneCount);
    }
    private IEnumerator FailButton()
    {
        MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);

        SceneManager.LoadScene(_startSceneCount);
    }
    private void SettingButton()
    {
        if (GameManager.Instance.gameStat != GameManager.GameStat.finish)
        {
            startPanel.SetActive(false);
            _settingGame.SetActive(true);
            _settingButton.gameObject.SetActive(false);
            _globalPanel.SetActive(false);
        }
    }
    private void SettingBackButton()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.intro)
            startPanel.SetActive(true);
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
        _globalPanel.SetActive(true);
    }
    private void VibrationButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.vibration == 1)
        {
            _vibrationOnImage.gameObject.SetActive(false);
            _vibrationOffImage.gameObject.SetActive(true);
            gameManager.vibration = 0;
        }
        else
        {
            _vibrationOffImage.gameObject.SetActive(false);
            _vibrationOnImage.gameObject.SetActive(true);
            gameManager.vibration = 1;
        }

        gameManager.SetVibration();
    }

    private void SoundButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.sound == 1)
        {
            _soundOnImage.gameObject.SetActive(false);
            _soundOffImage.gameObject.SetActive(true);
            gameManager.sound = 0;
        }
        else
        {
            _soundOffImage.gameObject.SetActive(false);
            _soundOnImage.gameObject.SetActive(true);
            gameManager.sound = 1;
        }

        SoundSystem.Instance.SetSoundVolume();
        gameManager.SetSound();
    }

    private void MusicButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.music == 1)
        {
            _musicOnImage.gameObject.SetActive(false);
            _musicOffImage.gameObject.SetActive(true);
            gameManager.music = 0;
        }
        else
        {
            _musicOffImage.gameObject.SetActive(false);
            _musicOnImage.gameObject.SetActive(true);
            gameManager.music = 1;
        }

        SoundSystem.Instance.SetMusicVolume();
        gameManager.SetMusic();
    }

    public void NotBossFinal()
    {
        _winPrizeButton.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = "Next";
    }
}
