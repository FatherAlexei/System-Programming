using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Task2 : MonoBehaviour
{
    void Start()
    {
        NativeArray<Vector3> arr1 = new NativeArray<Vector3>(new Vector3[] { Vector3.one,Vector3.one,Vector3.up }, Allocator.Persistent);
        NativeArray<Vector3> arr2 = new NativeArray<Vector3>(new Vector3[] { Vector3.down, Vector3.left, Vector3.right }, Allocator.Persistent);
        NativeArray<Vector3> result = new NativeArray<Vector3>(new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero }, Allocator.Persistent);
        MyJob myJob = new MyJob()
        {
            arr1 = arr1,
            arr2 = arr2,
            result = result
        };
        JobHandle jobHandle = myJob.Schedule(arr1.Length,0);
        jobHandle.Complete();
        for (int i = 0; i < result.Length; i++)
            Debug.Log(result[i]);
    }

    public struct MyJob : IJobParallelFor
    {
        public NativeArray<Vector3> arr1;
        public NativeArray<Vector3> arr2;
        public NativeArray<Vector3> result;
        public void Execute(int index)
        {
            result[index] = arr1[index] + arr2[index];
        }
    }
}
