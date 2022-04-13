using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool isAlive;
    public int score;
    private TubeSpawner TS;
    private PlayerController PC;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text endScoreText;
    [SerializeField] private Text tapText;
    private Animator EndPanelAnim;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject Land_1;
    [SerializeField] private GameObject Land_2;
    private Rigidbody2D land1_rb, land2_rb;
    public bool isGameStarted = false;

    private Animator playerAnimation;
    [SerializeField] private GameObject player;

    void Start()
    {
        land1_rb = Land_1.GetComponent<Rigidbody2D>();
        land2_rb = Land_2.GetComponent<Rigidbody2D>();
        EndPanelAnim = EndPanel.GetComponent<Animator>();
        playerAnimation = player.GetComponent<Animator>();
        playerAnimation.enabled = true;

        TS = FindObjectOfType<TubeSpawner>();
        PC = FindObjectOfType<PlayerController>();

        isAlive = true;
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore(){
        score = score + 1;
        scoreText.text = score.ToString();
        endScoreText.text = "yOUR SCORE : " + score.ToString();

    }

    public void GameOver(){
        isAlive = false;
        TS.GameOver();

        land1_rb.velocity = new Vector2(0,0);
        land2_rb.velocity = new Vector2(0,0);
        
        scoreText.gameObject.SetActive(false);
        EndPanelAnim.SetTrigger("EndPanelAnim");

    }

    private void Update() {
        if (!isGameStarted && Input.GetButtonDown("Fire1"))
        {
            isGameStarted = true;
            StartGame();
        }
        if(isGameStarted ){
            LandController();
        }
    }

    private void StartGame(){
        isAlive = true;
        playerAnimation.enabled = false;
        tapText.gameObject.SetActive(false);
        PC.Jump();
        TS.StartGame();
    }


    public void PlayButton(){
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }


    private void LandController(){
        if(isAlive){
            land1_rb.velocity = new Vector2(-2.5f,0);
            land2_rb.velocity = new Vector2(-2.5f,0);
            Vector3 pos = transform.position;
            pos.x = 10.7f;
            pos.y = -5.35f;
            if(Land_1.transform.position.x <= -8.9){
                Land_1.transform.position = pos; 
            }

            if(Land_2.transform.position.x <= -8.9){
                Land_2.transform.position = pos; 
            }
        }
        if(!isAlive){
            land1_rb.velocity = new Vector2(0,0);
            land2_rb.velocity = new Vector2(0,0);
        }
    }

}
