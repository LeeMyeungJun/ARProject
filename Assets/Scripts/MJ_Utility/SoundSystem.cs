// Copyright (C) 2018-2019 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;



public class SoundSystem : MonoSingleton<SoundSystem>
{
	public List<SoundCollection> Collections = new List<SoundCollection>();

	[SerializeField]
	private SoundPoolSizeCtrl soundFxPool;
	[SerializeField]
	private SoundPoolSizeCtrl soundFx3dPool;
	private readonly Dictionary<string, AudioClip> nameToSound = new Dictionary<string, AudioClip>();
	public Dictionary<string, int> soundCount = new Dictionary<string, int>();

	public int MaxSoundCount = 5;
	public static SoundSystem Load()
	{
		GameObject obj = Resources.Load<GameObject>("prefabs/SoundSystem");

		obj.transform.SetParent(null);
		obj.transform.position = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		obj.transform.rotation = Quaternion.identity;
		return obj.GetComponent<SoundSystem>();
	}

	override protected void Awake()
	{
		base.Awake();
		if (!PlayerPrefs.HasKey("sound_enabled"))
			PlayerPrefs.SetInt("sound_enabled", 1);

		if (!PlayerPrefs.HasKey("music_enabled"))
			PlayerPrefs.SetInt("music_enabled", 1);

		if (!PlayerPrefs.HasKey("push_enabled"))
			PlayerPrefs.SetInt("push_enabled", 1);

		foreach (var collection in Collections)
		{
			if (collection != null)
			{
				foreach (var sound in collection.Sounds)
                {
					nameToSound.Add(sound.name, sound);
					soundCount.Add(sound.name, 0);
				}
			}
		}
	}

	private void Start()
	{
		soundFxPool.Initialize();
		soundFx3dPool.Initialize();
	}


	public SoundFx PlaySoundFx(string soundName, bool loop = false)
	{
		if (nameToSound.ContainsKey(soundName) == false)
		{
			LMJ.LogError("No sound data : " + soundName);
			return null;
		}

		transform.position = Vector3.zero;

		var clip = nameToSound[soundName];

		if(soundCount[soundName] < MaxSoundCount) //N개
        {
			soundCount[soundName]++;
			if (clip != null)
				return PlaySoundFx(clip, Vector3.zero, loop);
		}

		return null;
	}

	public SoundFx PlaySoundFx(string soundName, Vector3 pos, bool loop = false)
	{
		if (nameToSound.ContainsKey(soundName) == false)
		{
			LMJ.LogError("No sound data : " + soundName);
			return null;
		}

		//transform.position = pos;

		var clip = nameToSound[soundName];

		if (soundCount[soundName] < MaxSoundCount) //N개
		{
			soundCount[soundName]++;
			if (clip != null)
				return PlaySoundFx(clip, pos, loop);
		}

		return null;
	}

	public SoundFx PlaySoundFx(string soundName, Transform parent, bool loop = false)
	{
		if (nameToSound.ContainsKey(soundName) == false)
		{
			LMJ.LogError("No sound data : " + soundName);
			return null;
		}

		//transform.position = pos;

		var clip = nameToSound[soundName];

		if (soundCount[soundName] < MaxSoundCount) //N개
		{
			soundCount[soundName]++;
			if (clip != null)
				return PlaySoundFx(clip, parent, loop);
		}



		return null;
	}

	private SoundFx PlaySoundFx(AudioClip clip, Vector3 pos, bool loop = false)
	{
		if (clip == null)
			return null;

		var sound = PlayerPrefs.GetInt("sound_enabled");
		if (sound == 0)
			return null;

		GameObject obj = null;

		//if (pos == Vector3.zero)
		//	obj = soundFxPool.GetObject(!loop);
		//else
		//	obj = soundFx3dPool.GetObject(!loop);

		if (pos == Vector3.zero)
			obj = soundFxPool.GetObject(loop);
		else
			obj = soundFx3dPool.GetObject(loop);


		if (obj != null && sound == 1 && clip != null)
		{
			SoundFx sfx = obj.GetComponent<SoundFx>();
			sfx.Play(clip, pos, loop);
			return sfx;
		}
		return null;
	}

	private SoundFx PlaySoundFx(AudioClip clip, Transform parent, bool loop = false)
	{
		if (clip == null)
			return null;

		var sound = PlayerPrefs.GetInt("sound_enabled");

		GameObject obj = null;

		//if (parent == null)
		//	obj = soundFxPool.GetObject(!loop);
		//else
		//	obj = soundFx3dPool.GetObject(!loop);
		if (parent == null)
			obj = soundFxPool.GetObject(loop);
		else
			obj = soundFx3dPool.GetObject(loop);

		if (obj != null && sound == 1 && clip != null)
		{
			SoundFx sfx = obj.GetComponent<SoundFx>();
			sfx.Play(clip, parent, loop);
			return sfx;
		}
		return null;
	}

	public void SetSoundEnabled(bool soundEnabled)
	{
		PlayerPrefs.SetInt("sound_enabled", soundEnabled ? 1 : 0);
	}

	public void SetMusicEnabled(bool musicEnabled)
	{
		PlayerPrefs.SetInt("music_enabled", musicEnabled ? 1 : 0);
		var bgMusic = FindObjectOfType<BackgroundMusic>();
		if (bgMusic != null)
			bgMusic.GetComponent<AudioSource>().mute = !musicEnabled;
	}


	public void SoundAllPlay(bool _playing)
    {
		List<GameObject> fxObjects = soundFxPool.GetPoolObjectList();
		List<GameObject> fx3dObjects = soundFxPool.GetPoolObjectList();

		foreach(var obj in fxObjects)
		{
			if (_playing)
				obj.GetComponent<SoundFx>().AudioSource.Play();
			else
				obj.GetComponent<SoundFx>().AudioSource.Pause();
		}

		foreach (var obj in fx3dObjects)
		{
			if (_playing)
				obj.GetComponent<SoundFx>().AudioSource.Play();
			else
				obj.GetComponent<SoundFx>().AudioSource.Pause();
		}

	}
}
