using KamilaClient.Models;

namespace KamilaClient.Storage
{
    public class SmartphoneList : IStorage<Smartphone>
    {
        private object _sync = new object();
        private List<Smartphone> _smartphoneList = new List<Smartphone>();
        public Smartphone this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectLabDataException($"No LabData with id {id}");

                    return _smartphoneList.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectLabDataException("Cannot request LabData with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _smartphoneList.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<Smartphone> All => _smartphoneList.Select(x => x).ToList();

        public void Add(Smartphone value)
        {
            if (value.Id == Guid.Empty) throw new IncorrectLabDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _smartphoneList.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _smartphoneList.RemoveAll(x => x.Id == id);
            }
        }
    }
}
