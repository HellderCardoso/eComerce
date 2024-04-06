using Commerce.Domain.Entitie;

namespace comerce.domain.Model
{
    public class __ScriptMigrationHistory : Entity
    {
        public string FileName { get; set; }
        protected __ScriptMigrationHistory() { }
        public __ScriptMigrationHistory(string fileName)
        {
            FileName = fileName;
        }
    }
}
