namespace Framework.Models
{
    public class Card
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Icon { get; set; }

        public virtual int Cost { get; set; }

        public virtual string Rarity { get; set; }

        public virtual string Type { get; set; }

        public virtual int Arena { get; set; }
    }
}
