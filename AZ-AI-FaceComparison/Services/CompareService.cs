using AZ_AI_FaceComparison.Interfaces;
using AZ_AI_FaceComparison.Models;
using Azure.AI.Vision.Face;

namespace AZ_AI_FaceComparison.Services
{
    public class CompareService(FaceClient faceClient) : ICompareService
    {
        public async Task<FaceResponse> CompareFacesAsync(IFormFile image1, IFormFile image2)
        {
            List<FaceDetectionResult> faces1, faces2;

            using (var stream1 = image1.OpenReadStream())
            {
                var binary1 = BinaryData.FromStream(stream1);
                var result1 = await faceClient.DetectAsync(
                    binary1,
                    FaceDetectionModel.Detection03,
                    FaceRecognitionModel.Recognition04,
                    returnFaceId: true);

                faces1 = result1.Value.ToList();
            }

            using (var stream2 = image2.OpenReadStream())
            {
                var binary2 = BinaryData.FromStream(stream2);
                var result2 = await faceClient.DetectAsync(
                    binary2,
                    FaceDetectionModel.Detection03,
                    FaceRecognitionModel.Recognition04,
                    returnFaceId: true);

                faces2 = result2.Value.ToList();
            }

            var faceId1 = faces1[0].FaceId;
            var faceId2 = faces2[0].FaceId;


            var verifyResult = await faceClient.VerifyFaceToFaceAsync(faceId1.Value, faceId2.Value);

            var response = new FaceResponse() { IsIdentical = verifyResult.Value.IsIdentical, Confidence = verifyResult.Value.Confidence };

            return response;
        }
    }
}
