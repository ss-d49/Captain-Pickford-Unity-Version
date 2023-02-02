using UnityEngine;
using System.Collections;

public class BackgroundPropSpawner : MonoBehaviour
{
	public static float leftSpawnPosX = -80f;				// The x coordinate of position if it's instantiated on the left.
	public static float rightSpawnPosX = 70f;			// The x coordinate of position if it's instantiated on the right.
	public static float minSpeed = 5f;					// The lowest possible speed of the prop.
	public static float maxSpeed = 8f;					// The highest possible speeed of the prop.
	public static GameObject Bus, Cab, Swan;
	
	public static GameObject CreateInst(entity prefab) {
		bool facingLeft = Random.Range(0,2) == 0;
		float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
		float speed = Random.Range(minSpeed, maxSpeed);	
		
		GameObject propInstance = Instantiate((GameObject)Resources.Load("Environment/"+prefab.e), new Vector3(posX, prefab.pos[1], 0.0f), Quaternion.identity);
		propInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		if(facingLeft)
		{
			speed *= -1f;		
		}
		else
		{
			speed *= 1f;
			Vector3 scale = propInstance.transform.localScale;
			scale.x *= -1;
			propInstance.transform.localScale = scale;
		}
		return propInstance;
	}
	
	
	public static int check(string gos) 
	{
		GameObject go = GameObject.Find(gos+"(Clone)");
		if (go != null)
		{
			if ( (go.transform.position.x < leftSpawnPosX - 0.5f) || (go.transform.position.x > rightSpawnPosX + 0.5f) )
			{
					Destroy(go);
			}
			return 1;
		}
		else
		{
			return 0;
		}
	}
	
	
	public static void bgpropgen()
	{
		entity[] bgprops = {
			new entity("Bus", new float[] {0.0f, -5.5f, 0.0f}, new float[] {0.0f,0.0f,0.0f}, new float[] {0.0f,0.0f,0.0f}),
			new entity("Cab", new float[] {0.0f, -6.4f, 0.0f}, new float[] {0.0f,0.0f,0.0f}, new float[] {0.0f,0.0f,0.0f}),
			new entity("Cushion", new float[] {0.0f, 3.0f, 0.0f}, new float[] {0.0f,0.0f,0.0f}, new float[] {0.0f,0.0f,0.0f})
		};
		
		for (int i=0;i<bgprops.Length;i++)
		{
			if (check(bgprops[i].e) == 0)
			{
				GameObject prop = CreateInst(bgprops[i]);
			}
		}
	}
}
