using UnityEngine;
using System.Collections;
using TMPro;

public class DisplayHighscores : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] highscoreFields;

    private Highscores highscoresManager;

    void Start()
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". Fetching...";
        }

        highscoresManager = GetComponent<Highscores>();
        StartCoroutine(RefreshHighscores());
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". ";
            if (i < highscoreList.Length)
            {
                highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score.ToString("N0");
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoresManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}