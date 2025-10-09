using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;

using Unity.Services.PlayerAccounts;

using UnityEngine;

public class UnityPlayerAccount : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();

        //await AuthenticationService.Instance.SignInAnonymouslyAsync();

        PlayerAccountService.Instance.SignedIn += OnSignIn;

    }

    public async void BTN_StartPlayerAccountSignInAsync()
    {
        if (PlayerAccountService.Instance.IsSignedIn)
        {
            await SignInWithUnityAuth();
            return;
        }

        try
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (PlayerAccountsException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    public void BTN_SignOut(bool clearSessionToken = false)
    {
        // Sign out of Unity Authentication, with the option to clear the session token
        AuthenticationService.Instance.SignOut(clearSessionToken);

        // Sign out of Unity Player Accounts
        PlayerAccountService.Instance.SignOut();
    }

    async void OnSignIn()
    {
        await SignInWithUnityAuth();
    }

    async Task SignInWithUnityAuth()
    {
        Debug.Log("AuthServ User Obtenido: " + AuthenticationService.Instance.PlayerId);
        Debug.Log("AuthServ Token Obtenido: " + AuthenticationService.Instance.AccessToken);

        Debug.Log("PLayerAccount User Obtenido: " + PlayerAccountService.Instance.AccessToken);


        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
            Debug.Log("Logeado con la cuenta de jugador, de unity");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

}