using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumpHeight = 4;
    private GameManager GM;
    [SerializeField] private AudioClip wing_sfx;
    [SerializeField] private AudioClip hit_sfx;
    [SerializeField] private AudioClip fall_sfx;
    [SerializeField] private AudioClip beep_sfx;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(GM.isAlive){
            if (Input.GetButtonDown("Fire1")){
                Jump();
            }
        }

        if (GM.isAlive && GM.isGameStarted)
		{
			float v = transform.GetComponent<Rigidbody2D>().velocity.y;
			float rotate = Mathf.Min(Mathf.Max(-90, v * 10+60), 30);
			transform.rotation = Quaternion.Euler(0f, 0f, rotate);
		}
		else if (GM.isGameStarted)
		{
			transform.GetComponent<Rigidbody2D>().rotation = -90;
		}
    }

    public void Jump(){
        rb.velocity = new Vector2(0, jumpHeight);
        AudioSource.PlayClipAtPoint(wing_sfx, Vector3.zero);
    }

	void OnTriggerEnter2D (Collider2D other)
	{
        if(GM.isAlive){
            if (other.CompareTag("land") || other.CompareTag("tube") || other.CompareTag("pipe") || other.CompareTag("border"))
            {
                AudioSource.PlayClipAtPoint(hit_sfx, Vector3.zero);
                if(other.CompareTag("border") || other.CompareTag("tube") || other.CompareTag("pipe")){
                    AudioSource.PlayClipAtPoint(fall_sfx, Vector3.zero);
                }
                GM.GameOver();
                rb.velocity = new Vector2(0,0);
            }
            else if (other.CompareTag("score")){
                GM.AddScore();
                AudioSource.PlayClipAtPoint(beep_sfx, Vector3.zero);
            }
        }
        if(!GM.isAlive){
            
			if (other.CompareTag("land"))
			{
				rb.gravityScale = 0;
				rb.velocity = new Vector2(0, 0);
			}
        }
    }
}
