using UnityEngine;
using System.Collections;

[System.Serializable]
public class RadioChannel{
	
	public AudioSource src;
	
	public float targetVolume = 1;
	private float volumeDelta= 0.05f;
	
	public float targetPitch = 1;
	private float pitchDelta = 0.05f;

	private bool active = true;
	
	public void update(){

		if (active) {

			int sign = 1;
		
			//Step the volume and pitch closer to the target
			if (src.volume != targetVolume) {
				sign = src.volume < targetVolume ? 1 : -1;
			
				if (Mathf.Abs (src.volume - targetVolume) < volumeDelta)
					src.volume = targetVolume;
				else
					src.volume += volumeDelta * sign;		
			}
		
			if (src.pitch != targetPitch) {
				sign = src.pitch < targetPitch ? 1 : -1;
			
				if (Mathf.Abs (src.pitch - targetPitch) < pitchDelta)
					src.pitch = targetPitch;
				else
					src.pitch += pitchDelta * sign;		
			}

		} else {
			src.volume = 0;
		}
	}
	
	public void Play(){
		src.Play ();
	}
	
	public void setTargetVolume(float v){
		targetVolume = v;
	}
	
	public void setTargetVolume(float v, float delta){
		targetVolume = v;
		volumeDelta = delta;
	}
	
	public void setTargetPitch(float p){
		targetPitch = p;
	}
	
	public void setTargetPitch(float p, float delta){
		targetPitch = p;
		pitchDelta = delta;
	}

	public void setActive(bool state){
		active = state;
	}
}
