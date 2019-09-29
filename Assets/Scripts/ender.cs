using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ender : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}
	private Score score;

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		SceneManager.LoadScene("EndLevel", LoadSceneMode.Single);
		score = GameObject.Find("Score").GetComponent<Score>();
		PlayerPrefs.SetInt("score", score.score);
		PlayerPrefs.Save();
	}
}
