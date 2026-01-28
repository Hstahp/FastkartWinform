namespace Common
{
    public static class PermCode
    {
        // =============== FUNCTIONS ===============
        public const string FUNC_PRODUCT = "PRODUCT";
        public const string FUNC_CATEGORY = "CATEGORY";
        public const string FUNC_SUBCATEGORY = "SUBCATEGORY";
        public const string FUNC_USER = "USER";
        public const string FUNC_ROLE = "ROLE";
        public const string FUNC_COUPON = "COUPON";
        public const string FUNC_ORDER = "ORDER";  // ✅ Bao gồm POS

        // ⚠️ XÓA: Không cần FUNC_POS riêng
        // public const string FUNC_POS = "POS";

        // =============== PERMISSION TYPES ===============
        public const string TYPE_VIEW = "VIEW";
        public const string TYPE_CREATE = "CREATE";
        public const string TYPE_EDIT = "EDIT";
        public const string TYPE_DELETE = "DELETE";
    }
}