using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public YandexSDK SDK;
    public Bank bank;
    public Language language;
    void Awake()
    {
        // ������������� ������� ����
        if (Bank.Instance == null)
        {
            Instantiate(SDK).name = "YandexSDK";
            Instantiate(bank).name = "Bank";
            Instantiate(language).name = "Language";
        }
    }
}
