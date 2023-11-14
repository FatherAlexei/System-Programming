using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Task2 : MonoBehaviour
{
    public CancellationTokenSource cancellationTokenSource;
    // Start is called before the first frame update
    void Start()
    {
        cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancelToken = cancellationTokenSource.Token;
        UnitTasksAsync(cancelToken);
    }

    void UnitTasksAsync(CancellationToken cancelToken)
    {
        Task task1 = Task.Run(() => Unit1Async(cancelToken));
        Task task2 = Task.Run(() => Unit2Async(cancelToken));
    }
    async Task Unit1Async(CancellationToken cancelToken)
    {
        await Task.Delay(1000);
        Debug.Log("Task 1 ended");
    }
    async Task Unit2Async(CancellationToken cancelToken)
    {
        for (int i = 0; i < 60; i++)
            await Task.Yield();
        Debug.Log("Task 2 ended");
    }
}
