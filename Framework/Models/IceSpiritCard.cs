using System;

namespace Framework.Models
{
    public class IceSpiritCard : Card
    {
        public override string Name { get; set; } = "Ice Spirit";

        public override int Cost { get; set; } = 1;

        public override string Rarity { get; set; } = "Common";

        public override string Type { get; set; } = "Troop";

        public override int Arena { get; set; } = 8;
    }
}
