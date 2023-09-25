using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFx : MonoBehaviour
{
	private AudioSource Source;
    AudioSource audioSource 
	{ 
		get 
		{
			float val = PlayerPrefs.GetFloat("SFX");
			Source.volume = val;
			return Source; 
		} 
		set 
		{ } 
	}
    private Transform followTransform;

	private void Awake()
	{
        Source = GetComponent<AudioSource>();
	}
	public string Name
	{
		get
		{
			if (audioSource.clip == null)
				return "";
			return audioSource.clip.name;
		}
	}
	public bool isLoop
	{
		get
		{
			if (audioSource == null || audioSource.clip == null)
				return false;
			return audioSource.loop;
		}
	}

    public AudioSource AudioSource { get => audioSource; }

    [ContextMenu("TestPlay")]
	public void TestPlay()
	{
		if (audioSource.isPlaying)
			KillSoundFx();
		else
			Play(audioSource.clip, true);
	}

	public void Play(AudioClip clip, bool loop = false)
	{
		if (clip != null)
		{
			audioSource.clip = clip;
			audioSource.Play();
			audioSource.loop = loop;
			if (!loop)
				Invoke(nameof(KillSoundFx), clip.length + 0.1f);
		}
	}

	public void Play(AudioClip clip, Vector3 pos, bool loop = false)
	{
		if (clip != null)
		{
			audioSource.clip = clip;
			audioSource.Play();
			audioSource.loop = loop;
			audioSource.transform.position = pos;
			if (!loop)
				Invoke(nameof(KillSoundFx), clip.length + 0.1f);
		}
	}

	public void Play(AudioClip clip, Transform _followTransform, bool loop = false)
	{
		if (clip != null)
		{
			audioSource.clip = clip;
			audioSource.Play();
			audioSource.loop = loop;
			this.followTransform = _followTransform;
			Update();
			if (!loop)
				Invoke(nameof(KillSoundFx), clip.length + 0.1f);
		}
	}


	private void KillSoundFx()
	{
		PooledObject obj = GetComponent<PooledObject>();
		if (obj.isInPool == false)
			GetComponent<PooledObject>().Pool.ReturnObject(gameObject);
		followTransform = null;
		transform.position = Vector3.zero;
		
		SoundSystem.Instance.soundCount[audioSource.clip.name]--;
		if(SoundSystem.Instance.soundCount[audioSource.clip.name] < 0)
        {
			SoundSystem.Instance.soundCount[audioSource.clip.name] = 0;
		}
	}

	public void KillSoundFxManual(bool retain = true)
	{
		PooledObject obj = GetComponent<PooledObject>();
		if (obj.isInPool == false)
		{
			if (retain)
			{
				audioSource.loop = false;
				Invoke(nameof(KillSoundFx), audioSource.clip.length + 0.1f);
			}
			else
				audioSource.Stop();

			GetComponent<PooledObject>().Pool.ReturnObject(gameObject);
		}

		followTransform = null;
		transform.position = Vector3.zero;
	}

	private void Update()
	{
		if (followTransform != null)
			transform.position = followTransform.position;
	}
}
