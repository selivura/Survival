using System.Collections.Generic;

namespace Selivura.Player
{
    public enum StatModType
    {
        Flat,
        PercentAdd,
        PercentMult,
    }
    [System.Serializable]
    public class PlayerStat
    {
        public float BaseValue;
        private bool _isDirty = true;
        private float _value;
        private float _lastBaseValue = float.MinValue;
        public float Value
        {
            get
            {
                if (_isDirty || _lastBaseValue != BaseValue)
                {
                    _lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    _isDirty = false;
                }
                return _value;
            }
        }
        private readonly List<PlayerStatModifier> statModifiers;
        public PlayerStat()
        {
            statModifiers = new List<PlayerStatModifier>();
        }
        public PlayerStat(float baseValue)
        {
            BaseValue = baseValue;
            statModifiers = new List<PlayerStatModifier>();
        }
        public void AddModifier(PlayerStatModifier mod)
        {
            if (mod.Value == 0)
                return;
            _isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }
        public bool RemoveModifier(PlayerStatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                _isDirty = true;
                return true;
            }
            return false;
        }
        public void RemoveAllModifiers()
        {
            _isDirty = true;
            statModifiers.Clear();
        }
        //public void RemoveAllModifiersFromSource(object source)
        //{
        //    _isDirty = true;
        //    for (int i = 0; i < statModifiers.Count; i++)
        //    {
        //        if (statModifiers[i].Source == source)
        //            statModifiers.RemoveAt(i);
        //    }
        //}
        private int CompareModifierOrder(PlayerStatModifier a, PlayerStatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }
        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                PlayerStatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return finalValue;
        }
    }
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
