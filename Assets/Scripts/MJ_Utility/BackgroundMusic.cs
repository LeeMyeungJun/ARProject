using UnityEngine;

    /// </summary>
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoSingle<BackgroundMusic>
{
    [SerializeField]
    private bool loop = true;

    [SerializeField]
    AudioClip bgm;

    public static BackgroundMusic Load()
    {
        GameObject obj = Resources.Load<GameObject>("prefabs/BackgroundMusic");
        if (obj == null)
            return null;
        obj.transform.SetParent(null);
        obj.transform.position = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.identity;
        return obj.GetComponent<BackgroundMusic>();
    }

    AudioSource audio;
    override protected void Awake()
    {
        base.Awake();
        audio = GetComponent<AudioSource>();
        if (bgm != null)
            audio.clip = bgm;
        //DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("music_enabled"))
        {
            var musicEnabled = PlayerPrefs.GetInt("music_enabled");
            if (musicEnabled == 0)
                audio.mute = true;
            else
                audio.loop = loop;
        }
        audio.Play();
    }

    override protected void OnDestroy()
    {
        base.OnDestroy();
        Stop();

    }

    public void SetVolume(float _val)
    {
        audio.volume = _val;
    }
    public void Stop()
    {
        audio.Stop();
    }

    public void ChangeBGM(AudioClip _bgm)
    {
        audio.clip = _bgm;
        audio.Play();
    }
}
