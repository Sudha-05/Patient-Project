using GestorPacientes.Core.Domain.Common;

namespace GestorPacientes.Core.Domain.Entities
{
    public class LaboratoryTest : AuditableBaseEntity
    {
        public string Name { get; set; }

        //navigation property
        public ICollection<LaboratoryResult>? LaboratoryResults{ get; set; }
    }
}
