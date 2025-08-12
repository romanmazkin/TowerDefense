using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CoroutinesPerformer _coroutinesPerformerPrefab;

    private ICoroutinesPerformer _coroutinesPerformer;

    private void Awake()
    {
        _coroutinesPerformer = Instantiate(_coroutinesPerformerPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _coroutinesPerformer.StartPerform(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(1);
        Debug.Log("End");
    }
}
