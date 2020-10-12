using MonopolyKata.Cards;
using System.Collections.Generic;

namespace MonopolyKata
{
    public static class DeckFactory
    {
        public static Deck CommunityChest()
        {
            Deck communityChest = new Deck( new List<Card> {
                new AdvanceToGo(),
                new BankError(),
                new DoctorsFees(),
                new SaleOfStock(),
                new GetOutOfJail(),
                new GoToJail(),
                new OperaNight(),
                new HolidayFund(),
                new IncomeTaxRefund(),
                new Birthday(),
                new LifeInsurance(),
                new HospitalFees(),
                new SchoolFees(),
                new ConsultancyFees(),
                new StreetRepairs(),
                new BeautyContest(),
                new Inheritance()
            });
            communityChest.Shuffle();
            return communityChest;
        }

        public static Deck Chance()
        {
            Deck chance = new Deck( new List<Card> {
                new AdvanceToGo(),
                new AdvanceToIllinois(),
                new AdvanceToStCharles(),
                new AdvanceToUtility(),
                new AdvanceToRailroad(),
                new AdvanceToRailroad(),
                new BankDividend(),
                new GetOutOfJail(),
                new GoBack3Spaces(),
                new GoToJail(),
                new GeneralRepairs(),
                new PoorTax(),
                new AdvanceToReading(),
                new AdvanceToBoardwalk(),
                new ElectedChairman(),
                new BuildingAndLoan(),
                new Crossword()
            });
            chance.Shuffle();
            return chance;
        }
    }
}