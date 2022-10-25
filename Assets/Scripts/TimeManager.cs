using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TimeManager : MonoBehaviour
{
    public class TimeEvent
    {
        public delegate void OnTime();
        public float freq;//�W�v
        public OnTime onTime;
        public float nextTime;
    }

    static List<TimeEvent> timeEvents = new List<TimeEvent>();

    public static void SetTimeEvent(float freq, TimeEvent.OnTime func)
    {
        var te = new TimeEvent();
        te.nextTime = Time.realtimeSinceStartup + freq;
        te.freq = freq;
        te.onTime = func;
        timeEvents.Add(te);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeEvents.ForEach(CheckTime);
    }

    void CheckTime(TimeEvent te)
    {
        var t = Time.realtimeSinceStartup;
        if (t >= te.nextTime)
        {
            te.onTime();
            te.nextTime += te.freq;
        }
    }
}
