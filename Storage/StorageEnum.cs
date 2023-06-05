namespace KamilaClient.Storage
{
    public enum StorageEnum
    {
        Undefined,
        SmartphoneList,
        FileStorage
    }

    public static class StorageEnumExtensions
    {
        public static StorageEnum ToStorageEnum(this string value)
        {
            switch (value)
            {
                case var s when s.ToLowerInvariant() == "SmartphoneList"
                                || s.ToLowerInvariant() == "Smartphone"
                                || s.ToLowerInvariant() == "сonsolesList":
                    return StorageEnum.SmartphoneList;
                case var s when s.ToLowerInvariant() == "filestorage"
                                || s.ToLowerInvariant() == "file"
                                || s.ToLowerInvariant() == "storage":
                    return StorageEnum.FileStorage;
                default:
                    return StorageEnum.Undefined;
            }
        }
    }
}
