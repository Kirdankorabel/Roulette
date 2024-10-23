using UnityEngine;

namespace Controller
{
    public class RollController : MonoBehaviour
    {
        [SerializeField] private string _dataName = "Sectors";

        private static int _lastResult;

        public event System.Action<int> OnRolled;

        public static int LastResult => _lastResult;

        public void Roll()
        {
            _lastResult = Random.Range(0, DataManager.GetData(_dataName).Count);
            OnRolled?.Invoke(_lastResult);
        }
    }
}