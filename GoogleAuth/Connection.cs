namespace Helper
{
    public class Connection : IConnection
    {
        public IProvider Provider { get; }
        public object Values { get; }
        public string Name { get; }
        public int Id { get; }

        public Connection(int id, string name, IProvider provider, object values)
        {
            Id = id;
            Provider = provider;
            Values = values;
            Name = name;
        }

        public override string ToString()
        {
            return $"Connection: {Name}, {Provider.Name} - {Values}";
        }
    }
}