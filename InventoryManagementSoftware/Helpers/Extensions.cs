namespace InventoryManagementSoftware.Web
{
    public static class Extensions
    {
        public static bool IsSet(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return false;

            return true;
        }
    }
}
