using UnityEngine;
using System.Collections;
using Assets.Scripts.UI;
using TMPro;

public class Highscores : MonoBehaviour
{
    [SerializeField]
    private string privateCode = "WXRENG_J-EqP-6viHGAhrAU4dUSSefGUC4hkUTxAA81g";

    [SerializeField]
    private string publicCode = "5c94e52b3eba42041cd68c71";

    [SerializeField]
    private string webURL = "http://dreamlo.com/lb/";

    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private Score score;

    DisplayHighscores highscoreDisplay;
    public Highscore[] highscoresList;

    private void Awake()
    {
        highscoreDisplay = GetComponent<DisplayHighscores>();
    }

    public void AddHighscore()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            StartCoroutine(UploadNewHighscore(inputField.text, score.CurrentScore));
        }
    }

    private IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHighscores();
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    private IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            highscoreDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }

    private void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
        }
    }

}