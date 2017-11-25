using UnityEngine;
using System.Collections;

//Singleton class will only work with monobehaviours.
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    //This will be the that we can access from anywhere
    private static T instance = null;

    //To ensure persistence between scenes
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            instance = this as T;
        }
    }

    //Getter.  You can use this like: "Sound_Manager.Instance.Play_Event(clip_type.Goal);"
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
}
