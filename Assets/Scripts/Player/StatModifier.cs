public enum StatModifierType
{
    Flat,
    PercentageAdditive,
    PercentageMultiplicative,
}
[System.Serializable]
public class StatModifier
{
    public float Value;
    public StatModifierType Type;
    public int Order;
    public object Source;

    public StatModifier(float value, StatModifierType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    public void UpdateOrder()
    {
        Order = (int)Type;
    }

    public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null) { }
    public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null) { }
    public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source) { }

}
