using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Updates the bat cards and the buy or equip button in the Shop scene.
public class ShopMenu : MonoBehaviour
{
    [SerializeField] TMP_Text balanceLabel;
    [SerializeField] TMP_Text batNameLabel;
    [SerializeField] TMP_Text descriptionLabel;
    [SerializeField] TMP_Text actionLabel;
    [SerializeField] Button actionButton;
    [SerializeField] Image actionFace;
    [SerializeField] Image[] cardHighlights;
    [SerializeField] Image[] cardFaces;
    [SerializeField] Image[] priceFaces;
    [SerializeField] Image[] priceIcons;
    [SerializeField] TMP_Text[] priceLabels;

    private int selectedBat;

    void Start()
    {
        int equipped = BatCatalog.Equipped;
        SelectBat(equipped >= 0 && equipped < BatCatalog.Count && BatCatalog.Owns(equipped)
            ? equipped : BatCatalog.ClassicWood);
    }

    public void SelectBat(int bat)
    {
        if (bat < 0 || bat >= BatCatalog.Count) return;

        selectedBat = bat;
        Show();
    }

    public void UseSelectedBat()
    {
        if (Services.Save == null || selectedBat == BatCatalog.Equipped) return;

        if (BatCatalog.Owns(selectedBat))
        {
            Services.Save.SetInt(BatCatalog.EquippedKey, selectedBat);
        }
        else if (selectedBat != BatCatalog.FlameBat)
        {
            Services.Coins?.TryPurchase(BatCatalog.Price(selectedBat), BatCatalog.OwnershipKey(selectedBat));
        }

        Show();
    }

    void Show()
    {
        ICoinService coins = Services.Coins;
        int balance = coins?.Balance ?? 0;
        int equipped = BatCatalog.Equipped;
        balanceLabel.text = balance.ToString("N0");
        batNameLabel.text = BatCatalog.Name(selectedBat);
        descriptionLabel.text = BatCatalog.Description(selectedBat);

        for (int bat = 0; bat < BatCatalog.Count; bat++)
        {
            bool owns = BatCatalog.Owns(bat);
            bool selected = bat == selectedBat;
            cardHighlights[bat].enabled = selected;
            cardFaces[bat].color = selected ? new Color32(255, 249, 222, 255) : Color.white;
            priceIcons[bat].enabled = !owns;
            priceLabels[bat].alignment = owns ? TextAlignmentOptions.Center : TextAlignmentOptions.MidlineRight;
            priceLabels[bat].text = bat == equipped ? "EQUIPPED" : owns ? "OWNED" : BatCatalog.Price(bat).ToString("N0");
            priceFaces[bat].color = owns ? new Color32(28, 149, 221, 255)
                : bat == BatCatalog.FlameBat ? new Color32(174, 121, 241, 255) : new Color32(101, 203, 18, 255);
        }

        bool selectedIsOwned = BatCatalog.Owns(selectedBat);
        int price = BatCatalog.Price(selectedBat);
        if (selectedBat == equipped)
            actionLabel.text = "EQUIPPED";
        else if (selectedIsOwned)
            actionLabel.text = "EQUIP";
        else if (selectedBat == BatCatalog.FlameBat)
            actionLabel.text = "GEMS REQUIRED";
        else if (balance < price)
            actionLabel.text = "NEED " + (price - balance).ToString("N0") + " COINS";
        else
            actionLabel.text = "BUY " + price.ToString("N0");

        actionButton.interactable = Services.Save != null && selectedBat != equipped &&
            (selectedIsOwned || (selectedBat != BatCatalog.FlameBat && coins != null && balance >= price));
        actionFace.color = actionButton.interactable ? new Color32(101, 203, 18, 255) : new Color32(164, 169, 188, 255);
    }
}
