namespace Framework.Models
{
    public class Card
    {
        public virtual string Name { get; set; }

        public virtual int Cost { get; set; }

        public virtual string Rarity { get; set; }

        public virtual string Type { get; set; }

        public virtual string Arena { get; set; }
    }
}
