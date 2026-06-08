using Microsoft.AspNetCore.Http;
using PostFlow.Api.Features.MediaAssets.Contracts;

namespace PostFlow.Api.Features.MediaAssets.Services;

public interface IMediaAssetService
{
    Task<MediaAssetResponse> UploadAsync(IFormFile file);

    Task<List<MediaAssetResponse>> GetAllAsync();

    Task<MediaAssetResponse?> GetByIdAsync(Guid id);
}