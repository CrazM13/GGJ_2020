using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public bool logSounds = false;
    public AudioSource audioSource;
    private SoundEvents lastSoundPlayed = SoundEvents.Invalid;

    [System.Serializable]
    public class SoundEventData
    {
        public bool isEnabled = true;
        public SoundEvents ID;
        public List<AudioClip> clips;
    }

    public List<SoundEventData> allSoundEvents = new List<SoundEventData>();

    // fast access database
    private Dictionary<SoundEvents, SoundEventData> database = new Dictionary<SoundEvents, SoundEventData>();


    #region Singleton Setup
    private static SoundSystem activeInstance;
    public static SoundSystem Instance
    {
        get
        {
            if (activeInstance == null)
            {
                activeInstance = (GameObject.Instantiate<GameObject>( Resources.Load<GameObject>("PR_SoundSystem"))).GetComponent<SoundSystem>();
                activeInstance.BuildDatabase();
            }
            return activeInstance;
        }
    }
    #endregion Singleton Setup

    void BuildDatabase()
    {
        foreach(SoundEventData eventX in allSoundEvents)
        {
            if(!database.ContainsKey(eventX.ID))
            {
                database.Add(eventX.ID, eventX);
            }
        }
    }

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHeroConfirmSound(int unitNumber)
    {
        switch(unitNumber)
        {
            case 1:
                PlaySound(SoundEvents.H1Confirm);
                break;
            case 2:
                PlaySound(SoundEvents.H2Confirm);
                break;
            case 3:
                PlaySound(SoundEvents.H3Confirm);
                break;
            case 4:
                PlaySound(SoundEvents.H4Confirm);
                break;
        }
    }

    public void PlayHeroSelectedSound(int unitNumber)
    {
        switch (unitNumber)
        {
            case 1:
                PlaySound(SoundEvents.H1SelectGreet);
                break;
            case 2:
                PlaySound(SoundEvents.H2SelectGreet);
                break;
            case 3:
                PlaySound(SoundEvents.H3SelectGreet);
                break;
            case 4:
                PlaySound(SoundEvents.H4SelectGreet);
                break;
        }
    }

    public void PlayHeroUnselectedSound(int unitNumber)
    {
        switch (unitNumber)
        {
            case 1:
                PlaySound(SoundEvents.H1Deselect);
                break;
            case 2:
                PlaySound(SoundEvents.H2Deselect);
                break;
            case 3:
                PlaySound(SoundEvents.H3Deselect);
                break;
            case 4:
                PlaySound(SoundEvents.H4Deselect);
                break;
        }
    }

    public void PlayHeroMoveSound(int unitNumber)
    {
        switch (unitNumber)
        {
            case 1:
                PlaySound(SoundEvents.H1Move);
                break;
            case 2:
                PlaySound(SoundEvents.H2Move);
                break;
            case 3:
                PlaySound(SoundEvents.H3Move);
                break;
            case 4:
                PlaySound(SoundEvents.H4Move);
                break;
        }
    }

    public void PlaySound(SoundEvents i_ID, bool allowInterrupt = false)
    {
        if(logSounds)
        {
            Debug.Log("Playing Sound: " + i_ID.ToString());
        }
        SoundEventData dataForSound;
        if(database.TryGetValue(i_ID, out dataForSound))
        {
            if (dataForSound.isEnabled && dataForSound.clips.Count > 0)
            {
                if(!allowInterrupt && audioSource.isPlaying && lastSoundPlayed == i_ID)
                {
                    return;
                }

                AudioClip clip = dataForSound.clips[Random.Range(0, dataForSound.clips.Count)];
                if (clip)
                {
                    audioSource.PlayOneShot(clip);
                    lastSoundPlayed = i_ID;
                }
            }
        }
    }
}
