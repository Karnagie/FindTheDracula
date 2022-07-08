namespace Core.SaveAndLoadEssence
{
    public interface ISaveAndLoadSystem
    {
        int GetCurrentWeaponId { get;}
        int NextLevel { get;}
        void ChangeCurrentWeapon(int id);
        bool IsOpenedWeapon(int index);
        void OpenWeapon(int index);
        void AddOpenPercents(int index, float value);
        int GetCurrentOpening();
        float FillPercent();
    }
}