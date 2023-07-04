using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

public class ClientObject : MonoBehaviour
{
    [SerializeField] private string _host;

    private Client _client;

    public event Action<Client> Signed;
    public event Action<Client> SignFailed;
    public event Action<Client> Loged;
    public event Action<Client> LogFailed;

    public bool IsLogIn => _client.IsLogIn;

    public bool IsInGame => _client.GameInfo != null;

    public string GameString => _client.GameInfo.StringGame;

    public void SignIn(string name, string password)
    {
        Client client = new Client(name, password, _host);

        if (client.SignIn())
            Signed?.Invoke(client);
        else
            SignFailed?.Invoke(client);
    }

    public void LogIn(string name, string password)
    {
        _client = new Client(name, password, _host);
        _client.LogIn();
        if (_client.IsLogIn)
            Loged?.Invoke(_client);
        else
            LogFailed?.Invoke(_client);
    }

    public string GetGame() => _client.GetGame();

    public string AddGame(string side) => _client.AddGame(side);

    public string IntoToGame(int gameId) => _client.IntoToGame(gameId);

    public string BackToGame(int gameId) => _client.BackToGame(gameId);

    public string Step(int x, int y) => _client.Step(x, y);
}

public class Client
{
    public readonly string Host;
    public const string UsersString = "/Users";
    public const string GamesString = "/Games";
    public const string UserSignInString = "/UserSignIn";
    public const string UserLogInString = "/UserLogIn";
    public const string GetGameString = "/GetGame";
    public const string GameAddString = "/GameAdd";
    public const string IntoToGameString = "/IntoToGame";
    public const string BackToGameString = "/BackToGame";
    public const string StepString = "/Step";
    public const string GetSideString = "/GetSide";

    public User User { get; private set; }

    public GameInfo GameInfo { get; private set; }

    public string Side { get; private set; }

    public bool IsLogIn { get; private set; }

    public Client(string name, string password, string host)
    {
        User = new User(0, name, password);
        Host = host;
        IsLogIn = false;
    }

    public bool SignIn()
    {
        string res = CallServer($"{UserSignInString}/{User.Name}&{User.Password}");
        if (res == "Игрок с таким именем уже есть")
            return false;
        User = new User(ParseJson(res));
        return true;
    }

    public void LogIn()
    {
        string res = CallServer($"{UserLogInString}/{User.Name}&{User.Password}");
        if (res == "Человека с таким именем и паролем не существует")
            throw new Exception(res);
        User = new User(ParseJson(res));
        IsLogIn = true;
    }

    public string GetGame()
    {
        string res = CallServer($"{GetGameString}/{GameInfo.ID}");
        GameInfo = new GameInfo(ParseJson(res));
        return GameInfo.StringGame;
    }

    public string AddGame(string side)
    {
        Side = side;
        string res = CallServer($"{GameAddString}/{User.ID}&{Side}");
        GameInfo = new GameInfo(ParseJson(res));
        return GameInfo.StringGame;
    }

    public string IntoToGame(int gameId)
    {
        string res = CallServer($"{IntoToGameString}/{User.ID}&{gameId}");
        GameInfo = new GameInfo(ParseJson(res));
        Side = CallServer($"{GetSideString}/{gameId}&{User.ID}");
        return GameInfo.StringGame;
    }

    public string BackToGame(int gameId)
    {
        string res = CallServer($"{BackToGameString}/{gameId}&{User.ID}");
        GameInfo = new GameInfo(ParseJson(res));
        Side = CallServer($"{GetSideString}/{gameId}&{User.ID}");
        return GameInfo.StringGame;
    }

    public string Step(int x, int y)
    {
        Debug.Log($"{Host}{StepString}/{GameInfo.ID}&{User.ID}&{x}&{y}");
        string res = CallServer($"{StepString}/{GameInfo.ID}&{User.ID}&{x}&{y}");
        GameInfo = new GameInfo(ParseJson(res));
        return GameInfo.StringGame;
    }

    private string CallServer(string param = "")
    {
        Console.WriteLine(Host + param);
        WebRequest request = WebRequest.Create(Host + param);
        WebResponse response = request.GetResponse();
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
            return reader.ReadToEnd();
    }

    private NameValueCollection ParseJson(string json)
    {
        NameValueCollection list = new NameValueCollection();
        string pattern = @"""(\w+)\"":""?([^,""}]*)""?";
        foreach (Match m in Regex.Matches(json, pattern))
            if (m.Groups.Count == 3)
                list[m.Groups[1].Value] = m.Groups[2].Value;

        return list;
    }
}

public class User
{
    public int ID { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public User(int iD, string name, string password)
    {
        ID = iD;
        Name = name;
        Password = password;
    }

    public User(NameValueCollection nameValueCollection)
    {
        ID = int.Parse(nameValueCollection[nameof(ID)]);
        Name = nameValueCollection[nameof(Name)];
        Password = nameValueCollection[nameof(Password)];
    }
}

public class GameInfo
{
    public int ID { get; set; }
    public string StringGame { get; set; }
    public GameInfo(NameValueCollection list)
    {
        ID = int.Parse(list["ID"]);
        StringGame = list["StringGame"];
    }
    public override string ToString()
    {
        return $"ID {ID}\nStringGame {StringGame}";
    }
}