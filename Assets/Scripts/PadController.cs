using UnityEngine;
using TMPro;

public class PadController : MonoBehaviour
{
    public SpriteRenderer padSpriteRenderer;
    public Sprite basketballSprite;
    public Sprite footballSprite;
    public GameObject gameOverPanel;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    

    private void Start()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Change the pad's sprite based on the current sprite
                if (padSpriteRenderer.sprite == basketballSprite)
                {
                    padSpriteRenderer.sprite = footballSprite;
                }
                else
                {
                    padSpriteRenderer.sprite = basketballSprite;
                }
            }
        }
        scoreText.text  = score.ToString();

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(padSpriteRenderer.sprite == basketballSprite)
        {
            if (collision.gameObject.CompareTag("BasketballBall"))
            {
                Destroy(collision.gameObject);
                IncreaseScore();

            }
            else
            {
                GameOver();
            }
        }

        if(padSpriteRenderer.sprite == footballSprite)
        {
            if (collision.gameObject.CompareTag("FootballBall"))
            {
                Destroy(collision.gameObject);
                IncreaseScore();
            }
            else
            {
                GameOver();
            }
        }
    }

    public void IncreaseScore()
    {
        score++;
    }
    public void GameOver()
    {
        if (score > PlayerPrefs.GetInt("MaxScore"))
        {
            // Update the maximum score
            PlayerPrefs.SetInt("MaxScore", score);
        }

        GameManager.Instance.isGameOver = true;
        this.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        endScoreText.text = score.ToString();
        gameOverPanel.SetActive(true);
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Debug.Log(maxScore);

    }
   

}