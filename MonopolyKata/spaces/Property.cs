
namespace MonopolyKata.Spaces;

public class Property : Space
{
    public event EventHandler<Player> OnPropertyBought;
    public event EventHandler<Player> OnRentPaid;
    public event EventHandler<Property> OnPropertyReset;

    public override string Name { get; }
    public bool IsOwned { get; private set; }
    public int NumBuildings { get; set; }
    public int PurchasePrice { get; }
    public int[] RentPrices { get; }
    public PropertyGroup Group { get; }
    public Player? Owner { get; private set; } = null;

    public Property(string name, int purchasePrice, int[] rentPrice, PropertyGroup group)
    {
        Name = name;
        IsOwned = false;
        NumBuildings = 0;
        PurchasePrice = purchasePrice;
        RentPrices = rentPrice;
        Group = group;
        Group.AddProperty(this);
    }

    public void Reset()
    {
        Owner = null;
        IsOwned = false;
        NumBuildings = 0;

        OnPropertyReset?.Invoke(this, this);
    }

    public override void LandedOnBy(Player player)
    {
        base.LandedOnBy(player);

        if (IsOwned && Owner == player)
            return;

        if (IsOwned)
            RentTo(player);
        else
            if (player.WantsToBuy(this)) SellTo(player);
    }

    public virtual void SellTo(Player player)
    {
        if (player.Bank >= PurchasePrice)
        {
            OnPropertyBought?.Invoke(this, player);

            player.Properties.Add(this);
            player.Bank -= PurchasePrice;
            Owner = player;
            IsOwned = true;
        }
    }

    protected virtual void RentTo(Player player)
    {
        if (Owner is null) return;

        OnRentPaid?.Invoke(this, player);

        int rent = CalculateRent();

        player.Bank -= rent;
        Owner.Bank += rent;
    }

    private int CalculateRent()
    {
        int rent = 0;

        if (HasHouses())
            rent = GetHouseAndHotelRent();
        else
            rent = GetNormalRent();

        return rent;
    }

    private bool HasHouses()
    {
        return (NumBuildings > 0);
    }

    private int GetNormalRent()
    {
        int rent = RentPrices[0];
        if (OwnerOwnsAllPropertiesInGroup())
            rent *= 2;
        return rent;
    }

    private int GetHouseAndHotelRent()
    {
        int houseRent = Group.GetNumHousesPerProperty() > 0 ?
                                RentPrices[Group.GetNumHousesPerProperty()]
                                :
                                0;
        int hotelRent = Group.GetNumHotelsPerProperty() * RentPrices[5];
        return houseRent + hotelRent;
    }

    private bool OwnerOwnsAllPropertiesInGroup()
    {
        int numberOfPropertiesInGroup = Group.GetNumberOfProperties();
        int numberOfPropertiesInGroupOwnedByOwner = Owner?.GetNumberOfPropertiesOwnedInGroup(Group) ?? 0;
        return numberOfPropertiesInGroup == numberOfPropertiesInGroupOwnedByOwner;
    }
}
