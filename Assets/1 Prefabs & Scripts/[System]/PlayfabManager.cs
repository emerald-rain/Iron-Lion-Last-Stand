using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    [Header("Login UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [Header("Register UI")]
    public TMP_InputField regEmailInput;
    public TMP_InputField regUsernameInput;
    public TMP_InputField regPasswordInput;

    [Header("Registration and Login game objects")]
    public GameObject logInGameObject;
    public GameObject registrationGameObject;

    [Header("Leaderboard")]
    public GameObject rowPrefab;
    public Transform rowsParent;

    // New account registation
    public void RegisterButton() {
        // Check if password is shorter than 6 characters.
        if (regPasswordInput.text.Length < 6) {
            messageText.text = "Password is too short.";
            return;
        }

        // Registration request to Playfab
        var request = new RegisterPlayFabUserRequest {
            Email = regEmailInput.text,
            DisplayName = regUsernameInput.text,
            Password = regPasswordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void BackToLogInButton() {
        registrationGameObject.SetActive(false);
        logInGameObject.SetActive(true);
    }

    public void RegisterAccountButton() {
        registrationGameObject.SetActive(true);
        logInGameObject.SetActive(false);
    }

    // Login process
    public void LoginButton() {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        messageText.text = "Successfull Registration!";
        BackToLogInButton();
    }

    void OnError(PlayFabError error) {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLoginSuccess(LoginResult result) {
        messageText.text = "Logged in!";
        Debug.Log("Successfull Login!");
        SceneManager.LoadScene("WaveAttack");
    }

    // LEADERBOARD 
    public void SendLeaderboard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "waves_leaderboard",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successfull leaderboard update");
    }

    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "waves_leaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result) {
        foreach (Transform item in rowsParent) {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard) {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    void Start() { // Auto LogIn
        if (Application.isEditor && SceneManager.GetActiveScene().name == "WaveAttack") {
            var request = new LoginWithEmailAddressRequest {
                Email = "admin@admin.com",
                Password = "admin6",
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }
    }

}
