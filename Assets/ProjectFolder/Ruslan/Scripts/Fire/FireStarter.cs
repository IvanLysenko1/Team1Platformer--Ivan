using System;
using UnityEngine;

public class FireStarter : FireBase
{
    public event Action SaveGame;

    private void Start()
    {
        _isBurning = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IFireable>() != null)
        {
            HandleFire(collision.GetComponent<IFireable>());
        }
    }

    private void HandleFire(IFireable fire)
    {
        if (fire is FireStopper)
        {
            this._isBurning = false;
            _fire.SetActive(this._isBurning);
            return;
        }
        if (fire.IsBurning)
        {
            this._isBurning = true;
            _fire.SetActive(this._isBurning);
        }
        if (fire is FireSaver)
        {
            SaveGame?.Invoke();
        }
        fire.HandleFire(_isBurning);
    }
}
