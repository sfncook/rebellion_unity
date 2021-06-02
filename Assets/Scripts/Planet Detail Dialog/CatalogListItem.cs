using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogListItem : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public Image image;

    public delegate void OnSelectItem(CatalogListItem selectedCatalogListItem);
    private OnSelectItem onSelectItem;

    private AbstractType type;

    public AbstractType getType()
    {
        return type;
    }

    public void setType(AbstractType type)
    {
        this.type = type;
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



    private void OnMouseUp()
    {
        onSelectItem(this);
    }

    public void setOnSelectItem(OnSelectItem onSelectItem)
    {
        this.onSelectItem = onSelectItem;
    }

    public void setSelected(bool isSelected)
    {
        if (isSelected)
        {
            gameObject.GetComponent<Image>().color = Color.yellow;
        } else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
}
