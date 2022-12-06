using Newtonsoft.Json;
using UnityEngine;
using Zork.Common;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityInputService InputService;

    [SerializeField]
    private UnityOutputService OutputService;

    [SerializeField]
    private TextMeshProUGUI LocationText;

    [SerializeField]
    private TextMeshProUGUI HealthText;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private TextMeshProUGUI MovesText;

    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
        _game.Player.LocationChanged += Player_LocationChanged;
        _game.Player.MovesChanged += Player_MovesChanged;
        _game.Player.ScoreChanged += Player_ScoreChanged;
        _game.Player.HealthChanged += Player_HealthChanged;
        _game.Run(InputService, OutputService);
    }

    private void Player_ScoreChanged(object sender, int score)
    {
        ScoreText.text = ("Score: " + score.ToString());
    }

    private void Player_MovesChanged(object sender, int moves)
    {
        MovesText.text = ("Moves: " + moves.ToString());
    }

    private void Player_HealthChanged(object sender, int health)
    {
        HealthText.text = ("Health: " + health.ToString());
    }

    private void Player_LocationChanged(object sender, Room location)
    {
        LocationText.text = location.Name;
    }

    public void Start()
    {
        InputService.SetFocus();
        LocationText.text = _game.Player.CurrentRoom.Name;
        MovesText.text = ("Moves: " + _game.Player.Moves);
        ScoreText.text = ("Score: " + _game.Player.Score);
        HealthText.text = ("Health: " + _game.Player.Health);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputService.ProcessInput();
            InputService.SetFocus();
        }

        if (_game.IsRunning == false)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    private Game _game;

}
