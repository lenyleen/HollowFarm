namespace DefaultNamespace.Premades
{
    public class Potions
    {
        /*1. Создай интерфейс для эффекта
csharp
Копировать
Редактировать
public interface ISoilEffect
{
    void Apply(Soil target);
    void Update(float deltaTime);
    bool IsExpired { get; }
}
Альтернатива: IPlantEffect, если эффект на растение.

2. Пример конкретного эффекта
csharp
Копировать
Редактировать
public class GrowthBoostEffect : ISoilEffect
{
    private float _duration;
    private float _elapsed;

    public GrowthBoostEffect(float durationInSeconds)
    {
        _duration = durationInSeconds;
        _elapsed = 0;
    }

    public void Apply(Soil target)
    {
        target.SetGrowthModifier(1.5f); // Ускоряет рост
    }

    public void Update(float deltaTime)
    {
        _elapsed += deltaTime;
    }

    public bool IsExpired => _elapsed >= _duration;
}
3. В Soil — храни список активных эффектов
csharp
Копировать
Редактировать
private List<ISoilEffect> _activeEffects = new();

public void AddEffect(ISoilEffect effect)
{
    effect.Apply(this);
    _activeEffects.Add(effect);
}
В UpdateTime():

csharp
Копировать
Редактировать
foreach (var effect in _activeEffects.ToList())
{
    effect.Update(1f); // допустим, обновляется каждую секунду
    if (effect.IsExpired)
        _activeEffects.Remove(effect);
}
4. ScriptableObject для зелья
csharp
Копировать
Редактировать
[CreateAssetMenu(menuName = "Potions/GrowthPotion")]
public class GrowthPotion : PotionData
{
    public float growthMultiplier;
    public float durationInSeconds;

    public override ISoilEffect CreateEffect()
    {
        return new GrowthBoostEffect(durationInSeconds);
    }
}
csharp
Копировать
Редактировать
public abstract class PotionData : ScriptableObject
{
    public abstract ISoilEffect CreateEffect();
}
5. Применение зелья
csharp
Копировать
Редактировать
public class PotionHandler
{
    public void UsePotion(PotionData potion, Soil soil)
    {
        var effect = potion.CreateEffect();
        soil.AddEffect(effect);
    }
}*/
    }
}