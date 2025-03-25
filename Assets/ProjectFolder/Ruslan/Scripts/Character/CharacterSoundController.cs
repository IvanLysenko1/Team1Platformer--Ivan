using System.Collections;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    [Header("Grass Sounds")]
    [SerializeField] private AudioClip[] _grassStep;
    [SerializeField] private AudioClip[] _grassJump;
    [Header("Water Sounds")]
    [SerializeField] private AudioClip[] _waterStep;
    [SerializeField] private AudioClip[] _waterJump;
    [Header("Stone Sounds")]
    [SerializeField] private AudioClip[] _stoneStep;
    [SerializeField] private AudioClip[] _stoneJump;
    [Header("Wood Sounds")]
    [SerializeField] private AudioClip[] _woodStep;
    [SerializeField] private AudioClip[] _woodJump;
    [Header("Fire Sounds")]
    [SerializeField] private AudioClip _fireOn;
    [SerializeField] private AudioClip _fireOff;

    [SerializeField] private float _stepInterval = 0.4f;
    private CharacterMovementHandler _movementHandler;
    private CharacterJump _characterJump;
    private CharacterFire _characterFire;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private AudioClip _footStepSond;
    private AudioClip _jumpSound;
    private enum Surface { grass, ground, water, wood, stone}
    private Surface _currentSurface;
    private bool _isMoving;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _movementHandler = GetComponentInParent<CharacterMovementHandler>();
        _characterJump = GetComponentInParent<CharacterJump>();
        _characterFire = GetComponentInParent<CharacterFire>();
    }

    private void OnEnable()
    {
        _characterJump.OnJump += PlayJumpClip;
        _characterFire.OnFire += PlayFireSound;
    }

    private void OnDisable()
    {
        _characterJump.OnJump -= PlayJumpClip;
        _characterFire.OnFire -= PlayFireSound;
    }

    void Start()
    {
        StartCoroutine(PlayFootsteps());
        _currentSurface = Surface.grass;
        int objectLayer = gameObject.layer;
        int ignoreLayer = _movementHandler.gameObject.layer;

        Physics2D.IgnoreLayerCollision(objectLayer, ignoreLayer, true);
    }


    void Update()
    {
        _isMoving = Mathf.Abs(_rb.linearVelocityX) > 0.1f && _movementHandler.IsGrounded();
    }

    private AudioClip FootStepSound()
    {
        switch (_currentSurface)
        {
            case Surface.water:
                _footStepSond = _waterStep[Random.Range(0, _waterStep.Length)];
                break;
            case Surface.stone:
                _footStepSond = _stoneStep[Random.Range(0, _stoneStep.Length)];
                break;
            case Surface.wood:
                _footStepSond = _woodStep[Random.Range(0, _woodStep.Length)];
                break;
            default:
                _footStepSond = _grassStep[Random.Range(0, _grassStep.Length)];
                break;
        }
        return _footStepSond;
    }

    private AudioClip JumpSound()
    {
        switch (_currentSurface)
        {
            case Surface.water:
                _jumpSound = _waterJump[Random.Range(0, _waterJump.Length)];
                break;
            case Surface.stone:
                _jumpSound = _stoneJump[Random.Range(0, _stoneJump.Length)];
                break;
            case Surface.wood:
                _jumpSound = _woodJump[Random.Range(0, _woodJump.Length)];
                break;
            default:
                _jumpSound = _grassJump[Random.Range(0, _grassJump.Length)];
                break;
        }
        return _jumpSound;
    }

    private IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (_isMoving)
            {
                _audioSource.PlayOneShot(FootStepSound());
            }
            yield return new WaitForSeconds(_stepInterval);
        }
    }

    private void PlayJumpClip()
    {
        _audioSource.PlayOneShot(JumpSound());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out FireStopper fireStopper))
        {
            _currentSurface = Surface.water;
            _audioSource.PlayOneShot(_waterJump[Random.Range(0, _waterJump.Length)]);
            Debug.Log(_currentSurface);
            return;
        }
        if (collision.TryGetComponent(out FireBridgeTree woodBridge))
        {
            _currentSurface = Surface.wood;
            Debug.Log(_currentSurface);
            return;
        }
        if (collision.TryGetComponent(out StoneBridge stoneBridge) || collision.TryGetComponent(out Interactable interactable))
        {
            _currentSurface = Surface.stone;
            _audioSource.PlayOneShot(_stoneStep[Random.Range(0, _stoneStep.Length)]);
            Debug.Log(_currentSurface);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentSurface = Surface.grass;
        Debug.Log(_currentSurface);
    }

    private void PlayFireSound(bool isFireOn) => _audioSource.PlayOneShot(isFireOn ? _fireOn : _fireOff);
}
