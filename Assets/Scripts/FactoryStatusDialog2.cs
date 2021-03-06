using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FactoryStatusDialog2 : MonoBehaviour
{
    public HeaderControls headerControls;
    public TextMeshProUGUI buildTypeText;
    public Image buildTypeImg;
    public TextMeshProUGUI dayCompleteText;
    public TextMeshProUGUI daysRemainingText;
    public TextMeshProUGUI destinationText;
    public Image factoryIsWorkingIcon;

    private Factory factory;

    void Start()
    {
        factory = MainGameState.gameState.factoryForDetail;
        updateStatus();
        MainGameState.gameState.addListenerUiUpdateEvent(updateStatus);
    }

    private void updateStatus()
    {
        headerControls.setHeaderTitle(factory.type.name);

        if(factory.isBuilding)
        {
            buildTypeText.text = factory.buildingType.name;
            dayCompleteText.text = factory.buildingDoneDay.ToString();
            daysRemainingText.text = (factory.buildingDoneDay - MainGameState.gameState.gameTime).ToString();
            destinationText.text = factory.planetDestination.name;

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
        } else
        {
            SceneManager.LoadScene("Factory Build Dialog 2");
        }
    }

    public void onClickBackButton()
    {
        MainGameState.gameState.factoryForDetail = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    public void onClickCancelButton()
    {
        factory.isBuilding = false;
        factory.buildingType = null;
        MainGameState.gameState.factoryForDetail = null;
        SceneManager.LoadScene("Planet Detail 2");
    }

    private void FixedUpdate()
    {
        factoryIsWorkingIcon.transform.Rotate(Vector3.forward * (Time.deltaTime * 180.0f));
    }

}
