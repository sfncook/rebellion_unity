using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class CancelFactoryBuildCommandEvent : UnityEvent<Factory>
{
}

public class FactoryStatusDialog : MonoBehaviour
{
    public GameObject detailsPanel;
    public TextMeshProUGUI labelText;
    public Image factoryImg;
    public Button closeButton;
    public TextMeshProUGUI buildTypeText;
    public Image buildTypeImg;
    public TextMeshProUGUI dayCompleteText;
    public TextMeshProUGUI daysRemainingText;
    public Button cancelButton;
    public CancelFactoryBuildCommandEvent cancelFactoryBuildCommandEvent;

    private Factory factory;

    private void Start()
    {
        closeButton.onClick.AddListener(onClickClose);
        cancelButton.onClick.AddListener(onClickCancel);
    }

    public void setFactory(Factory factory)
    {
        this.factory = factory;
        labelText.text = factory.type.name;
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
        buildTypeText.text = factory.buildingType.name;
        dayCompleteText.text = factory.buildingDoneDay.ToString();
        daysRemainingText.text = (factory.buildingDoneDay - MainGameState.gameState.gameTime).ToString();

        string path = "";
        switch (factory.buildingType.typeCategory)
        {
            case TypeCategory.Defense:
                path = "Images/Defenses/";
                break;
            case TypeCategory.Factory:
                path = "Images/Factories/";
                break;
            case TypeCategory.Personnel:
                path = "Images/Personnel/";
                break;
            case TypeCategory.Ship:
                path = "Images/Ships/";
                break;
        }
        buildTypeImg.sprite = Resources.Load<Sprite>(path + factory.buildingType.name);
    }

    public void show()
    {
        detailsPanel.SetActive(false);
        gameObject.SetActive(true);
    }

    private void onClickClose()
    {
        detailsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void onClickCancel()
    {
        cancelFactoryBuildCommandEvent.Invoke(factory);
    }
}
