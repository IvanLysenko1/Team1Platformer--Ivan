using UnityEngine;

public class FireBase : MonoBehaviour, IFireable, IInteractable
{
    [SerializeField] protected GameObject _fire;
    [SerializeField] protected bool _isBurning = false;
    public bool IsBurning => _isBurning;

    public virtual void HandleFire(bool isFireStarterBurning) { }

    public void OnInteract(CharacterInterractor interactor) 
    {
        _isBurning = interactor.GetComponent<CharacterFire>().IsBurning;
        Debug.Log("FireBase " + _isBurning);
        HandleFire(_isBurning);
        //interactor.GetComponent<CharacterFire>().BraiseFire();
    }

    public void BraiseFire()
    {
        if (_isBurning)
        {
            _isBurning = false;
            _fire.SetActive(_isBurning);
        }
    }
}
