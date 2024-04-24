using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Mikha.Controllers
{
    internal interface IPremiumMessageController
    {
        bool AddPremiumMessage(MessageWrapper message);
        bool DeletePremiumMessage(MessageWrapper message);
    }
}