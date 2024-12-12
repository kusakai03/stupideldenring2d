using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Arc1cs1 : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private TimelineSubtitle subtitle;
    [SerializeField] private List<TimelineDialogue> lines;
    private void Start(){
        timeline.played += OnTimelinePlayed;
    }

    private void OnTimelinePlayed(PlayableDirector director)
    {
        subtitle.SetValue(lines);
        subtitle.PlaySubtitle();
    }
}
