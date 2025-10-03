using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

using TMPro;

public class UnityAuthManager : MonoBehaviour
{
    string user;
    string password;


    [SerializeField] TextMeshProUGUI txt_userID;


    public void SetUser(string s)
    {
        user = s;
    }
    public void SetPassword(string s)
    {
        password = s;
    }


    async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public async void BTN_CreateUser()
    {
        await SignUp(user, password);
    }

    public async void BTN_LoginUser()
    {
        await SignIn(user, password);
    }


    private async Task SignUp(string email, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(email, password);
            Debug.Log("Cuenta creada correctamente");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Error al crear cuenta: {ex.Message}");
        }
    }

    private async Task SignIn(string email, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(email, password);
            Debug.Log($"Logeado correctamente. UserID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Error de login: {ex.Message}");
        }
    }

}