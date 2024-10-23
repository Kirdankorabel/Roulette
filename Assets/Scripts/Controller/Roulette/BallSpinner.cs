using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BallSpinner : MonoBehaviour
    {
        [SerializeField] private Transform _roulette;
        [SerializeField] private BounceTrajectory _bounceTrajectory;
        [SerializeField] private RouletteSpinner _rouletteSpinner;

        [Header("Ball settings"), Space]
        [SerializeField] private float _startRadius = 3f;
        [SerializeField] private float _rouletteRadius;
        [SerializeField] private float _stopRadius = 1f;
        [SerializeField] private float _minBallSpeed = 5f;
        [SerializeField] private float _ballDeceleration = 20f;
        [SerializeField] private float _initialBallSpeed = 200f;
        [SerializeField] private float _minStopSpeed = 10f;
        [SerializeField] private float _stoppingAngle = 90f;

        [Header("Bounce settings"), Space]
        [SerializeField] private float _minSpidFoBounce = 200f;
        [SerializeField] private float _bounceMultipler = 1f;
        [SerializeField] private float _bounceDuration = 2f;
        [SerializeField] private float _bounceChance = 0.2f;
        [SerializeField] private int _maxBounceCount = 2;
        [SerializeField] private List<float> _bouncePositions;

        private float _bounceRadius = 0f;
        private float _radius = 3f;
        private float _currentBallSpeed;
        private float _currentBallAngle;
        private int _targetSectorIndex;
        private float _ballPosition;

        private float _bounceTime = 0f;
        private int _bounceCount = 0;
        private float _jumpTimer = 0f;
        private bool _isBouncing = false;
        private int _bounceCurve = -1;

        public static bool IsBallSpinning { get; private set; } = false;

        private void Start()
        {
            GameConroller.OnGameRestared += Stop;
            SetBallPosition(0);
            _radius = _startRadius;
        }

        private void FixedUpdate()
        {
            if (IsBallSpinning)
            {
                RotateBall();
            }
        }

        public void Stop()
        {
            IsBallSpinning = false;
            SetBallPosition(0);
            _radius = _startRadius;
            _bounceCount = 0;
            _isBouncing = false;
        }

        public void SetBallPosition(float position)
        {
            _ballPosition = position;
            _currentBallAngle = Mathf.Lerp(0, 360, position);
            _radius = _startRadius;
            UpdateBallPosition();
        }

        private void UpdateBallPosition()
        {
            var x = Mathf.Cos(_currentBallAngle * Mathf.Deg2Rad) * (_radius + _bounceRadius * _bounceMultipler);
            var y = Mathf.Sin(_currentBallAngle * Mathf.Deg2Rad) * (_radius + _bounceRadius * _bounceMultipler);
            transform.position = new Vector3(_roulette.position.x + x, _roulette.position.y + y, transform.position.z);
        }

        public void StartBallSpin(int targetSector)
        {
            _currentBallAngle = 0f;
            _radius = _startRadius;
            SetBallPosition(_ballPosition);
            _targetSectorIndex = targetSector;
            _currentBallSpeed = Random.Range(_initialBallSpeed * 0.9f, _initialBallSpeed);
            IsBallSpinning = true;
            _bounceTime = 0f;
            _jumpTimer = 0f;
            _bounceCount = 0;
            _isBouncing = false;
        }

        private void RotateBall()
        {
            _currentBallAngle += _currentBallSpeed * Time.deltaTime;
            _currentBallAngle = _currentBallAngle % 360f;

            if (_currentBallSpeed > _minBallSpeed)
            {
                _currentBallSpeed -= _ballDeceleration * Time.deltaTime;
            }

            CheckPosition();
            TryBounce();

            if (_isBouncing)
            {
                Bounce();
            }

            UpdateBallPosition();
        }

        private void CheckPosition()
        {
            var targetBallAngle = _rouletteSpinner.GetAngleForSector(_targetSectorIndex);
            targetBallAngle = (targetBallAngle + 270f) % 360f;

            var angleDifference = Mathf.Abs(_currentBallAngle - targetBallAngle);
            angleDifference = angleDifference > 180f ? 360f - angleDifference : angleDifference;

            if (_currentBallSpeed > _minBallSpeed)
            {
                _currentBallSpeed -= _ballDeceleration * Time.deltaTime;
            }

            if (angleDifference < 1f && _currentBallSpeed <= _minStopSpeed)
            {
                _currentBallAngle = targetBallAngle;
                UpdateBallPosition();
                IsBallSpinning = false;
            }

            if (_currentBallSpeed <= _minStopSpeed && angleDifference < _stoppingAngle)
            {
                _radius = Mathf.MoveTowards(_radius, _stopRadius, Time.deltaTime);
            }
            else
            {
                _radius = Mathf.MoveTowards(_radius, _rouletteRadius, Time.deltaTime * 0.1f);
            }
        }

        private void Bounce()
        {
            _bounceTime = Mathf.Clamp01(_jumpTimer / _bounceDuration);
            _bounceRadius = _bounceTrajectory.GetBounceRadius(_bounceTime, _bounceCurve);
            _jumpTimer += Time.deltaTime;

            if (_jumpTimer >= _bounceDuration)
            {
                _bounceRadius = 0f;
                _isBouncing = false;
            }
        }

        private void TryBounce()
        {
            if (_bounceCount < _maxBounceCount && !_isBouncing)
            {
                _bouncePositions.ForEach(position => TryBounceInPosition(position));
            }
        }

        private void TryBounceInPosition(float position)
        {
            if (_currentBallSpeed > _minSpidFoBounce && Random.value < _bounceChance && Mathf.Abs(_ballPosition - position) < 0.001f)
            {
                _bounceCount++;
                _isBouncing = true;
                _bounceTime = 0f;
                _jumpTimer = 0f;
                _bounceCurve = Random.Range(0, _bounceTrajectory.CurveCount);
            }
        }
    }
}