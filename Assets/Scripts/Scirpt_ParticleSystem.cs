using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scirpt_ParticleSystem : MonoBehaviour {
	public GameObject psReference;
	private List<ParticleSystem> ps;
	void Start () {
		ps = new List<ParticleSystem>();
	}
	
	private ParticleSystem CreateEmmiter() {
		var go = Instantiate(psReference);
		ParticleSystem newPs = go.GetComponent<ParticleSystem>();
		newPs.Stop();
        Material mat = new Material(Shader.Find("Sprites/Default"));
		newPs.GetComponent<ParticleSystemRenderer>().material = mat;
		ps.Add(newPs);
		return newPs;
	}
	
	private ParticleSystem GetAvialableParticleSystem(Texture text) {
		foreach (ParticleSystem tmps in ps) {
			if (tmps.GetComponent<ParticleSystemRenderer>().material.GetTexture("_MainTex") == text)
				return tmps;
			if (!tmps.IsAlive())
				return tmps;
		}
		return CreateEmmiter();
	}

	public void Emit(ParticleSystem.EmitParams param, Texture sprite, int ammount, Color color) {
		ParticleSystem psToUse = GetAvialableParticleSystem(sprite);
		var main = psToUse.main;
		main.startColor = color * Color.white;
        psToUse.GetComponent<ParticleSystemRenderer>().material.mainTexture = sprite;
		psToUse.Emit(param, ammount);
	}
}
