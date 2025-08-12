using System.Collections;
using UnityEngine;

public interface ICoroutinesPerformer
{
    Coroutine StartPerform(IEnumerator coroutineFunction);

    void StopPerform(Coroutine coroutine);
}
