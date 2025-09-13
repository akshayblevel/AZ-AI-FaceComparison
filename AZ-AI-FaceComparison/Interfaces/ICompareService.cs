using AZ_AI_FaceComparison.Models;

namespace AZ_AI_FaceComparison.Interfaces
{
    public interface ICompareService
    {
        Task<FaceResponse> CompareFacesAsync(IFormFile image1, IFormFile image2);
    }
}
