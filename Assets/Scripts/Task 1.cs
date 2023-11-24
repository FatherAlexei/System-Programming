using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    void Start()
    {
        NativeArray<int> num = new NativeArray<int>(new int[] { 1,2,3,20,30,40 }, Allocator.Persistent);
        MyJob myJob = new MyJob()
        {
            result = num
        };
        JobHandle jobHandle = myJob.Schedule();
        jobHandle.Complete();
        for (int i = 0; i < num.Length; i++)
            Debug.Log(num[i]);
    }

    public struct MyJob : IJob
    {
        public NativeArray<int> result;
        public void Execute()
        {
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] >= 10)
                    result[i] = 0;
            }
        }
    }
}
