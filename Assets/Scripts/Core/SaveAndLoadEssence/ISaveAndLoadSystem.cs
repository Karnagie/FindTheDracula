namespace Core.SaveAndLoadEssence
{
    public interface ISaveAndLoadSystem
    {
        int GetCurrentWeaponId { get;}
        int NextLevel { get;}
        bool Tutorial { get;}
        bool OpenedNewTool { get;}
        bool OpenedNewWeapon { get;}
        void ResetOpenedNewTool();
        void ResetOpenedWeapon();
        int CurrentLevel { get; }
        int GetCurrentEquipmentId { get; }
        void ChangeCurrentWeapon(int id);
        bool IsOpenedWeapon(int index);
        void OpenWeapon(int index);
        void AddOpenWeaponPercents(int index, float value);
        int GetCurrentWeaponOpening();
        float FillWeaponPercent();
        
        
        
        void ChangeCurrentEquipment(int id);
        bool IsOpenedEquipment(int index);
        void OpenEquipment(int index);
        void AddOpenEquipmentPercents(int index, float value);
        int GetCurrentEquipmentOpening();
        float FillEquipmentPercent();
        int IsNextWeaponOrQuestion();
    }
}