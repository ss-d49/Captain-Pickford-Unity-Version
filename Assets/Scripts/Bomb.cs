using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	public static float bombRadius = 10f;			// Radius within which enemies are killed.
	public static float bombForce = 100f;			// Force that enemies are thrown from the blast.
	public static float fuseTime = 1.5f;
	private static ParticleSystem explosionFX;		// Reference to the particle system of the explosion effect.
    private static Animator anim;              // Reference to the animator component.
    private static bool landed = false;        // Whether or not the crate has landed yet.


	public static IEnumerator BombDetonation()
	{
			ExtAudio.sounding.PlayOneShot(ExtAudio.fuse);
			yield return new WaitForSeconds(fuseTime);
			Explode();
	}


	public static void Explode()
	{		
		Spawner.bombLaid = false;
		if (Spawner.bomb == null)
		{
			Spawner.bomb = GameObject.Find("bombCrate(Clone)");
		}
		
		Collider2D[] enemies = Physics2D.OverlapCircleAll(Spawner.bomb.transform.position, bombRadius, 2 << LayerMask.NameToLayer("Enemies"));

		foreach(Collider2D en in enemies)
		{
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if(rb != null && rb.tag == "Enemy")
			{
				rb.gameObject.GetComponent<Enemy>().HP = 0;
				Vector3 deltaPos = rb.transform.position - Spawner.bomb.transform.position;
				Vector3 force = deltaPos.normalized * bombForce;
				rb.AddForce(force);
			}
		}
		explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		explosionFX.transform.position = Spawner.bomb.transform.position;
		explosionFX.Play();
        ExtAudio.sounding.PlayOneShot(ExtAudio.boom);
		Destroy (Spawner.bomb);
	}
}
