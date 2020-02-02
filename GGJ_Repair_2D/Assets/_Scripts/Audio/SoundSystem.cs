using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public AudioSource audioSource;

    [System.Serializable]
    public class SoundEventData
    {
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(SoundEvents i_ID)
    {
        SoundEventData dataForSound;
        if(database.TryGetValue(i_ID, out dataForSound))
        {
            if (dataForSound.clips.Count > 0)
            {
                AudioClip clip = dataForSound.clips[Random.Range(0, dataForSound.clips.Count)];
                if (clip)
                {
                    audioSource.PlayOneShot(clip);
                }
            }
        }
    }
}
