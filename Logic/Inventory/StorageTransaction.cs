namespace Src.Logic.Inventory
{
    public class StorageTransaction
    {
        private Storage _fromStorage;
        private Storage _toStorage;

        public bool MoveItem(int fromIndex)
        {
            var itemId = _fromStorage.itemIds[fromIndex];
            var amount = _fromStorage.amounts[fromIndex];
            // invalid itemId or amount
            if (itemId == null) return false;
            if (amount < 1) return false;

            if (!_toStorage.AddItem(itemId, amount, out var indexAddedTo)) return false;

            if (_fromStorage.RemoveItem(itemId, amount, fromIndex))
            {
                _fromStorage.TriggerUpdate();
                _toStorage.TriggerUpdate();
                return true;
            }

            // rollback addition
            _toStorage.RemoveItem(itemId, amount, indexAddedTo);
            return false;
        }

        public static StorageTransaction Create(Storage fromStorage, Storage toStorage)
        {
            return new StorageTransaction
            {
                _fromStorage = fromStorage,
                _toStorage = toStorage
            };
        }
    }
}