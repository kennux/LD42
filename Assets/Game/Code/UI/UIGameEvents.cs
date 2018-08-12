using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK;

public class UIGameEvents : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public string text;
        public float deleteAfter;
    }

    public float lifetime = 10f;
    public List<Entry> entries = new List<Entry>();

    public bool hasNotifications { get { return this.entries.Count >= 0; } }

    public void Start()
    {
        GameEvents.instance.onExecuteEvent += OnExecuteEvent;
    }

    private void Update()
    {
        List<Entry> delete = ListPool<Entry>.Get();

        foreach (var entry in entries)
        {
            if (Time.time >= entry.deleteAfter)
                delete.Add(entry);
        }

        foreach (var d in delete)
            this.entries.Remove(d);

        ListPool<Entry>.Return(delete);
    }

    private void OnExecuteEvent(GameEvent evt)
    {
        this.entries.Add(new Entry()
        {
            deleteAfter = Time.time + this.lifetime,
            text = evt.uiText
        });
    }
}
