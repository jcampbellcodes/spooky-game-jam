using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

// TODO -- make the audiosources not public
// find a scalar for the pitch of pulse and engine

public class PlayerSounds : MonoBehaviour {
	
	[SerializeField] private AudioSource pulse; // pitch of source updated
	[SerializeField] private AudioSource mechanics; // volume in mixer updated
	[SerializeField] private AudioSource suck;
	[SerializeField] private AudioClip[] airClip;

	float maxGroundVol;
	float maxEngineVol;
	public float velocityCap = 20;

	public AudioMixer _aud;

	Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();

		_aud.GetFloat("groundVol", out maxGroundVol);
		_aud.GetFloat("engineVol", out maxEngineVol);

        pulse.spatialBlend = 0.0f;             // Comment out for 3D sound
        mechanics.spatialBlend = 0.0f;
        suck.spatialBlend = 0.0f;   
    }
	
	// Update is called once per frame
	void Update () {

		float vel = rb.velocity.magnitude;


		// 20log(ratio) is conversion to dB
		float scalar = 20 * Mathf.Log10(vel / velocityCap);
		pulse.pitch = 1 + vel / velocityCap;
		mechanics.pitch = 1 + vel / velocityCap;

		// hard set caps
// 		if(scalar <= maxGroundVol)
// 			_aud.SetFloat("groundVol", scalar);
// 
// 		if(scalar <= maxEngineVol)
// 			_aud.SetFloat("engineVol", scalar);

		if(Input.GetKeyDown(KeyCode.Q))
		{
			PlaySuckSound();

		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			StopSuckSound();
			
		}
	
	}

	public void PlaySuckSound()
	{
	//	Debug.Log ("playing");
		//suck.Stop();

		if(suck.clip != airClip[0]) suck.clip = airClip[0];
		if(!suck.isPlaying) suck.Play ();
		_aud.SetFloat("suckVol", -14f);
		//suck.pitch = -1;
	}

	public void PlayBlowSound()
	{
		//Debug.Log ("playing");
		if(suck.clip != airClip[1]) suck.clip = airClip[1];
		if(!suck.isPlaying) suck.Play ();
		_aud.SetFloat("suckVol", -19f);
		//suck.pitch = 1;
	}

	public void StopSuckSound()
	{
//		Debug.Log ("not playing");
		_aud.SetFloat("suckVol", -80.0f);
		//suck.Stop();
	}

}
