public class Helper
{
    public static readonly string NULLOBJECT = "对象不存在";

    public static readonly string EmptyData = "No Data";

    public static string GetEquipmentField(object equipmentId, string fieldName)
    {
        using (var equipmentDetails =
            TSS.BLL.RepositoryFactory<TSS.BLL.EquipmentDetails>.Get()) {
            return equipmentDetails.GetValue(equipmentId.ToString(), fieldName);
        }
    }
}