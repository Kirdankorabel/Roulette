using UnityEngine;

namespace Controller
{
    public class RouletteSpinner : MonoBehaviour
    {
        [SerializeField] private string _sectorDataName = "Sectors";
        [SerializeField] private float _initialSpeed = 500f;
        [SerializeField] private float _minSpeed = 5f;
        [SerializeField] private float _spinDuration = 5f;
        [SerializeField] private float _deceleration = 10f;

        private float _startSpeed;
        private float _currentSpeed;
        private float _elapsedTime;
        private bool _isSpinning = false;
        private float _currentAngle;
        private float _decelerationRate;

        public bool IsStopped { get; private set; } = true;

        private void Start()
        {
            GameConroller.OnGameRestared += Stop;
        }

        void FixedUpdate()
        {
            if (_isSpinning)
            {
                Rotate();
            }
        }

        public void StartSpin()
        {
            _startSpeed = _initialSpeed * Random.Range(0.5f, 1.4f);
            _currentSpeed = _startSpeed;
            _elapsedTime = 0f;
            _currentAngle = transform.eulerAngles.z;
            _isSpinning = true;
            IsStopped = false;

            _decelerationRate = (_startSpeed - _minSpeed) / _spinDuration;
        }

        public void Stop()
        {
            _isSpinning = false;
            transform.rotation = Quaternion.identity;
        }

        private void Rotate()
        {
            _elapsedTime += Time.deltaTime;
            _currentSpeed = Mathf.Max(_minSpeed, _startSpeed - _decelerationRate * _elapsedTime);
            _currentAngle += _currentSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, -_currentAngle);

            if (_elapsedTime >= _spinDuration && _currentSpeed <= _minSpeed)
            {
                _isSpinning = false;
                IsStopped = true;
                _currentSpeed = 0f;
            }
        }

        public float GetAngleForSector(int sectorIndex)
        {
            var data = DataManager.GetData(_sectorDataName);
            var anglePerSector = 360f / data.Count;
            var sectorAngle = (sectorIndex - 0.5f) * anglePerSector;

            var adjustedAngle = (sectorAngle - _currentAngle) % 360f;
            return adjustedAngle >= 0 ? adjustedAngle : adjustedAngle + 360f;
        }
    }
}