using UnityEngine;

[CreateAssetMenu(menuName = "Test", fileName = "TestConfig")]
public class TestConfig : ScriptableObject
{
    [field: SerializeField] public int Damage {  get; private set; }
}
