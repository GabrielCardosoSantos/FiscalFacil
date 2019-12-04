
using System.Threading.Tasks;

namespace FiscalFacil.Services
{
    public interface IQrCodeScanningService
    {
        Task<string> ScanAsync();
    }
}
