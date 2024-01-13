namespace Selivura.Player
{
    [System.Serializable]
    public class PlayerStatModifier
    {
        public float Value;
        public StatModType Type;
        public int Order;
        public object Source;
        public PlayerStatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }
        public new string ToString()
        {
            if (Value > 0)
            {
                switch (Type)
                {
                    case StatModType.Flat:
                        return Value.ToString();
                    case StatModType.PercentAdd:
                        return Value + "%";
                    case StatModType.PercentMult:
                        return "x" + Value + "%";
                    default:
                        break;
                }
            }
            return "";
        }
    }

}
