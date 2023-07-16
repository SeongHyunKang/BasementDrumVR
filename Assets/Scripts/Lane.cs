using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    public Saber saber;
    public GameObject hitEffectPrefab;

    int spawnIndex = 0;
    int inputIndex = 0;
    void Start()
    {
        saber = GetComponent<Saber>();
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.instance.inputDelayInMilliSeconds / 1000.0);

            if (saber.HasHit())
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    ScoreManager.Hit();
                    print($"Hit on {inputIndex} note");

                    Instantiate(hitEffectPrefab, notes[inputIndex].transform.position, Quaternion.identity);

                    Destroy(notes[inputIndex].gameObject, 1f);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note");
                }
            }

            if (timeStamp + marginOfError <= audioTime)
            {
                ScoreManager.Miss();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }
    }
}
