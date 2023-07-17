using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        transform.Rotate(0, -90, 90 * Random.Range(0, 4));
    }

    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteTime * 2));

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.right * SongManager.instance.noteSpawnX, Vector3.right * SongManager.instance.noteDespawnX, t);
        }
    }
}
