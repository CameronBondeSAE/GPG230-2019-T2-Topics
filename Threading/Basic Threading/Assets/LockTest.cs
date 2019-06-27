using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LockTest : MonoBehaviour
{
    int count = 0;
    private Thread thread;
    private Thread thread2;
    object lockKey;

    // Start is called before the first frame update
    void Start()
    {
        lockKey = new object();

        // Start new thread
        thread = new Thread(DoStuff);
        thread.Start();
        thread2 = new Thread(DoStuff2);
        thread2.Start();
//        
//        
//        Thread paramTest = new Thread(new ParameterizedThreadStart(CamsFunction));
//        paramTest.Start(new PacketOfStuff()
//        {
//            thing = 5, 
//            stuff = 10
//        });
        
        
        Thread paramTest2 = new Thread( () => CamsFunction(5,10));
        paramTest2.Start();
    }

    private void OnDestroy()
    {
        thread.Abort();
        thread2.Abort();
    }

    public void CamsFunction(int aParameter, int aParameter2)
    {
        Debug.Log("THING! = "+aParameter + " : "+aParameter2);
    }

    public void DoStuff()
    {
        lock (lockKey)
        {
            Debug.Log("Start 1");
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(2000);
                count++;
                Debug.Log("1 woo = " + count);
            }
        }
    }

    public void DoStuff2()
    {
        lock (lockKey)
        {
            Debug.Log("Start 2");
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                count--;
                Debug.Log("2 = " + count);
            }
        }
    }
}