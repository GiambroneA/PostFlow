using Microsoft.AspNetCore.Http;
using PostFlow.Api.Features.MediaAssets.Contracts;
using PostFlow.Api.Features.MediaAssets.Models;

namespace PostFlow.Api.Features.MediaAssets.Services;

public class MediaAssetService : IMediaAssetService
{
    private static readonly List<MediaAsset> MediaAssets = [];

    public async Task<MediaAssetResponse> UploadAsync(IFormFile file)
    {
        if (file.Length == 0)
        {
            throw new ArgumentException("Uploaded file cannot be empty.");
        }

        var mediaAsset = new MediaAsset
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileSizeBytes = file.Length,
            StoragePath = $"uploads/media/{Guid.NewGuid()}-{file.FileName}",
            UploadedAtUtc = DateTime.UtcNow,
            Status = MediaAssetStatus.Uploaded
        };

        MediaAssets.Add(mediaAsset);

        return await Task.FromResult(ToResponse(mediaAsset));
    }

    public async Task<List<MediaAssetResponse>> GetAllAsync()
    {
        var responses = MediaAssets
            .Select(ToResponse)
            .ToList();

        return await Task.FromResult(responses);
    }

    public async Task<MediaAssetResponse?> GetByIdAsync(Guid id)
    {
        var mediaAsset = MediaAssets.FirstOrDefault(asset => asset.Id == id);

        if (mediaAsset is null)
        {
            return null;
        }

        return await Task.FromResult(ToResponse(mediaAsset));
    }

    private static MediaAssetResponse ToResponse(MediaAsset mediaAsset)
    {
        return new MediaAssetResponse
        {
            Id = mediaAsset.Id,
            FileName = mediaAsset.FileName,
            ContentType = mediaAsset.ContentType,
            FileSizeBytes = mediaAsset.FileSizeBytes,
            StoragePath = mediaAsset.StoragePath,
            UploadedAtUtc = mediaAsset.UploadedAtUtc,
            Status = mediaAsset.Status
        };
    }
}