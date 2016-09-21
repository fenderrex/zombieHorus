using UnityEngine;
using System.Collections;

public class ThreadedJob
{
    private bool m_IsDone = false;
    private object m_Handle = new object();
    private System.Threading.Thread m_Thread = null;
    public bool IsDone
    {
        get
        {
            bool tmp;
            lock (m_Handle)
            {
                tmp = m_IsDone;
            }
            return tmp;
        }
        set
        {
            lock (m_Handle)
            {
                m_IsDone = value;
            }
        }
    }

    public virtual void Start()
    {
        m_Thread = new System.Threading.Thread(Run);
        m_Thread.Start();
    }
    public virtual void Abort()
    {
        m_Thread.Abort();
    }

    protected virtual void ThreadFunction() { }

    protected virtual void OnFinished() { }

    public virtual bool Update()
    {
        if (IsDone)
        {
            OnFinished();
            return true;
        }
        return false;
    }
    public IEnumerator WaitFor()
    {
        while (!Update())
        {
            yield return null;
        }
    }
    private void Run()
    {
        ThreadFunction();
        IsDone = true;
    }
}

public class Job : ThreadedJob
{
    public Plugin plugin;  // arbitary job data
    protected override void ThreadFunction()
    {
        // Do your threaded task. DON'T use the Unity API here
        plugin.Start();
        while (plugin.Update())
        {
            //= plugin.FIFO;

        }
    }
    protected override void OnFinished()
    {
        plugin.Result();//todo check exit code
    }
}
public class threeding : MonoBehaviour
{
    Plugin plugin;
    Job myJob;

    void Start()
    {
        plugin = gameObject.GetComponentInParent<Plugin>();
        myJob = new Job();
        myJob.plugin = plugin;
        myJob.Start(); // Don't touch any data in the job class after you called Start until IsDone is true.
    }
    void Update()
    {
        plugin.Update();
        if (myJob != null)
        {
            if (myJob.Update())
            {
                // Alternative to the OnFinished callback
                myJob = null;

            }
        }
    }
}