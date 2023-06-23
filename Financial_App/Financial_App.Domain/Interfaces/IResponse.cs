using Financial_App.Domain.Enums;

namespace Financial_App.Domain.Interfaces
{
    public interface IResponse
    {
        IResponse AddErro(string mensagem);
        void DefinirMotivoErro(MotivoErro motivoFalha);
    }
}
