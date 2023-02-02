using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public static float xMargin = 2f;		// Distance in the x axis the player can move before the camera follows.
	public static float yMargin = 6f;		// Distance in the y axis the player can move before the camera follows.
	public static float xSmooth = 2f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public static float ySmooth = 2f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public static Vector2 maxXAndY = new Vector2(300.0f, 4.0f);		// The maximum x and y coordinates the camera can have.
	public static Vector2 minXAndY = new Vector2(-25.95f, 2.0f);		// The minimum x and y coordinates the camera can have.





	bool CheckXMargin()
	{
		return Mathf.Abs(transform.position.x - Spawner.player.transform.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		return Mathf.Abs(transform.position.y - Spawner.player.transform.position.y) > yMargin;
	}


	void FixedUpdate ()
	{
		if(Spawner.running)
		{
			TrackPlayer();
		}
	}
	
	
	void TrackPlayer ()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if(CheckXMargin())
			targetX = Mathf.Lerp(transform.position.x, Spawner.player.transform.position.x, xSmooth * Time.deltaTime);

		if(CheckYMargin())
			targetY = Mathf.Lerp(transform.position.y, Spawner.player.transform.position.y, ySmooth * Time.deltaTime);

		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
