using UnityEngine;
using System.Collections;

public class Scorefinal : MonoBehaviour
{
	

	int m_Score;
	void Update ()
	{
		m_Score = PlayerPrefs.GetInt("score");			// The player's score.
		GetComponent<GUIText>().text = "Score: " + m_Score;
	}

}