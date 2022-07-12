using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;
using TMPro;

// ===== Log In Authenticator =====
//  - Manages the log in process
//  - Automatically logs in the user anonymously

public class LogInAuthenticator : MonoBehaviour
{

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public async void SignIn() //Signs in the player with a new anonymous id
    {

        await UnityServices.InitializeAsync();

        //Add listeners
        AuthenticationService.Instance.SignedIn += OnSignIn;
        AuthenticationService.Instance.SignedOut += OnSignOut;

        try //Anonymous Sign In
        {
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync(); // Don't sign out later, since that changes the anonymous token, which would prevent the player from exiting lobbies they're already in.
        }
        catch
        {
            UnityEngine.Debug.LogError("Failed to login. Did you remember to set your Project ID under Services > General Settings?");
            Debug.LogError("LIAuth: Failed to login.");
            throw;
        }
    }

    private void OnSignIn()
    {
        Debug.Log("LIAuth: Signed in successfully.");
    }

    private void OnSignOut()
    {
        Debug.Log("LIAuth: Signed out successfully.");
    }
}
