namespace Core.SaveAndLoadEssence
{
    public interface ISaveAndLoadSystem
    {
        int GetCurrentWeaponId { get;}
        void ChangeCurrentWeapon(int id);
        bool IsOpenedWeapon(int index);
        void OpenWeapon(int index);
    }
}