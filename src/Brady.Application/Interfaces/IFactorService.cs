using Brady.Domain.Enum;

namespace Brady.Application.Interfaces
{
    public interface IFactorService
    {
        public decimal ValueFactor(GenerationType type, WindType windType = default);
          
        public decimal EmissionFactor(GenerationType type);
    }
}
