using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static level1;

public class entity {
    public string e;
    public float[] pos;
    public float[] rot;
	public float[] scale;
	
	public entity(string e, float[] pos, float[] rot, float[] scale)
    {
        this.e = e;
        this.pos = pos;
        this.rot = rot;
		this.scale = scale;
    }
}

public class Spawner : MonoBehaviour
{
	public static bool bombLaid = false;		// Whether or not a bomb has currently been laid.
	public static int bombCount = 0;			// How many bombs the player has.
	public static GameObject bomb;				// Prefab of the bomb.
	public static Vector3 sunoffset;
	public static Vector3 healthbaroffset;
	public static GameObject sun;
	public static GameObject cam;
	public static GameObject player;
	public static GameObject continueBtn;
	public static GameObject Score;
	public static bool paused = false;
	public static bool running = false;
	public static GameObject bg;
	public static GameObject healthHUD;
	public static GameObject bombHUD;
	
	public static void run ()
	{
		if(paused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
		
		bg.GetComponent<SpriteRenderer>().enabled = false;
		Camera.main.backgroundColor =  new Color(96f / 255f, 147f / 255f, 172f / 255f, 1f);
		
		
 		Transform UI = GameObject.Find("UI").transform;
		
		healthHUD = Instantiate((GameObject)Resources.Load("UI/ui_healthDisplay"));
		bombHUD = Instantiate((GameObject)Resources.Load("UI/ui_bombHUD"));		
		bombHUD.transform.SetParent(UI, false);
		
		Score = Instantiate((GameObject)Resources.Load("UI/Score"));		
		Score.transform.SetParent(UI, false);
		
		continueBtn = Instantiate((GameObject)Resources.Load("UI/continueBtn"));		
		continueBtn.transform.SetParent(UI, false);
		
		Instantiate((GameObject)Resources.Load("Props/explosionParticle")).name = "explosionParticle";	
		
		
		TextAsset mytxtData=(TextAsset)Resources.Load("Levels/level"+DontDestroy.levelnum);
		string[] input = mytxtData.text.Split('\n');
		
		int[] ground = new int[input[0].Length];
		for (int i=0;i<input[0].Length;i++) 
		{
			ground[i] = (int) char.GetNumericValue(input[0][i]);
		}
		
 		entity[] entities = new entity[input.Length-1];

		for (int x=1;x<=input.Length-1;x++) 
		{
			string[] temp = input[x].Split('	');
			entities[x-1] = new entity(temp[0],new float[] {float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3])},new float[] {float.Parse(temp[4]), float.Parse(temp[5]), float.Parse(temp[6])}, new float[] {float.Parse(temp[7]), float.Parse(temp[8]), float.Parse(temp[9])});
		}
		

  		for (int i = 0; i < entities.Length; i++)
		{
			GameObject ent = Instantiate((GameObject)Resources.Load(entities[i].e), new Vector3(entities[i].pos[0], entities[i].pos[1], entities[i].pos[2]), Quaternion.Euler(entities[i].rot[0], entities[i].rot[1], entities[i].rot[2]));
			ent.transform.localScale = new Vector3(entities[i].scale[0], entities[i].scale[1], entities[i].scale[2]);
		}

 
 		for (int i = 0; i < ground.Length; i++)
		{				
			if (ground[i] > 0)
			{
				GameObject ent = Instantiate((GameObject)Resources.Load("Environment/Ground"), new Vector3(-49f+(3.78f*i), -7.1f, 0.0f), Quaternion.Euler(0, 0, 0));
				ent.transform.localScale = new Vector3(0.4f,0.4f,1);
			}
		}
		

		
		healthbaroffset = new Vector3(-1.44f,2.1f,0.0f);
		
		player = GameObject.FindGameObjectWithTag("Player");
		cam.GetComponent<CameraFollow>().enabled = true;
		sunoffset = new Vector3(15.0f,8.0f,25.0f);
		sun = GameObject.Find("zimtstern(Clone)");
		
		running = true;		
	}
	
	void Start ()
	{
		cam = Instantiate((GameObject)Resources.Load("mainCamera"));
		cam.name = "mainCamera";
		cam.GetComponent<CameraFollow>().enabled = false;
 		bg = new GameObject();
		bg.name = "bg";
		bg.transform.position = new Vector3(-24.12f, 2.04f, 0.0f);
		bg.transform.localScale = new Vector3(2.5f, 2.4f, 0.0f);
		bg.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Environment/main");		
	}
	
	void Update ()
	{
		if (running)
		{
			if(Input.GetKeyUp(KeyCode.P))
			{
				paused = !paused;
			}
			
			sun.transform.position = cam.transform.position + sunoffset;
			healthHUD.transform.position = player.transform.position + healthbaroffset;
			
			Vector3 p = player.transform.position;
			if (p.x <= -48.0f)
			{
				player.transform.position = new Vector3(-48.0f,p.y,p.z);
			}
			
			if (player.transform.position.y < -5f)
			{
				Destroy (player);
				running = false;
				reset();
				cam = Instantiate((GameObject)Resources.Load("mainCamera"));
				cam.name = "mainCamera";
				cam.GetComponent<CameraFollow>().enabled = false;
				
				run();
				//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
			}
			
			if(player.transform.position.x > 320.0f)
			{
				paused = !paused;
				GameObject contText = GameObject.Find("contText");
				contText.GetComponent<Text>().enabled = true;
				continueBtn.GetComponent<Image>().enabled = true;
				Button btn = continueBtn.GetComponent<Button>();
				btn.onClick.AddListener(ReloadGame);
				
				// reload level.
			}
			
			if(Input.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
			{
				bombCount--;
				bombLaid = true;
				bomb = Instantiate((GameObject)Resources.Load("Props/bomb"), player.transform.position, player.transform.rotation);
				StartCoroutine(Bomb.BombDetonation());				
			}
			bombHUD.GetComponent<Image>().enabled = bombCount > 0;
			
			BackgroundPropSpawner.bgpropgen();
			
			
			
		}
	}

	public static void ReloadGame()
	{
		if (DontDestroy.levelnum < 2)
		{
			DontDestroy.levelnum++;
		}
		else
		{
			QuitApplication.Quit();
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		PlayerPrefs.SetInt("score", Score.GetComponent<Score>().score);
		PlayerPrefs.Save();
	}
	
	void reset()
	{
		GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		for(int i = 0; i < gameObjects.Length; i++)
		{
			if( gameObjects[i].name.Contains("Clone") )
			{
				DestroyImmediate(gameObjects[i]);
			}
		}
		DestroyImmediate(GameObject.Find("explosionParticle"));	
		DestroyImmediate(cam);
		DestroyImmediate(player);
		DestroyImmediate(sun);
		
	}


	
}
