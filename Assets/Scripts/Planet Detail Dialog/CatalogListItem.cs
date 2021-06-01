using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogListItem : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public Image image;

    public void setType(AbstractType type)
    {
        //Debug.Log("labelText:"+ labelText+" type:"+type);
        labelText.text = type.name;
        string path = "";
        switch (type.typeCategory)
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

        image.sprite = Resources.Load<Sprite>(path + type.name);
        labelText.text = type.name;
    }
}
