using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T: MonoBehaviourSingleton<T>
{
    public static T instance { get; protected set; }
 
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            throw new UnityException("An instance of this singleton already exists.");
        }
        else
        {
            instance = (T)this;
        }
    }
}