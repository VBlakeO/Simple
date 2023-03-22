using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    static public GameManager2 Instance { get; private set;}

    public float score = 0.0f;
    public float defeatDelay = 0.3f;
    public float changeDelay = 5.0f;

    public bool imortal = false;

    public TextMeshProUGUI scoreText;
    public Image[] chanceImage;

    private bool defeated = false;
    public int chances = 3;

    public ScaleEffectManager scaleEffectManager;
    public Announcement announcement;
    public GameObject defeatScreem;

    private void Start()
    {
        Instance = this;

        if (score < 10)
            scoreText.text = "00" + score.ToString();

        defeatScreem.SetActive(false);

        UpdateChanceImage(chances);

        StartCoroutine(ChangeRhythm());
    }

    public void AddScore(float _score)
    {
        score += _score;

       if(_score < 0 && score < 0)
            score = 0;

       if(score < 10)
            scoreText.text = "00" + score.ToString();
       else if(score < 100)
            scoreText.text = "0" + score.ToString();
       else
            scoreText.text = score.ToString();
    }

    public float GetScore()
    {
        return score;
    }


    public void AddChance()
    {
        if (chances < 3)
            chances++;

        UpdateChanceImage(chances);
    }

    public void RemoveChance()
    {
        if (imortal)
            return;

        if (defeated)
            return;

        if (chances > 0)
            chances--;

        if (chances <= 0)
        {
            StartCoroutine(Defeat());
            StopCoroutine(ChangeRhythm());
        }

        UpdateChanceImage(chances);
    }

    private void UpdateChanceImage(int _id)
    {
        switch (_id)
        {
            case 3:
                chanceImage[0].enabled = true;
                chanceImage[1].enabled = true;
                chanceImage[2].enabled = true;
                break;

            case 2:
                chanceImage[0].enabled = true;
                chanceImage[1].enabled = true;
                chanceImage[2].enabled = false;
                break;

            case 1:
                chanceImage[0].enabled = true;
                chanceImage[1].enabled = false;
                chanceImage[2].enabled = false;
                break;

            case 0:
                chanceImage[0].enabled = false;
                chanceImage[1].enabled = false;
                chanceImage[2].enabled = false;
                break;
        }
    }

    IEnumerator ChangeRhythm()
    {
        WaitForSeconds wfs = new WaitForSeconds(changeDelay);
        
        yield return wfs;

        if (!defeated)
        {
            if (GameplayData.Instance.intervalMultiplier > 0.15f)
                GameplayData.Instance.intervalMultiplier -= 0.05f;

            if (GameplayData.Instance.intervalMultiplier < 0.15f)
                GameplayData.Instance.intervalMultiplier = 0.15f;

            scaleEffectManager.StartEffect();

            print("Acelerou");

            StartCoroutine(ChangeRhythm());
        }
    }

    IEnumerator Defeat()
    {
        WaitForSeconds wfs = new WaitForSeconds(defeatDelay);

        defeated = true;
        announcement.Organize();

        yield return wfs;

        BlockManager2.Instance.Defeat();
        defeatScreem.SetActive(true);

       // scoreText.text = "Defeat";
    }

    public void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
