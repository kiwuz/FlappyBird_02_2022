using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tube;
    [SerializeField] private float height;
    private float spawnDelay = 2f;
    private float tubeSpeed =-2.5f;
    private float dist;
    private GameObject player;
    [SerializeField] private GameObject[] tubes;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	void SpawnTube ()
	{
		height = Random.Range(-1.5f, 3.4f);
		Vector2 pos = new Vector2(6, height);

		GameObject tube_ = Instantiate(tube, pos, transform.rotation);
        tube_.GetComponent<Rigidbody2D>().velocity = new Vector2(tubeSpeed, 0f);
        tubes = GameObject.FindGameObjectsWithTag("tube");
    
    
	}


    private void Update()
    {
        for (int i =0; i<tubes.Length; i++){
            if(tubes[i] != null){
                if(tubes[i].transform.position.x <= -6.5){
                    Destroy(tubes[i]);
                }
            }
        }
    }
    public void GameOver(){
      CancelInvoke("SpawnTube");
        for (int i =0; i<tubes.Length; i++){
            if(tubes[i] != null){
                tubes[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            }
        }

    }

    public void StartGame(){
        InvokeRepeating("SpawnTube",1f, spawnDelay);

    }

}
