namespace commerce.mock
{
    public class Sequencial : ISequencial
    {
        private readonly Dictionary<string, int> _sequencial;

        public Sequencial()
        {
            _sequencial = new Dictionary<string, int>();
        }
        public int Next(string tabela)
        {
            int seq = 0;
            if (_sequencial.ContainsKey(tabela))
            {
                seq = _sequencial.FirstOrDefault(t => t.Key == tabela).Value;
                _sequencial.Remove(tabela);
            }

            ++seq;

            _sequencial.Add(tabela, seq);

            return seq;
        }
    }
}
